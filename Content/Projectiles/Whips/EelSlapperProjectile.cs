using EelSlapperMod.Content.Buffs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EelSlapperMod.Content.Projectiles.Whips
{
    public class EelSlapperProjectile
        : ModProjectile
    {
        private const int SegmentCount = 20;
        private const float RangeMultiplier = 0.95f;

        private const int SummonTagBuffTime = 360;
        private const float MultiHitPenalty = 0.33f;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.DefaultToWhip();

            Projectile.WhipSettings.Segments = SegmentCount;
            Projectile.WhipSettings.RangeMultiplier = RangeMultiplier;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<EelSlapperSummonTagDebuff>(), SummonTagBuffTime);
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;

            Projectile.damage = Math.Max((int)(damage * (1.0f - MultiHitPenalty)), 1);
        }
    }
}
