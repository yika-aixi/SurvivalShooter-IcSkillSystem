using CabinIcarus.IcSkillSystem.Expansion.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs;
using CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Buffs.Unity;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Components;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Entitys;
using CabinIcarus.IcSkillSystem.Runtime.Buffs.Systems.Interfaces;
using Scripts.Buff;
using UnityEngine;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour,IEntity,IBuffCreateSystem<IBuffDataComponent>
    {
        public int startingHealth = 100;            // The amount of health the enemy starts the game with.
        public int _cuu;
        [field:SerializeField]
        public int currentHealth
        {
            get => (int) _buff.Value;
            set => _buff.Value = Mathf.Clamp(value,0,startingHealth);
        }
        // The current health the enemy has.
        public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
        public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
        public AudioClip deathClip;                 // The sound to play when the enemy dies.


        Animator anim;                              // Reference to the animator.
        AudioSource enemyAudio;                     // Reference to the audio source.
        ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        bool isSinking;                             // Whether the enemy has started sinking through the floor.

        #region Buff

        private IMechanicBuff _buff;

        #endregion

        void Awake ()
        {
            GameManager.Manager.BuffManager.AddBuffSystem(this);
            
            // Setting up the references.
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
            _buff = GameManager.Manager.BuffManager.CreateBuff<Mechanics>();
            _buff.MechanicsType = MechanicsType.Health;
            _buff.Value = startingHealth;
            
            GameManager.Manager.BuffManager.AddBuff(this,_buff);
            GameManager.Manager.BuffManager.CreateAndAddBuff<Mechanics>(this, x =>
                {
                    x.MechanicsType = MechanicsType.Health;
                    x.Value = startingHealth;
                });
            
//            // Setting the current health when the enemy first spawns.
//            currentHealth = startingHealth;
            
#if UNITY_EDITOR
            var link = gameObject.AddComponent <BuffEntityLinkComponent>();
            
            link.Init(GameManager.Manager.BuffManager,this);
#endif
        }

        void Update ()
        {
            _cuu = currentHealth;
            // If the enemy should be sinking...
            if(isSinking)
            {
                // ... move the enemy down by the sinkSpeed per second.
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }


        public void TakeDamage(IDamageBuff damage, Vector3 hitPoint)
        {
            // If the enemy is dead...
            if (GameManager.Manager.BuffManager.HasBuff<Death>(this))
            {
                // ... no need to take damage so exit the function.
                return;
            }

            // Play the hurt sound effect.
            enemyAudio.Play ();
            
            GameManager.Manager.BuffManager.AddBuff(this,damage);

//            // Reduce the current health by the amount of damage sustained.
//            currentHealth -= amount;
            
            // Set the position of the particle system to where the hit was sustained.
            hitParticles.transform.position = hitPoint;

            // And play the particles.
            hitParticles.Play();
        }


        void Death ()
        {
            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            // Tell the animator that the enemy is dead.
            anim.SetTrigger ("Dead");

            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }


        public void StartSinking ()
        {
            // Find and disable the Nav Mesh Agent.
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;

            // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            GetComponent <Rigidbody> ().isKinematic = true;

            // The enemy should no sink.
            isSinking = true;

            // Increase the score by the enemy's score value.
            ScoreManager.score += scoreValue;

            // After 2 seconds destory the enemy.
            Destroy (gameObject, 2f);
        }

        public bool Filter(IEntity entity, IBuffDataComponent buff)
        {
            var eEntity = entity as EnemyHealth;
            
            return buff is Death && eEntity != null;
        }

        public void Create(IEntity entity, IBuffDataComponent buff)
        {
            ((EnemyHealth)entity).Death();
        }
    }
}