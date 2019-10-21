using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.SkillSystem.Runtime.Utils;

namespace Scripts.Buff
{
    /// <summary>
    /// 持续时间的能力
    /// </summary>
    public struct MechanicsTime:IMechanicBuff,IBuffTimeDataComponent,IIcon
    {
        public float Value { get; set; }
        public MechanicsType MechanicsType { get; set;}
        public float Duration { get; set; }
        public ECSString IconName { get; set; }
    }
}