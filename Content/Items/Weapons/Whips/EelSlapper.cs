using EelSlapperMod.Content.Projectiles.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EelSlapperMod.Content.Items.Weapons.Whips
{
    public class EelSlapper
        : ModItem
    {
        private const int Damage = 22;
        private const int Knockback = 4;
        private const int Velocity = 10;
        private const int UseTime = 30;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<EelSlapperProjectile>(), Damage, Knockback, Velocity, UseTime);

            Item.rare = ItemRarityID.Orange;
        }
    }
}
