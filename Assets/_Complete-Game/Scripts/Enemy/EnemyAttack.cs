using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using Scripts.Buff;

namespace CompleteProject
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
        public int attackDamage = 10;               // The amount of health taken away per attack.
        public int DamageType = 1;
        
        public float currentdamagePerShot;
        public float currentBetweenBulletsTime;

        Animator anim;                              // Reference to the animator component.
        GameObject player;                          // Reference to the player GameObject.
        PlayerHealth playerHealth;                  // Reference to the player's health.
        EnemyHealth enemyHealth;                    // Reference to this enemy's health.
        bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
        float timer;                                // Timer for counting up to the next attack.
        private List<IMechanicBuff> _buffs;
        
        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();
            _buffs = new List<IMechanicBuff>();
        }

        [SerializeField]
        private float fource;
        [SerializeField]
        private ForceMode mode;
        [ContextMenu("AddF")]
        void _addf()
        {
            var rigi = GetComponent<Rigidbody>();
            
            rigi.AddForce(fource * -(transform.forward),mode);
        }
        

        void OnTriggerEnter (Collider other)
        {
            // If the entering collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
            }
        }


        void OnTriggerExit (Collider other)
        {
            // If the exiting collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }


        void Update ()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

           // _updateAttackAndAttackSpeed();
            
            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.startingHealth > 0)
            {
                // ... attack.
                Attack ();
            }

            // If the player has zero or less health...
            if(playerHealth.CurrentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }
        }
        
        private void _updateAttackAndAttackSpeed()
        {
            currentBetweenBulletsTime = timeBetweenAttacks;
            currentdamagePerShot = attackDamage;

            var buffs = GameManager.Manager.BuffManager.GetBuffs<Mechanics>(enemyHealth.Entity);

            for (var i = 0; i < buffs.Count; i++)
            {
                var buff = buffs[i];

                if (buff.MechanicsType == MechanicsType.AttackSpeed)
                {
                    currentBetweenBulletsTime -= buff.Value;
                }

                if (buff.MechanicsType == MechanicsType.Attack)
                {
                    currentdamagePerShot += buff.Value;
                }

            }
        }

        void Attack ()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(playerHealth.CurrentHealth > 0)
            {
                var damageBuff = new Damage();
                damageBuff.Value = attackDamage;
                damageBuff.Entity = enemyHealth.Entity.FromIIcSkSEntity();
                damageBuff.Type = DamageType;
                playerHealth.TakeDamage(damageBuff);
            }
        }
    }
}