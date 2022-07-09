using EelSlapperMod.Content.NPCs.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EelSlapperMod.Content.Buffs
{
    public class EelSlapperSummonTagDebuff
        : ModBuff
    {
        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EelSlapperSummonTagDebuffNPC>().MarkedByEelSlapper = true;
        }
    }
}
