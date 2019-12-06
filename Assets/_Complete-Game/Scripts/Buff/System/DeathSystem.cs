//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-15:11
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems.Interfaces;

namespace Scripts.Buff.System
{
    public class DeathSystem:IBuffUpdateSystem
    {
        private readonly IStructBuffManager<IIcSkSEntity> _buffManager;

        public void Execute()
        {
            foreach (var entity in _buffManager.Entitys)
            {
                var buffs = _buffManager.GetBuffs<Mechanics>(entity);

                foreach (var mechanicse in buffs)
                {
                    if (mechanicse.MechanicsType == MechanicsType.Health)
                    {
                        if (mechanicse.Value <= 0 && !_buffManager.HasBuff(entity,new DeathStruct()))
                        {
                            _buffManager.AddBuff(entity,new DeathStruct());
                        }
                    }           
                }
            }
        }

        public DeathSystem(IStructBuffManager<IIcSkSEntity> buffManager)
        {
            this._buffManager = buffManager;
        }
    }
}