using ImmortalVehicles.Types;
using Rocket.Core;
using Rocket.Unturned.Player;

namespace ImmortalVehicles
{
    public static class Main
    {
        public static DamageGroup GetDamageGroup(this UnturnedPlayer uPlayer)
        {
            foreach (var damageGroup in Plugin.Instance.Configuration.Instance.DamageGroups)
            {
                foreach (var permGroup in R.Permissions.GetGroups(uPlayer, false))
                {
                    if (damageGroup.Id.ToLower() == permGroup.Id.ToLower())
                        return damageGroup;
                }
            }
            return null;
        }
    }
}
