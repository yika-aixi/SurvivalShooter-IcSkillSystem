//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-18:21
//Assembly-CSharp

using System;
using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CompleteProject;

namespace Scripts.Buff
{
    public static class Utils
    {
        public static float GetBuffSumValue<T>(this IEntity entity,List<T> buffBuffer,Predicate<T> match) where T : IBuffDataComponent,IBuffValueDataComponent
        {
            buffBuffer.Clear();
            
            float value = 0;
            
            GameManager.Manager.BuffManager.GetBuffs(entity,match,buffBuffer);
            
            foreach (var buff in buffBuffer)
            {
                value += buff.Value;
            }

            return value;
        }

    }
}