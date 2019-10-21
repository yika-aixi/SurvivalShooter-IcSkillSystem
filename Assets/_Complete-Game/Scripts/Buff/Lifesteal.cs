//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:27
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.SkillSystem.Runtime.Utils;
using UnityEngine;

namespace Scripts.Buff
{
    /// <summary>
    /// 固定吸血
    /// </summary>
    public struct LifestealFixed:IFixedLifesteal,IBuffTimeDataComponent,IIcon
    {
        public float Value { get; set; }
        public float Duration { get; set; }
        public ECSString IconName { get; set; }
        public int Type { get; set; }
    }
    
    /// <summary>
    /// 百分比吸血
    /// </summary>
    public struct LifestealPercentage:IPercentageLifesteal,IBuffTimeDataComponent,IIcon
    {
        private float _value;

        public float Value
        {
            get => _value / 100f;
            set => _value = value;
        }

        public float Duration { get; set; }
        public ECSString IconName { get; set; }
        public int Type { get; set; }
    }

}