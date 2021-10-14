using ImmortalVehicles.Types;
using Rocket.API;
using System.Collections.Generic;

namespace ImmortalVehicles
{
    public class Config : IRocketPluginConfiguration
    {
        public List<DamageGroup> DamageGroups;
        public void LoadDefaults()
        {
            DamageGroups = new List<DamageGroup>
            {
                new DamageGroup
                {
                    Id = "admin",
                    CanDamage = true,
                    CanDamageTires = true,
                },
                new DamageGroup
                {
                    Id = "vip",
                    CanDamage = false,
                    CanDamageTires = true,
                }
            };
        }
    }
}