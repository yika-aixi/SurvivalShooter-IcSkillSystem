//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-14:39
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;

namespace Scripts.Buff
{
    public class Mechanics:IMechanicBuff
    {
        public Mechanics(MechanicsType mechanicsType)
        {
            MechanicsType = mechanicsType;
        }

        public float Value { get; set; }
        public MechanicsType MechanicsType { get; }
    }
}