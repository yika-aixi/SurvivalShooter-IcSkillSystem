using System;
using UnityEngine;
using XNode;

namespace CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes
{
    [CreateNodeMenu("CabinIcarus/Nodes/System/UInt64 Value")]
    public partial class UInt64ValueNode:ValueNode
    {
        [SerializeField]
        private System.UInt64 _value;

        public override Type ValueType { get; } = typeof(System.UInt64);
        
        protected override object GetOutValue()
        {
            return _value;
        }
    }
}