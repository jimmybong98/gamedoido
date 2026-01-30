using System.Collections.Generic;
using UnityEngine;

namespace Gamedoido
{
    public class QuickStartSceneBuilder : MonoBehaviour
    {
        public bool autoBuildOnPlay = true;
        public bool spawnEnemies = false;
        public int crateCount = 6;
        public Vector2 worldSize = new Vector2(60f, 60f);
        public float crateSpacing = 8f;

        private bool _built;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureQuickStartScene()
        {
            if (FindObjectOfType<QuickStartSceneBuilder>() != null)
            {
                return;
            }

            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                return;
            }

            GameObject builderObject = new GameObject("QuickStart Scene Builder");
            QuickStartSceneBuilder builder = builderObject.AddComponent<QuickStartSceneBuilder>();
            builder.BuildScene();
        }

        private void Start()
        {
            if (autoBuildOnPlay && !_built)
            {
                BuildScene();
            }
        }

        public void BuildScene()
        {
            if (_built)
            {
                return;
            }

            _built = true;
            CreateLighting();
            CreateGround();

            GameObject player = CreatePlayer();
            CreateCrates(player.transform.position);

            if (spawnEnemies)
            {
                CreateEnemyPlaceholders();
            }
        }

        private void CreateLighting()
        {
            if (FindObjectOfType<Light>() != null)
            {
                return;
            }

            GameObject lightObject = new GameObject("Directional Light");
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.1f;
            lightObject.transform.rotation = Quaternion.Euler(45f, 30f, 0f);
        }

        private void CreateGround()
        {
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.position = Vector3.zero;
            ground.transform.localScale = new Vector3(worldSize.x / 10f, 1f, worldSize.y / 10f);
        }

        private GameObject CreatePlayer()
        {
            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player";
            player.tag = "Player";
            player.transform.position = new Vector3(0f, 1.1f, 0f);

            CharacterController controller = player.AddComponent<CharacterController>();
            controller.height = 1.9f;
            controller.radius = 0.35f;
            controller.center = new Vector3(0f, 0.95f, 0f);

            player.AddComponent<PlayerSurvival>();
            player.AddComponent<Inventory>();
            player.AddComponent<UpgradeSystem>();
            player.AddComponent<NoiseEmitter>();
            player.AddComponent<BasicPlayerController>();
            player.AddComponent<PlayerInteraction>();
            player.AddComponent<SimpleHUD>();

            GameObject cameraObject = new GameObject("Player Camera");
            cameraObject.transform.SetParent(player.transform, false);
            cameraObject.transform.localPosition = new Vector3(0f, 1.6f, 0f);
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.nearClipPlane = 0.05f;
            cameraObject.AddComponent<AudioListener>();

            return player;
        }

        private void CreateCrates(Vector3 origin)
        {
            LootTable lootTable = CreateSampleLootTable();
            List<Vector3> positions = CreateScatterPositions(origin);

            foreach (Vector3 position in positions)
            {
                GameObject crate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                crate.name = "Loot Crate";
                crate.transform.position = position;
                crate.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                LootCrate lootCrate = crate.AddComponent<LootCrate>();
                lootCrate.lootTable = lootTable;
                lootCrate.rolls = 2;
            }
        }

        private List<Vector3> CreateScatterPositions(Vector3 origin)
        {
            List<Vector3> positions = new List<Vector3>();
            int rows = Mathf.CeilToInt(Mathf.Sqrt(crateCount));
            int cols = Mathf.CeilToInt(crateCount / (float)rows);
            int index = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (index >= crateCount)
                    {
                        break;
                    }

                    float x = (col - cols / 2f) * crateSpacing;
                    float z = (row - rows / 2f) * crateSpacing + 10f;
                    Vector3 position = origin + new Vector3(x, 0.6f, z);
                    positions.Add(position);
                    index++;
                }
            }

            return positions;
        }

        private LootTable CreateSampleLootTable()
        {
            LootTable lootTable = ScriptableObject.CreateInstance<LootTable>();
            lootTable.entries = new List<LootTable.LootEntry>
            {
                new LootTable.LootEntry { item = CreateItem("Iron Sword", ItemType.Weapon, ItemTier.Common), weight = 40 },
                new LootTable.LootEntry { item = CreateItem("Leather Armor", ItemType.Armor, ItemTier.Uncommon), weight = 30 },
                new LootTable.LootEntry { item = CreateItem("Healing Herb", ItemType.Consumable, ItemTier.Common), weight = 20 },
                new LootTable.LootEntry { item = CreateItem("Ancient Relic", ItemType.Material, ItemTier.Rare), weight = 10 }
            };
            return lootTable;
        }

        private ItemDefinition CreateItem(string name, ItemType type, ItemTier tier)
        {
            ItemDefinition item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.itemName = name;
            item.itemType = type;
            item.tier = tier;
            item.description = "Quick-start placeholder item.";
            return item;
        }

        private void CreateEnemyPlaceholders()
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                enemy.name = "Enemy Placeholder";
                enemy.transform.position = new Vector3(8f * (i + 1), 1f, 18f);
                enemy.AddComponent<EnemyAI>();
                enemy.AddComponent<HumanEnemy>();
                enemy.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }
        }
    }
}
