using UnityEngine;

namespace Gamedoido
{
    public class PlayerSurvival : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float maxStamina = 100f;
        public float maxHunger = 100f;
        public float maxThirst = 100f;

        public float health = 100f;
        public float stamina = 100f;
        public float hunger = 100f;
        public float thirst = 100f;

        public float hungerDrainPerSecond = 0.5f;
        public float thirstDrainPerSecond = 1f;
        public float staminaRegenPerSecond = 5f;

        private void Update()
        {
            hunger = Mathf.Max(0f, hunger - hungerDrainPerSecond * Time.deltaTime);
            thirst = Mathf.Max(0f, thirst - thirstDrainPerSecond * Time.deltaTime);

            float staminaRegenMultiplier = 1f;
            if (hunger <= maxHunger * 0.2f || thirst <= maxThirst * 0.2f)
            {
                staminaRegenMultiplier = 0.4f;
            }

            stamina = Mathf.Min(maxStamina, stamina + staminaRegenPerSecond * staminaRegenMultiplier * Time.deltaTime);
        }

        public void TakeDamage(float amount)
        {
            health = Mathf.Max(0f, health - amount);
        }

        public void RestoreHealth(float amount)
        {
            health = Mathf.Min(maxHealth, health + amount);
        }
    }
}
