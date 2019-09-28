//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:02
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems;

namespace Scripts.Buff.System
{
    public class AttackSpeedPercentageSystem:ABuffCreateSystem
    {
        public AttackSpeedPercentageSystem(IBuffManager buffManager) : base(buffManager)
        {
        }

        public override bool Filter(IEntity entity, IBuffDataComponent buff)
        {
            return buff is IMechanicBuff;
        }

        public override void Create(IEntity entity, IBuffDataComponent buff)
        {
            IMechanicBuff mechanicBuff = (IMechanicBuff) buff;

            if (!(mechanicBuff is IPercentage))
            {
                return;
            }
            
            var buffs = BuffManager.GetBuffs<IMechanicBuff>(entity, x => x.MechanicsType == MechanicsType.AttackSpeed);

            if (buffs != null)
            {
                var tor = buffs.GetEnumerator();

                if (tor.MoveNext())
                {
                    mechanicBuff.Value *= tor.Current.Value;
                    return;
                }
            }

            mechanicBuff.Value = 0;
        }
    }
}