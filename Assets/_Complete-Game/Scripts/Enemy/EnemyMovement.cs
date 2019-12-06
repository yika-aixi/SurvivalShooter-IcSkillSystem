using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using Scripts.Buff;
using UnityEngine.AI;
using UnityEngine.Profiling;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
        Transform player;               // Reference to the player's position.
        PlayerHealth playerHealth;      // Reference to the player's health.
        EnemyHealth enemyHealth;        // Reference to this enemy's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.

        private List<IMechanicBuff> _buffs;

        void Awake ()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent <EnemyHealth> ();
            nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
            nav.speed = 3;
            _buffs = new List<IMechanicBuff>();
        }


        void Update ()
        {
            Profiler.BeginSample("Enemy Update");
            _updateCurrentMoveSpeed();
            // If the enemy and the player have health left...
            if( enemyHealth.startingHealth > 0 && 
                playerHealth.CurrentHealth > 0)
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
            Profiler.EndSample();
        }

        private void _updateCurrentMoveSpeed()
        {
//            nav.speed = GameManager.Manager.BuffManager.GetBuffData<Mechanics>(enemyHealth.Entity,enemyHealth.MoveSpeedBuffIndex).Value;
        }
    }
}