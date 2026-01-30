using UnityEngine;

namespace Gamedoido
{
    [RequireComponent(typeof(EnemyAI))]
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

    public class EnemyBase : MonoBehaviour
    {
        public string enemyName = "Enemy";
        public float maxHealth = 100f;
        public float attackDamage = 10f;
        public float attackCooldown = 2f;

        protected float currentHealth;
        private float _lastAttackTime;

        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;
        public float HealthPercent => maxHealth <= 0f ? 0f : currentHealth / maxHealth;


        protected virtual void Awake()
        {
            currentHealth = maxHealth;
        }

        public virtual void TakeDamage(float amount)
        {
            currentHealth = Mathf.Max(0f, currentHealth - amount);
            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        public bool TryAttack(GameObject target)
        {
            if (Time.time - _lastAttackTime < attackCooldown)
            {
                return false;
            }

            _lastAttackTime = Time.time;
            PerformAttack(target);
            return true;
        }

        protected virtual void PerformAttack(GameObject target)
        {
            if (target == null)
            {
                return;
            }

            PlayerSurvival player = target.GetComponent<PlayerSurvival>();
            if (player != null)
            {
                player.TakeDamage(attackDamage);
            }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
