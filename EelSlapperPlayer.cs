using EelSlapperMod.Content.Items.Weapons.Whips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace EelSlapperMod
{
    public class EelSlapperPlayer
        : ModPlayer
    {
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            const int CatchRate = 45;

            if (!Player.ZoneBeach)
            {
                return;
            }

            if (Main.rand.NextBool(CatchRate))
            {
                itemDrop = ModContent.ItemType<EelSlapper>();
            }
        }
    }
}
