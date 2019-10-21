using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.SkillSystem.Runtime.Utils;

namespace Scripts.Buff
{
    /// <summary>
    /// 持续时间的能力-百分比
    /// </summary>
    public struct MechanicsTimeP:IMechanicBuff,IBuffTimeDataComponent,IIcon,IPercentage
    {
        private float _value;

        public float Value
        {
            get => _value / 100;
            set => _value = value;
        }

        public MechanicsType MechanicsType { get; set;}
        public float Duration { get; set; }
        public ECSString IconName { get; set; }
    }
}