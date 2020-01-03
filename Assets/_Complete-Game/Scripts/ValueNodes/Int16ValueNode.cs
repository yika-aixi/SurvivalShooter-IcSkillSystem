using System;
using UnityEngine;
using XNode;

namespace CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes
{
    [CreateNodeMenu("CabinIcarus/Nodes/System/Int16 Value")]
    public partial class Int16ValueNode:ValueNode
    {
        [SerializeField]
        private System.Int16 _value;

        public override Type ValueType { get; } = typeof(System.Int16);
        
        protected override object GetOutValue()
        {
            return _value;
        }
    }
}