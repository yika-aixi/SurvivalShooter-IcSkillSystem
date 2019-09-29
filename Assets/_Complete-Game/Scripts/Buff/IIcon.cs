using UnityEngine;

namespace Scripts.Buff
{
    public interface IIcon
    {
        Sprite Icon { get; set; }
        
        string IconName { get; set; }
    }
}