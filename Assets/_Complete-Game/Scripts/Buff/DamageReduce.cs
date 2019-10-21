//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:29
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.SkillSystem.Runtime.Utils;
using UnityEngine;

namespace Scripts.Buff
{
    public struct DamageReduceFixed:IDamageReduceFixedBuff,IBuffTimeDataComponent,IIcon
    {
        public float Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public float Duration { get; set; }
        public ECSString IconName { get; set; }
    }

    public struct DamageReducePercentage:IDamageReducePercentageBuff,IBuffTimeDataComponent,IIcon
    {
        public float Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public float Duration { get; set; }
        public ECSString IconName { get; set; }
    }
}