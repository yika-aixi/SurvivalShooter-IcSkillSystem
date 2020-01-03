using System;
using UnityEngine;
using XNode;

namespace CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes
{
    [CreateNodeMenu("CabinIcarus/Nodes/System/Byte Value")]
    public partial class ByteValueNode:ValueNode
    {
        [SerializeField]
        private System.Byte _value;

        public override Type ValueType { get; } = typeof(System.Byte);
        
        protected override object GetOutValue()
        {
            return _value;
        }
    }
}