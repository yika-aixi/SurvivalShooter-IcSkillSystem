using System;
using UnityEngine;
using XNode;

namespace CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes
{
    [CreateNodeMenu("CabinIcarus/Nodes/System/Double Value")]
    public partial class DoubleValueNode:ValueNode
    {
        [SerializeField]
        private System.Double _value;

        public override Type ValueType { get; } = typeof(System.Double);
        
        protected override object GetOutValue()
        {
            return _value;
        }
    }
}