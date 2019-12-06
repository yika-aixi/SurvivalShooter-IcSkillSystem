//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-14:41
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs.Systems;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Skills.Manager;
using NPBehave;
using Scripts.Buff;
using Scripts.Buff.System;
using UnityEngine;

namespace CompleteProject
{
    [DefaultExecutionOrder(-1)]
    public class GameManager:MonoBehaviour
    {
        public int Frame = 60;
        public static GameManager Manager;

//        public IcSkSEntityManager EntityManager;

        public BuffManager_Struct<IIcSkSEntity> BuffManager;

        public ISkillManager SkillManager;

        public string SharedBlackboardKey = "Game Shared";
        
        public string BuffManagerKey = "BuffManager";
        
        public string SkillManagerKey = "SkillManager";

        public string CurrentEntityKey = "CurrentEntity";
        
        public string TargetEntityKey = "TargetEntity";
        
        private void Awake()
        {
            Manager = this;

            Application.targetFrameRate = Frame;

            var blackboard = UnityContext.GetSharedBlackboard(SharedBlackboardKey);

            BuffManager = new BuffManager_Struct<IIcSkSEntity>();

//            EntityManager = new IcSkSEntityManager(BuffManager);

            blackboard.Set(BuffManagerKey,BuffManager);
            
            blackboard.Set(SkillManagerKey,SkillManager);

            BuffManager
                //时间减少
                .AddBuffSystem<LifestealFixed>(new BuffTimeSystem<LifestealFixed>(BuffManager))
                .AddBuffSystem<LifestealPercentage>(new BuffTimeSystem<LifestealPercentage>(BuffManager))
                .AddBuffSystem<DamageReduceFixed>(new BuffTimeSystem<DamageReduceFixed>(BuffManager))
                .AddBuffSystem<DamageReducePercentage>(new BuffTimeSystem<DamageReducePercentage>(BuffManager))
                .AddBuffSystem<MechanicsTime>(new BuffTimeSystem<MechanicsTime>(BuffManager))
                .AddBuffSystem<MechanicsTimeP>(new BuffTimeSystem<MechanicsTimeP>(BuffManager))

                //攻击速度计算
                .AddBuffSystem<MechanicsTimeP>(new AttackSpeedPercentageSystem(BuffManager))
                //固定伤害减少
                .AddBuffSystem<Damage>(new DamageReduceFixedSystem<DamageReduceFixed, Damage>(BuffManager))
                //百分比伤害减少
                .AddBuffSystem<Damage>(new DamageReducePercentageSystem<DamageReducePercentage, Damage>(BuffManager))
                //固定吸血
                .AddBuffSystem<Damage>(new LifestealFixedSystem<Mechanics, LifestealFixed, Damage>(BuffManager))
                //百分比吸血
                .AddBuffSystem<Damage>(new LifestealPercentageSystem<Mechanics,LifestealPercentage,Damage>(BuffManager))
                //伤害处理
                .AddBuffSystem<Damage>(new DamageSystem<Mechanics,Damage>(BuffManager))
                .AddBuffSystem<Damage>(new HealthChangeSys<Damage>(BuffManager))
                
                //死亡
                .AddBuffSystem<DeathStruct>(new DeathSystem(BuffManager));
        }

        private void Update()
        {
            BuffManager.Update();
        }
    }
}