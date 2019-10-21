//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:17
//Assembly-CSharp

using System;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CompleteProject;
using SkillSystem.SkillSystem.Scripts.Expansion.Runtime.Builtin.Entitys;
using UnityEngine;

namespace Scripts.Buff
{
    public class BuffComponent:MonoBehaviour
    {
        public BuffType Type;

        public DamageReduceFixed DamageReduceFixed;

        public DamageReducePercentage DamageReducePercentage;

        public MechanicsTime MechanicsTime;

        public LifestealFixed LifestealFixed;

        public LifestealPercentage LifestealPercentage;
        
        private void OnTriggerEnter(Collider other)
        {
            if (Type == BuffType.None)
            {
                return;
            }
            
            var entity = other.GetComponent<EntityBindComponent>();
            
            if (entity != null)
            {
                switch (Type)
                {
                    case BuffType.伤害减少_固定:
                        GameManager.Manager.BuffManager.AddBuff(entity.Entity,DamageReduceFixed);
                        break;
                    case BuffType.伤害减少_百分比:
                        GameManager.Manager.BuffManager.AddBuff(entity.Entity,DamageReducePercentage);
                        break;
                    case BuffType.属性:
                        GameManager.Manager.BuffManager.AddBuff(entity.Entity,MechanicsTime);
                        break;
                    case BuffType.吸血_固定:
                        GameManager.Manager.BuffManager.AddBuff(entity.Entity,LifestealFixed);
                        break;
                    case BuffType.吸血_百分比:
                        GameManager.Manager.BuffManager.AddBuff(entity.Entity,LifestealPercentage);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                Destroy(gameObject);
            }    
        }
    }
}