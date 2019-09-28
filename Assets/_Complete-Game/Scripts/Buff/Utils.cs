//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-18:21
//Assembly-CSharp

using System;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CompleteProject;

namespace Scripts.Buff
{
    public static class Utils
    {
        public static float GetBuffSumValue<T>(this IEntity entity,Predicate<T> match) where T : IBuffDataComponent,IBuffValueDataComponent
        {
            float value = 0;
            
            var buffs = GameManager.Manager.BuffManager.GetBuffs(entity,match);

            if (buffs == null)
            {
                return value;
            }
            
            foreach (var buff in buffs)
            {
                value += buff.Value;
            }

            return value;
        }

    }
}