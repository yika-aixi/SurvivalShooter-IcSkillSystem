//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年09月28日-16:15
//Assembly-CSharp

using System.Collections.Generic;
using System.Linq;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems.Interfaces;
using Scripts.Buff;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CompleteProject
{
    public class BuffRandomCreateManager:MonoBehaviour//,IBuffUpdateSystem,IBuffCreateSystem<IcSkSEntity>,IBuffDestroySystem<IcSkSEntity>
    {
        class IconInfo
        {
            public IcSkSEntity Entity;
            public IBuffDataComponent Buff;
            public Image Icon;
        }
        public GameObject GameObject;
        public float spawnTime = 2;
        public Sprite[] Icons;
        public RectTransform Canvas;
        public Image IconTemplate;
        public Transform[] Points;

        private List<IconInfo> _iconImags = new List<IconInfo>();

        private Camera _mainCamera;
        
        void Start ()
        {
            _mainCamera = Camera.main;
//            GameManager.Manager.BuffManager.AddBuffSystem(this);
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
            
//            var buffGo = Instantiate (GameObject, parent);
//            buffGo.transform.position += Vector3.up;
//            var buffC = buffGo.GetComponent<BuffComponent>();
//            
//            var buff = BuffFactory.GeBuff(GameManager.Manager.BuffManager,Random.Range(1, 13), out var color);
//
//            var render = buffGo.GetComponent<Renderer>();
//
//            render.material.color = color;
//
//            if (buff is IIcon icon)
//            {
//                icon.Icon = Icons.First(x => x.name == icon.IconName);
//            }
//            
//            if (buff is IBuffType type)
//            {
//                type.Type = Random.Range(1, 4);
//            }
//            
//            if (buff is IBuffTimeDataComponent time)
//            {
//                time.Duration = Random.Range(1, 10.5f);
//            }
//
//            if (buff is IBuffValueDataComponent value)
//            {
//                if (buff is IMechanicBuff mechanicBuff)
//                {
//                    if (mechanicBuff.MechanicsType == MechanicsType.AttackSpeed)
//                    {
//                        value.Value = Random.Range(0.1f, 0.4f);
//                        goto end;
//                    }else if (mechanicBuff.MechanicsType == MechanicsType.MoveSpeed)
//                    {
//                        value.Value =  Random.Range(2, 5f);
//                        goto end;
//                    }
//                }
//
//                value.Value = Random.Range(10, 20);
//            }
//            
//            end:
//            
//            buffC.Buff = buff;
        }

        private List<IBuffDataComponent> _buffIcons = new List<IBuffDataComponent>();
        
//        public bool Filter(IEntity entity)
//        {
//            return GameManager.Manager.BuffManager.HasBuff<IBuffDataComponent>(entity,x=> x is IIcon);
//        }
//
//        public void Execute(IEntity entity)
//        {
//            GameManager.Manager.BuffManager.GetBuffs(entity,x=>x is IIcon,_buffIcons);
//            int i = 0;
//            var iconInfos = _iconImags.Where(x=>x.Entity == entity && x.Icon.enabled).ToList();
//
//            foreach (var buffIcon in iconInfos)
//            {
//                foreach (var buffDataComponent  in _buffIcons)
//                {
//                    if (buffDataComponent == buffIcon.Buff && buffIcon.Entity == entity)
//                    {
//                        if (entity == null)
//                        {
//                            return;
//                        }
//
//                        var x = i * buffIcon.Icon.rectTransform.sizeDelta.x;
//                        var entityPos = ((Component) entity).transform.position;
//                        
//                        Vector2 ViewportPosition=_mainCamera.WorldToViewportPoint(entityPos);
//                        Vector2 WorldObject_ScreenPosition=new Vector2(
//                            ((ViewportPosition.x*Canvas.sizeDelta.x)-(Canvas.sizeDelta.x*0.5f)),
//                            ((ViewportPosition.y*Canvas.sizeDelta.y)-(Canvas.sizeDelta.y*0.5f)));
//                        
//                        buffIcon.Icon.rectTransform.anchoredPosition = WorldObject_ScreenPosition + new Vector2(x,-1);
//                    }
//                }
//                ++i;
//            }
//        }
//
//        public bool Filter(IEntity entity, IBuffDataComponent buff)
//        {
//            return buff is IIcon;
//        }
//
//        public void Destroy(IEntity entity, IBuffDataComponent buff)
//        {
//            foreach (var iconImag in _iconImags)
//            {
//                if (iconImag.Entity == entity && iconImag.Buff == buff)
//                {
//                    iconImag.Icon.enabled = false;
//                }
//            }
//        }
//
//        public void Create(IEntity entity, IBuffDataComponent buff)
//        {
//            IconInfo icon = null;
//            IIcon iconBuff = (IIcon) buff;
//            foreach (var iconImag in _iconImags)
//            {
//                if (!iconImag.Icon.enabled)
//                {
//                    iconImag.Icon.enabled = true;
//                    iconImag.Icon.sprite = iconBuff.Icon;
//                    iconImag.Entity = entity;
//                    iconImag.Buff = buff;
//                    icon = iconImag;
//                    break;
//                }
//            }
//
//            if (icon == null)
//            {
//                var image = Instantiate(IconTemplate,Canvas,false);
//                image.sprite = iconBuff.Icon;
//                image.enabled = true;
//                icon = new IconInfo()
//                {
//                    Entity = entity,
//                    Buff = buff,
//                    Icon = image
//                };
//                _iconImags.Add(icon);
//            }
//        }
//
//        public void Execute()
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public void Create(IcSkSEntity entity, int index)
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public void Destroy(IcSkSEntity entity, int index)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//    
//    public static class BuffFactory
//    {
//        public static IBuffDataComponent GeBuff(IBuffManager<IBuffDataComponent> manager,int id,out Color color)
//        {
//            color = Color.cyan;
//            switch (id)
//            {
//                case 1:
//                case 4:
//                    color = Color.red;
//                    var buff = manager.CreateBuff<MechanicsTime>();
//                    buff.MechanicsType = MechanicsType.Attack;
//                    buff.IconName = "Attack";
//                    return buff;
//                case 2:
//                case 5:
//                    color = Color.magenta;
//                    buff = manager.CreateBuff<MechanicsTime>();
//                    buff.MechanicsType = MechanicsType.AttackSpeed;
//                    buff.IconName = "AttackSpeed";
//                    return buff;
//                case 3:
//                case 6:
//                    color = Color.green;
//                    buff = manager.CreateBuff<MechanicsTime>();
//                    buff.MechanicsType = MechanicsType.MoveSpeed;
//                    buff.IconName = "MoveSpeed";
//                    return buff;
//                case 7:
//                    color = Color.yellow;
//                    LifestealFixed lifestealFixed = manager.CreateBuff<LifestealFixed>();
//                    lifestealFixed.IconName = "LifestealFixed";
//                    return lifestealFixed;
//                case 8:
//                    color = Color.yellow;
//                    var lifestealPercentage = manager.CreateBuff<LifestealPercentage>();
//                    lifestealPercentage.IconName = "LifestealPercentage";
//                    return lifestealPercentage;
//                case 9:
//                case 11:
//                    color = Color.cyan;
//                    var damageReduceFixed = manager.CreateBuff<DamageReduceFixed>();
//                    damageReduceFixed.IconName = "DamageReduceFixed";
//                    return damageReduceFixed;
//            }
//            
//            var damageReducePercentage = manager.CreateBuff<DamageReducePercentage>();
//            damageReducePercentage.IconName = "DamageReducePercentage";
//            return damageReducePercentage;
//        }
    }
}