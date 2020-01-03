using System;
using UnityEngine;
using XNode;

namespace CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes
{
    [CreateNodeMenu("CabinIcarus/Nodes/System/UInt32 Value")]
    public partial class UInt32ValueNode:ValueNode
    {
        [SerializeField]
        private System.UInt32 _value;

        public override Type ValueType { get; } = typeof(System.UInt32);
        
        protected override object GetOutValue()
        {
            return _value;
        }
    }
}