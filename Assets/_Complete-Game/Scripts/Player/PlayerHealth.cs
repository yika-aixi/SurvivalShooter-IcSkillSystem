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
    public class PlayerHealth : MonoBehaviour,IEntity,IBuffDestroySystem
    {
        public int startingHealth = 100;                            // The amount of health the player starts the game with.
        
        [field:SerializeField]
        public int currentHealth
        {
            get => (int) _buff.Value;
            set => _buff.Value = Mathf.Clamp(value,0,startingHealth);
        }

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


        #region Buff

        IMechanicBuff _buff;

        #endregion

        void Awake ()
        {
            // Setting up the references.
            anim = GetComponent <Animator> ();
            playerAudio = GetComponent <AudioSource> ();
            playerMovement = GetComponent <PlayerMovement> ();
            playerShooting = GetComponentInChildren <PlayerShooting> ();

            _buff = GameManager.Manager.BuffManager.CreateBuff<Mechanics>();
            _buff.MechanicsType = MechanicsType.Health;
            _buff.Value = startingHealth;

            
            GameManager.Manager.BuffManager.AddBuff(this,_buff);
            healthSlider.maxValue = startingHealth;
            healthSlider.value = startingHealth;
            GameManager.Manager.BuffManager.CreateAndAddBuff<Mechanics>(this, x =>
            {
                x.Value = startingHealth;
                x.MechanicsType = MechanicsType.Health;
            });
            
            GameManager.Manager.BuffManager.AddBuffSystem(this);

#if UNITY_EDITOR
            var link = gameObject.AddComponent < BuffEntityLinkComponent>();
            
            link.Init(GameManager.Manager.BuffManager,this);
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


        public void TakeDamage (IDamageBuff damage)
        {
            // Set the damaged flag so the screen will flash.
            damaged = true;

//            // Reduce the current health by the damage amount.
//            currentHealth -= amount;

            GameManager.Manager.BuffManager.AddBuff(this,damage);
            
            // Play the hurt sound effect.
            playerAudio.Play ();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if(currentHealth <= 0 || GameManager.Manager.BuffManager.HasBuff<Death>(this))
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

        public bool Filter(IEntity entity, IBuffDataComponent buff)
        {
            switch (buff)
            {
                case IDamageBuff damage:
                    if (entity as PlayerHealth != this)
                    {
                        if (damage.Maker != null && damage.Maker as PlayerHealth != this)
                        {
                            return false;
                        }                        
                    }
                    break;
            }

            return true;
        }

        public void Destroy(IEntity entity, IBuffDataComponent buff)
        {
            // Set the health bar's value to the current health.
            healthSlider.value = currentHealth;
        }
    }
}