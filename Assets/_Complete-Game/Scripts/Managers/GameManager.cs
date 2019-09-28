//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-14:41
//Assembly-CSharp

using System;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs.Systems;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Skills.Manager;
using Scripts.Buff.System;
using SkillSystem.SkillSystem.Scripts.Expansion.Runtime.Builtin.Skills.Manager;
using UnityEngine;

namespace CompleteProject
{
    [DefaultExecutionOrder(-1)]
    public class GameManager:MonoBehaviour
    {
        public static GameManager Manager;

        public IBuffManager BuffManager;

        public ISkillManager SkillManager;
        
        private void Awake()
        {
            Manager = this;

            BuffManager = new BuffManager();

            SkillManager = new SkillManager();


            BuffManager
                .AddBuffSystem(new BuffTimeSystem(BuffManager))
                .AddBuffSystem(new DamageReduceFixedSystem(BuffManager))
                .AddBuffSystem(new DamageReducePercentageSystem(BuffManager))
                .AddBuffSystem(new LifestealFixedSystem(BuffManager))
                .AddBuffSystem(new LifestealPercentageSystem(BuffManager))
                .AddBuffSystem(new DamageSystem(BuffManager))
                .AddBuffSystem(new DeathSystem(BuffManager));
        }

        private void Update()
        {
            BuffManager.Update();
        }
    }
}