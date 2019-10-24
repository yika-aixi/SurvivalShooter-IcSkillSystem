using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs.Unity;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems.Interfaces;
using Scripts.Buff;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class PlayerHealth : MonoBehaviour,IBuffDestroySystem<IcSkSEntity,Damage>
    {
        public int startingHealth = 100;                            // The amount of health the player starts the game with.

        public int CurrentHealth { get; private set; }

        public Slider healthSlider;                                 // Reference to the UI's health bar.
        public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
        public AudioClip deathClip;                                 // The audio clip to play when the player dies.
        public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

        Animator anim;                                              // Reference to the Animator component.
        AudioSource playerAudio;                                    // Reference to the AudioSource component.
        PlayerMovement playerMovement;                              // Reference to the player's movement.
        PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
        bool damaged;                                               // True when the player gets damaged.

        public IcSkSEntity Entity { get; private set; }

        #region Buff

        public int HealthBuffIndex { get; } = 0;
        
        public int MaxHealthBuffIndex { get; } = 1;

        public int MoveSpeedBuffIndex { get; } = 2;

        #endregion

        void Awake ()
        {
            // Setting up the references.
            anim = GetComponent <Animator> ();
            playerAudio = GetComponent <AudioSource> ();
            playerMovement = GetComponent <PlayerMovement> ();
            playerShooting = GetComponentInChildren <PlayerShooting> ();

            Entity = GameManager.Manager.EntityManager.CreateEntityAndBind(gameObject,gameObject.GetInstanceID());
            
            GameManager.Manager.BuffManager.AddBuff<Mechanics>(Entity,new Mechanics()
            {
                Value = startingHealth,
                MechanicsType = MechanicsType.Health
            });
            
            GameManager.Manager.BuffManager.AddBuff<Mechanics>(Entity,new Mechanics()
            {
                Value = startingHealth,
                MechanicsType = MechanicsType.MoveSpeed
            });

            GameManager.Manager.BuffManager.AddBuff<Mechanics>(Entity,new Mechanics()
            {
                Value = playerMovement.speed,
                MechanicsType = MechanicsType.Health
            });

            healthSlider.maxValue = startingHealth;
            healthSlider.value = startingHealth;
            CurrentHealth = startingHealth;
            
            GameManager.Manager.BuffManager.AddBuffSystem<Damage>(this);

#if UNITY_EDITOR
            var link = gameObject.AddComponent < BuffEntityLinkComponent>();
            
            link.Init(GameManager.Manager.EntityManager,Entity);
#endif
        }


        void Update ()
        {
            // If the player has just been damaged...
            if(damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;
        }


        public void TakeDamage (Damage damage)
        {
            // Set the damaged flag so the screen will flash.
            damaged = true;

//            // Reduce the current health by the damage amount.
//            currentHealth -= amount;

            GameManager.Manager.BuffManager.AddBuff(Entity,damage);
            
            // Play the hurt sound effect.
            playerAudio.Play ();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if(GameManager.Manager.BuffManager.HasBuff(Entity,new DeathStruct()))
            {
                // ... it should die.
                Death ();
            }
        }


        void Death ()
        {
            // Turn off any remaining shooting effects.
            playerShooting.DisableEffects ();

            // Tell the animator that the player is dead.
            anim.SetTrigger ("Die");

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.clip = deathClip;
            playerAudio.Play ();

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }


        public void RestartLevel ()
        {
            // Reload the level that is currently loaded.
            SceneManager.LoadScene (0);
        }

        public void Destroy(IcSkSEntity entity, int index)
        {
            var damage = GameManager.Manager.BuffManager.GetBuffData<Damage>(entity, index);

            if (damage.Entity != Entity)
            {
                int health = (int) GameManager.Manager.BuffManager.GetBuffData<Mechanics>(Entity,HealthBuffIndex).Value;
                healthSlider.value = health;
                CurrentHealth = health;
            }
        }
    }
}