using CabinIcarus.IcSkillSystem.xNode_Group;

namespace Scripts.Skill
{
    public class EntitySkillsComponent : UnityEngine.MonoBehaviour
    {
        public IcSkillGroup[] Skills;

        public void UseSkill(int index)
        {
            Skills[index].Start().Start();
        }
    }
}