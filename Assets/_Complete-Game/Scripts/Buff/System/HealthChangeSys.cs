using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems.Interfaces;

namespace Scripts.Buff.System
{
    public class HealthChangeSys<TDamage>:IBuffDestroySystem<IIcSkSEntity> where TDamage : struct, IDamage, IBuffDataComponent
    {
        private readonly IStructBuffManager<IIcSkSEntity> _buffManager;
        
        public HealthChangeSys(IStructBuffManager<IIcSkSEntity> buffManager)
        {
            this._buffManager = buffManager;
        }
        
        public void Destroy(IIcSkSEntity entity, int index)
        {
            var buff = _buffManager.GetBuffData<TDamage>(entity, index);
            
            
            switch (entity)
            {
                case CompleteProject.PlayerHealth playerHealth:
                    playerHealth.CurrentHealth -= buff.Value;
                    break;
                case CompleteProject.EnemyHealth enemyHealth :
                    enemyHealth.CurrentHealth -= buff.Value;
                    break;
            }
        }
    }
}