//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:15
//Assembly-CSharp

using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using Scripts.Buff;
using UnityEngine;
using Random = UnityEngine.Random;

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
            
            var buff = BuffFactory.GeBuff(GameManager.Manager.BuffManager,Random.Range(1, 13), out var color);

            var render = buffGo.GetComponent<Renderer>();

            render.material.color = color;

            if (buff is IBuffType type)
            {
                type.Type = Random.Range(1, 4);
            }
            
            if (buff is IBuffTimeDataComponent time)
            {
                time.Duration = Random.Range(1, 10.5f);
            }

            if (buff is IBuffValueDataComponent value)
            {
                if (buff is IMechanicBuff mechanicBuff)
                {
                    if (mechanicBuff.MechanicsType == MechanicsType.AttackSpeed)
                    {
                        value.Value = Random.Range(0.1f, 0.4f);
                        goto end;
                    }else if (mechanicBuff.MechanicsType == MechanicsType.MoveSpeed)
                    {
                        value.Value =  Random.Range(2, 5f);
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
        public static IBuffDataComponent GeBuff(IBuffManager manager,int id,out Color color)
        {
            color = Color.cyan;
            switch (id)
            {
                case 1:
                case 4:
                    color = Color.red;
                    var buff = manager.CreateBuff<MechanicsTime>();
                    buff.MechanicsType = MechanicsType.Attack;
                    return buff;
                case 2:
                case 5:
                    color = Color.magenta;
                    buff = manager.CreateBuff<MechanicsTime>();
                    buff.MechanicsType = MechanicsType.AttackSpeed;
                    return buff;
                case 3:
                case 6:
                    color = Color.green;
                    buff = manager.CreateBuff<MechanicsTime>();
                    buff.MechanicsType = MechanicsType.MoveSpeed;
                    return buff;
                case 7:
                    color = Color.yellow;
                    return manager.CreateBuff<LifestealFixed>();
                case 8:
                    color = Color.yellow;
                    return manager.CreateBuff<LifestealPercentage>();
                case 9:
                case 11:
                    color = Color.cyan;
                    return manager.CreateBuff<DamageReduceFixed>();
            }
            
            return manager.CreateBuff<DamageReducePercentage>();;
        }
    }
}