using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Expansion.Builtin.Skills.Component;
using CabinIcarus.Joystick.Evetns;
using UnityEngine;

namespace Scripts.Skill
{
    public class CSkillsBehaveComponent : SkillBehaveComponent
    {
        public NoParEvent OnUse = new NoParEvent();

        public FloatParEvent OnSetCooling = new FloatParEvent();
        
        public float CoolingTime;

        [SerializeField]
        private string _behaveComKey = "behave";

        protected override void Init()
        {
            base.Init();
            
            OnSetCooling?.Invoke(CoolingTime);
        }

        void UseSkill()
        {
            OnUse?.Invoke();
        }

        public override void Use(Dictionary<string, object> data)
        {
            CurrentSkill.RootNode.Blackboard.Set(_behaveComKey,this);
            base.Use(data);
        }
    }
}