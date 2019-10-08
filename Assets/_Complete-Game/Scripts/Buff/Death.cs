//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-15:14
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;

namespace Scripts.Buff
{
    public class Death:IBuffDataComponent
    {
    }
    
    public struct DeathStruct:IStructBuffDataComponent
    {
        public int ID { get; }
    }
}