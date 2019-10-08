//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-15:11
//Assembly-CSharp

using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems.Interfaces;

namespace Scripts.Buff.System
{
    public class DeathSystem:ABuffUpdateSystem<IBuffDataComponent>,IBuffCreateSystem<IBuffDataComponent>
    {
        private List<IMechanicBuff> _buffs;

        public DeathSystem(IBuffManager<IBuffDataComponent> buffManager) : base(buffManager)
        {
            _buffs = new List<IMechanicBuff>();
        }


        public override bool Filter(IEntity entity)
        {
            return BuffManager.HasBuff<IMechanicBuff>(entity,x=>x.MechanicsType == MechanicsType.Health) &&
                   !BuffManager.HasBuff<Death>(entity);
        }

        public override void Execute(IEntity entity)
        {
            BuffManager.GetBuffs(entity,x=>x.MechanicsType == MechanicsType.Health,_buffs);

            var buff = _buffs[0];

            if (buff.Value <= 0)
            {
                BuffManager.CreateAndAddBuff<Death>(entity,null);
            }
        }

        public bool Filter(IEntity entity, IBuffDataComponent buff)
        {
            return buff is Death;
        }

        public void Create(IEntity entity, IBuffDataComponent buff)
        {
            BuffManager.DestroyEntityEx(entity);
        }
    }
}