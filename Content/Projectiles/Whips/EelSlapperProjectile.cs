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
        private const int SegmentCount = 20;
        private const float RangeMultiplier = 0.5f;

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

        private void DrawLine(in List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;

            Rectangle frame = texture.Frame();
            Vector2 origin = new(frame.Width / 2, 2);

            Vector2 position = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 difference = list[i + 1] - element;

                float rotation = difference.ToRotation() - MathHelper.PiOver2;

                Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.White);
                Vector2 scale = new Vector2(1, (difference.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, position - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                position += difference;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(list);

            Main.DrawWhip_WhipBland(Projectile, list);

            return false;
        }
    }
}
