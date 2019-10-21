using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using Scripts.Buff;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class PlayerShooting : MonoBehaviour
    {
        public PlayerHealth Player;
        public int DamageType = 1;
        [Header("Base Attack Damage")]
        public int damagePerShot = 20;                  // The damage inflicted by each bullet.
        [Header("Base Attack Speed")]
        public float timeBetweenBullets = 0.15f;        // The time between each shot.

        public float currentdamagePerShot;
        public float currentBetweenBulletsTime;
        public float range = 100f;                      // The distance the gun can fire.


        float timer;                                    // A timer to determine when to fire.
        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        ParticleSystem gunParticles;                    // Reference to the particle system.
        LineRenderer gunLine;                           // Reference to the line renderer.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
		public Light faceLight;								// Duh
        float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
        private List<IMechanicBuff> _buff;
        private List<IMechanicBuff> _buffs;


        void Awake ()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask ("Shootable");

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem> ();
            gunLine = GetComponent <LineRenderer> ();
            gunAudio = GetComponent<AudioSource> ();
            gunLight = GetComponent<Light> ();
            _buff = new List<IMechanicBuff>();
            _buffs = new List<IMechanicBuff>();
			//faceLight = GetComponentInChildren<Light> ();
        }


        void Update ()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            _updateAttackAndAttackSpeed();

#if !MOBILE_INPUT
            // If the Fire1 button is being press and it's time to fire...
			if(Input.GetButton ("Fire1") && timer >= currentBetweenBulletsTime && Time.timeScale != 0)
            {
                // ... shoot the gun.
                Shoot ();
            }
#else
            // If there is input on the shoot direction stick and it's time to fire...
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= currentBetweenBulletsTime)
            {
                // ... shoot the gun
                Shoot();
            }
#endif
            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if(timer >= currentBetweenBulletsTime * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects ();
            }
        }

        private void _updateAttackAndAttackSpeed()
        {
            currentBetweenBulletsTime = timeBetweenBullets;
            currentdamagePerShot = damagePerShot;
            
            var buffs = GameManager.Manager.BuffManager.GetBuffs<Mechanics>(Player.Entity);

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

        public void DisableEffects ()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
			faceLight.enabled = false;
            gunLight.enabled = false;
        }


        void Shoot ()
        {
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play ();

            // Enable the lights.
            gunLight.enabled = true;
			faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop ();
            gunParticles.Play ();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition (0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

                // If the EnemyHealth component exist...
                if(enemyHealth != null)
                {
                    var damage = new Damage();
                    damage.Value = currentdamagePerShot;
                    damage.Entity = Player.Entity;
                    damage.Type = DamageType;
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage (damage, shootHit.point);
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition (1, shootHit.point);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}