using UnityEngine;

namespace Gamedoido
{
    public class SimpleHUD : MonoBehaviour
    {
        public PlayerSurvival player;
        public Inventory inventory;

        private void Awake()
        {
            if (player == null)
            {
                player = GetComponent<PlayerSurvival>();
            }

            if (inventory == null)
            {
                inventory = GetComponent<Inventory>();
            }
        }

        private void OnGUI()
        {
            if (player == null)
            {
                return;
            }

            GUI.Box(new Rect(12, 12, 260, 130), "Survival Stats");
            GUI.Label(new Rect(22, 40, 240, 20), $"Health: {player.health:0}/{player.maxHealth:0}");
            GUI.Label(new Rect(22, 60, 240, 20), $"Stamina: {player.stamina:0}/{player.maxStamina:0}");
            GUI.Label(new Rect(22, 80, 240, 20), $"Hunger: {player.hunger:0}/{player.maxHunger:0}");
            GUI.Label(new Rect(22, 100, 240, 20), $"Thirst: {player.thirst:0}/{player.maxThirst:0}");
            int itemCount = inventory != null ? inventory.items.Count : 0;
            GUI.Label(new Rect(22, 120, 240, 20), $"Inventory Items: {itemCount}");

            GUI.Box(new Rect(12, 152, 360, 70), "Quick Controls");
            GUI.Label(new Rect(22, 178, 340, 20), "WASD move, Shift sprint, Space jump");
            GUI.Label(new Rect(22, 198, 340, 20), "Mouse look, E interact, Esc toggle cursor");
        }
    }
}
