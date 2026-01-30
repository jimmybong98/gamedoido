using UnityEngine;

namespace Gamedoido
{
    public class BossEnemy : EnemyBase
    {
        public LootTable bossLoot;

        protected override void Awake()
        {
            base.Awake();
            enemyName = "Boss";
            maxHealth = 500f;
            attackDamage = 40f;
            attackCooldown = 3f;
        }

        protected override void Die()
        {
            if (bossLoot != null)
            {
                ItemDefinition reward = bossLoot.RollLoot();
                if (reward != null)
                {
                    Debug.Log($"Boss dropped: {reward.itemName}");
                }
            }

            base.Die();
        }
    }
}
