//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:17
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CompleteProject;
using UnityEngine;

namespace Scripts.Buff
{
    public class BuffComponent:MonoBehaviour
    {
        public IBuffDataComponent Buff;
        
        private void OnTriggerEnter(Collider other)
        {
            if (Buff == null)
            {
                return;
            }
            
            var entity = other.GetComponent<IEntity>();
            
            if (entity != null)
            {
                GameManager.Manager.BuffManager.AddBuff(entity,Buff);
                Destroy(gameObject);
            }    
        }
    }
}