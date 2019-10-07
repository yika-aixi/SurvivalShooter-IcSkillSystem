using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using Scripts.Buff;
using UnityEngine.AI;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
        Transform player;               // Reference to the player's position.
        PlayerHealth playerHealth;      // Reference to the player's health.
        EnemyHealth enemyHealth;        // Reference to this enemy's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
        private EnemyHealth _enemy;

        public float CurrentMoveSpeed;

        private float _baseMoveSpeed;
        private List<IMechanicBuff> _buffs;

        void Awake ()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent <EnemyHealth> ();
            nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
            _enemy = GetComponent<EnemyHealth>();
            _buffs = new List<IMechanicBuff>();
            _baseMoveSpeed = nav.speed;
        }


        void Update ()
        {
            _updateCurrentMoveSpeed();
            // If the enemy and the player have health left...
            if(GameManager.Manager.BuffManager.HasBuff<Mechanics>(enemyHealth,x=>x.MechanicsType == MechanicsType.Health) && 
               GameManager.Manager.BuffManager.HasBuff<Mechanics>(playerHealth,x=>x.MechanicsType == MechanicsType.Health))
            {
                // ... set the destination of the nav mesh agent to the player.
                nav.SetDestination (player.position);
            }
            // Otherwise...
            else
            {
                // ... disable the nav mesh agent.
                nav.enabled = false;
            }
        }

        private void _updateCurrentMoveSpeed()
        {
            CurrentMoveSpeed = _baseMoveSpeed;

            CurrentMoveSpeed += _enemy.GetBuffSumValue(_buffs,x => x.MechanicsType == MechanicsType.MoveSpeed);
            
            nav.speed = CurrentMoveSpeed;
        }
    }
}