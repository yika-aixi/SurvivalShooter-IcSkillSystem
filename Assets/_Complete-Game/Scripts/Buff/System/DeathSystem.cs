//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-15:11
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems;
using NotImplementedException = System.NotImplementedException;

namespace Scripts.Buff.System
{
    public class DeathSystem:ABuffUpdateSystem
    {
        public DeathSystem(IBuffManager buffManager) : base(buffManager)
        {
        }


        public override bool Filter(IEntity entity)
        {
            return BuffManager.HasBuff<IMechanicBuff>(entity,x=>x.MechanicsType == MechanicsType.Health) &&
                   !BuffManager.HasBuff<Death>(entity);
        }

        public override void Execute(IEntity entity)
        {
            var buffs = BuffManager.GetBuffs<IMechanicBuff>(entity,x=>x.MechanicsType == MechanicsType.Health);
            
            var enumerator = buffs.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return;
            }
            
            var buff = enumerator.Current;

            if (buff.Value <= 0)
            {
                BuffManager.AddBuff(entity,new Death());
            }
        }
    }
}