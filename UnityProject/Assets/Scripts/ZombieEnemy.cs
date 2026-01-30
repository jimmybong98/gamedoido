using UnityEngine;

namespace Gamedoido
{
    public class ZombieEnemy : EnemyBase
    {
        public float infectionChance = 0.25f;

        protected override void Awake()
        {
            base.Awake();
            enemyName = "Zombie";
            maxHealth = 140f;
            attackDamage = 15f;
        }
    }
}
