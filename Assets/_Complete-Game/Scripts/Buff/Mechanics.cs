//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-14:39
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using UnityEngine;

namespace Scripts.Buff
{
    public interface IPercentage{}
    
    public class Mechanics:IMechanicBuff
    {
        public float Value { get; set; }
        public MechanicsType MechanicsType { get; set; }
    }
    
    /// <summary>
    /// 持续时间的能力
    /// </summary>
    public class MechanicsTime:IMechanicBuff,IBuffTimeDataComponent,IIcon
    {
        public float Value { get; set; }
        public MechanicsType MechanicsType { get; set;}
        public float Duration { get; set; }
        public Sprite Icon { get; set; }
        public string IconName { get; set; }
    }
    
    /// <summary>
    /// 持续时间的能力
    /// </summary>
    public struct MechanicsTimeStruct:IMechanicBuffStruct,IBuffTimeDataComponent
    {
        public float Value { get; set; }
        public MechanicsType MechanicsType { get; set;}
        public float Duration { get; set; }
        public Sprite Icon { get; set; }
        public string IconName { get; set; }
        public int ID { get; }
    }
}