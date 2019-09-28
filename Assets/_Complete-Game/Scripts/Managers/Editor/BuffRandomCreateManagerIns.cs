using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using Scripts.Buff;
using UnityEditor;
using UnityEngine;

namespace CompleteProject
{
    [CustomEditor(typeof(BuffRandomCreateManager))]
    public class BuffRandomCreateManagerIns : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical("box");
            {
                EditorGUILayout.ColorField("Attack Buff", Color.red);
                EditorGUILayout.ColorField("Attack Speed Buff", Color.magenta);
                EditorGUILayout.ColorField("Move Speed Buff", Color.green);
                EditorGUILayout.ColorField("Lifesteal Buff", Color.yellow);
                EditorGUILayout.ColorField("Damage Reduce Buff", Color.cyan);
            }
            EditorGUILayout.EndVertical();
        }
    }
}