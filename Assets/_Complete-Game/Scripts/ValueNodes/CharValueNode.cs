using System;
using UnityEngine;
using XNode;

namespace CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes
{
    [CreateNodeMenu("CabinIcarus/Nodes/System/Char Value")]
    public partial class CharValueNode:ValueNode
    {
        [SerializeField]
        private System.Char _value;

        public override Type ValueType { get; } = typeof(System.Char);
        
        protected override object GetOutValue()
        {
            return _value;
        }
    }
}