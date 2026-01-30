using UnityEngine;

namespace Gamedoido
{
    public class HumanEnemy : EnemyBase
    {
        public float aggressionRange = 15f;

        protected override void Awake()
        {
            base.Awake();
            enemyName = "Bandit";
        }
    }
}
