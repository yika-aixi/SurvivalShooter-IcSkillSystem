//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:02
//Assembly-CSharp

using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems.Interfaces;
using NotImplementedException = System.NotImplementedException;

namespace Scripts.Buff.System
{
    public class AttackSpeedPercentageSystem:AIcStructBuffSystem<IcSkSEntity,MechanicsTimeP>
    {
        private readonly IStructBuffManager<IcSkSEntity> _buffManager;

        public override void Create(IcSkSEntity entity, int index)
        {
            var mechanics = _buffManager.GetBuffs<Mechanics>(entity);
            
            var buffData = _buffManager.GetBuffData<MechanicsTimeP>(entity, index);
            
            if (buffData.MechanicsType == MechanicsType.AttackSpeed)
            {
                //只已第一条攻速属性buff为准
                for (var i = 0; i < mechanics.Count; i++)
                {
                    var mechanise = mechanics[i];
                    if (mechanise.MechanicsType == MechanicsType.AttackSpeed)
                    {
                        mechanise.AddValue += mechanise.BaseValue * buffData.Value;
                        _buffManager.SetBuffData(entity,mechanise,i);
                        
                        break;
                    }
                }

//                if (mechanics != null)
//                {
//                    var enumerator = mechanics.GetEnumerator();
//
//                    enumerator.MoveNext();
//
//                    var mechanic = enumerator.Current;
//                    
//                    mechanic.Value *= buffData.Value;
//                    
//                    _buffManager.SetBuffData(entity,mechanic,0);
//                }
            }
        }
        
        public override void Destroy(IcSkSEntity entity, int index)
        {
            var mechanics = _buffManager.GetBuffs<Mechanics>(entity);
            
            var buffData = _buffManager.GetBuffData<MechanicsTimeP>(entity, index);
            
            if (buffData.MechanicsType == MechanicsType.AttackSpeed)
            {
                //只已第一条攻速属性buff为准
                for (var i = 0; i < mechanics.Count; i++)
                {
                    var mechanise = mechanics[i];
                    if (mechanise.MechanicsType == MechanicsType.AttackSpeed)
                    {
                        mechanise.AddValue -= mechanise.BaseValue * buffData.Value;
                        _buffManager.SetBuffData(entity,mechanise,i);
                        break;
                    }
                }

//                if (mechanics != null)
//                {
//                    var enumerator = mechanics.GetEnumerator();
//
//                    enumerator.MoveNext();
//
//                    var mechanic = enumerator.Current;
//                    
//                    mechanic.Value *= buffData.Value;
//                    
//                    _buffManager.SetBuffData(entity,mechanic,0);
//                }
            }
        }

        public AttackSpeedPercentageSystem(IStructBuffManager<IcSkSEntity> buffManager) : base(buffManager)
        {
        }
    }
}