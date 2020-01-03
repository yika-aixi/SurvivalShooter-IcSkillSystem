using System;
using UnityEngine;
using XNode;

namespace CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes
{
    [CreateNodeMenu("CabinIcarus/Nodes/System/UInt16 Value")]
    public partial class UInt16ValueNode:ValueNode
    {
        [SerializeField]
        private System.UInt16 _value;

        public override Type ValueType { get; } = typeof(System.UInt16);
        
        protected override object GetOutValue()
        {
            return _value;
        }
    }
}