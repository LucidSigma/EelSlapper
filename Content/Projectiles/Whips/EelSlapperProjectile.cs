using EelSlapperMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EelSlapperMod.Content.Projectiles.Whips
{
    public class EelSlapperProjectile
        : ModProjectile
    {
        private const int SegmentCount = 30;
        private const float RangeMultiplier = 0.42f;

        private const int SummonTagBuffTime = 360;
        private const float MultiHitPenalty = 1.0f / 3.0f;

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

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new();
            Projectile.FillWhipControlPoints(Projectile, list);

            SpriteEffects flip = Projectile.spriteDirection < 0
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;

            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 position = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                Rectangle frame = new(0, 0, 10, 26);

                if (i == list.Count - 2)
                {
                    frame.Y = 74;
                    frame.Height = 18;
                }
                else if (i > 10)
                {
                    frame.Y = 58;
                    frame.Height = 16;
                }
                else if (i > 5)
                {
                    frame.Y = 42;
                    frame.Height = 16;
                }
                else if (i > 0)
                {
                    frame.Y = 26;
                    frame.Height = 16;
                }

                Vector2 element = list[i];
                Vector2 difference = list[i + 1] - element;

                Vector2 origin = new(5.0f, 8.0f);
                float rotation = difference.ToRotation() - MathHelper.PiOver2;
                float scale = 1.0f;
                Color colour = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(
                    texture,
                    position - Main.screenPosition,
                    frame,
                    colour,
                    rotation,
                    origin,
                    scale,
                    flip,
                    0
                );

                position += difference;
            }

            return false;
        }
    }
}
