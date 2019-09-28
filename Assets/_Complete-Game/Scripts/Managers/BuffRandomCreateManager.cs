//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:15
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using Scripts.Buff;
using UnityEngine;

namespace CompleteProject
{
    public class BuffRandomCreateManager:MonoBehaviour
    {
        public GameObject GameObject;
        public float spawnTime = 2;
        public Transform[] Points;
        
        void Start ()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating ("Spawn", spawnTime, spawnTime);
        }


        void Spawn ()
        {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, Points.Length);

            var parent = Points[spawnPointIndex];

            if (parent.childCount> 0)
            {
                Destroy(parent.GetChild(0).gameObject);
            }
            
            var buffGo = Instantiate (GameObject, parent);
            buffGo.transform.position += Vector3.up;
            var buffC = buffGo.GetComponent<BuffComponent>();
            
            var buff = BuffFactory.GetBuff(Random.Range(1, 11),out var color);

            var render = buffGo.GetComponent<Renderer>();

            render.sharedMaterial.color = color;

            if (buff is IBuffValueDataComponent value)
            {
                if (buff is IMechanicBuff mechanicBuff)
                {
                    if (mechanicBuff.MechanicsType == MechanicsType.AttackSpeed)
                    {
                        value.Value = Random.Range(0.1f, 0.4f);
                        goto end;
                    }
                }
                
                value.Value = Random.Range(10, 20);
            }
            
            end:
            
            buffC.Buff = buff;
        }
    }
    
    public static class BuffFactory
    {
        public static IBuffDataComponent GetBuff(int id,out Color color)
        {
            IBuffDataComponent buff = null;
            color = Color.white;
            switch (id)
            {
                case 1:
                    color = Color.red;
                    buff = new MechanicsTime(MechanicsType.Attack);
                    break;
                case 2:
                    color = Color.magenta;
                    buff = new MechanicsTime(MechanicsType.AttackSpeed);
                    break;
                case 3:
                    color = Color.green;
                    buff = new MechanicsTime(MechanicsType.MoveSpeed);
                    break;
                case 4:
                    color = Color.red;
                    buff = new MechanicsPercentageTime(MechanicsType.Attack);
                    break;
                case 5:
                    color = Color.magenta;
                    buff = new MechanicsPercentageTime(MechanicsType.AttackSpeed);
                    break;
                case 6:
                    color = Color.green;
                    buff = new MechanicsPercentageTime(MechanicsType.MoveSpeed);
                    break;
                case 7:
                    color = Color.yellow;
                    buff = new LifestealFixed();
                    break;
                case 8:
                    color = Color.yellow;
                    buff = new LifestealPercentage();
                    break;
                case 9:
                    color = Color.cyan;
                    buff = new DamageReduceFixed();
                    break;
                case 10:
                    color = Color.cyan;
                    buff = new DamageReducePercentage();
                    break;
            }

            return buff;
        }
    }
}