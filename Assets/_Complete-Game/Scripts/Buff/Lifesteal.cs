//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:27
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;

namespace Scripts.Buff
{
    /// <summary>
    /// 固定吸血
    /// </summary>
    public class LifestealFixed:IFixedLifesteal
    {
        public float LifestealValue { get; set; }
    }
    
    /// <summary>
    /// 百分比吸血
    /// </summary>
    public class LifestealPercentage:IPercentageLifesteal
    {
        public float LifestealValue { get; set; }
    }
}