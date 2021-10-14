using ImmortalVehicles.Types;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;

namespace ImmortalVehicles
{
    public class Plugin : RocketPlugin<Config>
    {
        public override TranslationList DefaultTranslations => new TranslationList
        {

        };

        public static Plugin Instance { get; private set; }
        
        protected override void Load()
        {
            Instance = this;
            VehicleManager.onDamageVehicleRequested += OnDamageVehicle;
            VehicleManager.onDamageTireRequested += OnDamageVehicleTire;
        }

        private void OnDamageVehicleTire(CSteamID instigatorSteamID, InteractableVehicle vehicle, int tireIndex, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            try
            {
                if (!CanDamage(instigatorSteamID, vehicle, EDamageType.Tire))
                    shouldAllow = false;
            }
            catch (Exception ex)
            {
                Rocket.Core.Logging.Logger.LogException(ex, "Exception in OnDamageVehicleTire()");
            }
        } 

        private void OnDamageVehicle(CSteamID instigatorSteamID, InteractableVehicle vehicle, ref ushort pendingTotalDamage, ref bool canRepair, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            try
            {
                if (!CanDamage(instigatorSteamID, vehicle, EDamageType.Vehicle))
                    shouldAllow = false;
            }
            catch (Exception ex)
            {
                Rocket.Core.Logging.Logger.LogException(ex, "Exception in OnDamageVehicle()");
            }
        }
        
        private bool CanDamage(CSteamID instigator, InteractableVehicle vehicle, EDamageType type)
        {
            try
            {
                if (instigator == default)
                    return true;

                var uPlayer = UnturnedPlayer.FromCSteamID(instigator);

                if (uPlayer.IsAdmin)
                    return true;

                if (!vehicle.isDriven)
                    return false;

                var damageGroup = uPlayer.GetDamageGroup();

                if (damageGroup == null)
                    return false;

                if (type == EDamageType.Tire && damageGroup.CanDamageTires || type == EDamageType.Vehicle && damageGroup.CanDamage)
                    return true;
            }
            catch (Exception ex)
            {
                Rocket.Core.Logging.Logger.LogException(ex, "Exception in CanDamage()");
            }
            return false;
        }

        protected override void Unload()
        {
            VehicleManager.onDamageVehicleRequested -= OnDamageVehicle;
            VehicleManager.onDamageTireRequested -= OnDamageVehicleTire;
        }
    }
}
