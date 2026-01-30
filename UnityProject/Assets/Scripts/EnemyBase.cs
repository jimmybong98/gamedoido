using UnityEngine;

namespace Gamedoido
{
    public class EnemyBase : MonoBehaviour
    {
        public string enemyName = "Enemy";
        public float maxHealth = 100f;
        public float attackDamage = 10f;
        public float attackCooldown = 2f;

        protected float currentHealth;

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

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
