using System;
using UnityEngine;
using XNode;

namespace CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes
{
    [CreateNodeMenu("CabinIcarus/Nodes/System/SByte Value")]
    public partial class SByteValueNode:ValueNode
    {
        [SerializeField]
        private System.SByte _value;

        public override Type ValueType { get; } = typeof(System.SByte);
        
        protected override object GetOutValue()
        {
            return _value;
        }
    }
}