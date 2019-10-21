//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-14:41
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Skills.Manager;
using NPBehave;
using Scripts.Buff.System;
using SkillSystem.SkillSystem.Scripts.Expansion.Runtime.Builtin.Entitys;
using UnityEngine;

namespace CompleteProject
{
    [DefaultExecutionOrder(-1)]
    public class GameManager:MonoBehaviour
    {
        public static GameManager Manager;

        public IcSkSEntityManager EntityManager;

        public BuffManager_Struct BuffManager;

        public ISkillManager SkillManager;

        public string SharedBlackboardKey = "Game Shared";
        
        public string BuffManagerKey = "BuffManager";
        
        public string SkillManagerKey = "SkillManager";

        public string CurrentEntityKey = "CurrentEntity";
        
        public string TargetEntityKey = "TargetEntity";
        
        private void Awake()
        {
            Manager = this;

            var blackboard = UnityContext.GetSharedBlackboard(SharedBlackboardKey);

            BuffManager = new BuffManager_Struct();

            blackboard.Set(BuffManagerKey,BuffManager);
            
            blackboard.Set(SkillManagerKey,SkillManager);

//            BuffManager
//                .AddBuffSystem(new BuffTimeSystem<IBuffDataComponent>(BuffManager))
//                .AddBuffSystem(new AttackSpeedPercentageSystem(BuffManager))
//                .AddBuffSystem(new DamageReduceFixedSystem(BuffManager))
//                .AddBuffSystem(new DamageReducePercentageSystem(BuffManager))
//                .AddBuffSystem(new LifestealFixedSystem(BuffManager))
//                .AddBuffSystem(new LifestealPercentageSystem(BuffManager))
//                .AddBuffSystem(new DamageSystem(BuffManager))
//                .AddBuffSystem(new DeathSystem(BuffManager));
        }

        private void Update()
        {
            BuffManager.Update();
        }
    }
}