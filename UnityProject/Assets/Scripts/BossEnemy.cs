using UnityEngine;

namespace Gamedoido
{
    public class BossEnemy : EnemyBase
    {
        public LootTable bossLoot;
        public float phaseTwoThreshold = 0.66f;
        public float phaseThreeThreshold = 0.33f;

        public float telegraphDuration = 1.2f;
        public float telegraphedAttackDamage = 60f;
        public float telegraphedAttackRange = 3f;
        public float areaAttackRadius = 5f;
        public float areaAttackDamage = 35f;

        public GameObject summonPrefab;
        public int summonCount = 2;
        public float summonCooldown = 12f;

        private int _phase = 1;
        private float _nextSummonTime;

        protected override void Awake()
        {
            base.Awake();
            enemyName = "Boss";
            maxHealth = 500f;
            attackDamage = 40f;
            attackCooldown = 3f;
        }

        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
            UpdatePhase();
        }

        protected override void PerformAttack(GameObject target)
        {
            if (target == null)
            {
                return;
            }

            if (_phase >= 3)
            {
                if (TrySummon())
                {
                    return;
                }
            }

            if (_phase >= 2)
            {
                PerformAreaAttack();
            }

            StartCoroutine(TelegraphedStrike(target.transform));
        }

        private void UpdatePhase()
        {
            if (HealthPercent <= phaseThreeThreshold)
            {
                _phase = 3;
            }
            else if (HealthPercent <= phaseTwoThreshold)
            {
                _phase = 2;
            }
            else
            {
                _phase = 1;
            }
        }

        private System.Collections.IEnumerator TelegraphedStrike(Transform target)
        {
            yield return new WaitForSeconds(telegraphDuration);

            if (target == null)
            {
                yield break;
            }

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= telegraphedAttackRange)
            {
                PlayerSurvival player = target.GetComponent<PlayerSurvival>();
                if (player != null)
                {
                    player.TakeDamage(telegraphedAttackDamage);
                }
            }
        }

        private void PerformAreaAttack()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, areaAttackRadius);
            foreach (Collider hit in hits)
            {
                PlayerSurvival player = hit.GetComponent<PlayerSurvival>();
                if (player != null)
                {
                    player.TakeDamage(areaAttackDamage);
                }
            }
        }

        private bool TrySummon()
        {
            if (summonPrefab == null || Time.time < _nextSummonTime)
            {
                return false;
            }

            _nextSummonTime = Time.time + summonCooldown;
            for (int i = 0; i < summonCount; i++)
            {
                Vector3 offset = Random.insideUnitSphere * 2f;
                offset.y = 0f;
                Instantiate(summonPrefab, transform.position + offset, Quaternion.identity);
            }

            return true;
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
