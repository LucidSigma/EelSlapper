using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EelSlapperMod.Content.NPCs.Globals
{
    public class EelSlapperSummonTagDebuffNPC
        : GlobalNPC
    {
        private const int SummonTagDamage = 6;

        public override bool InstancePerEntity => true;

        public bool MarkedByEelSlapper { get; internal set; } = false;

        public override void ResetEffects(NPC npc)
        {
            MarkedByEelSlapper = false;
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (MarkedByEelSlapper && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                damage += SummonTagDamage;
            }
        }
    }
}
