using UnityEngine;

namespace Gamedoido
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float interactDistance = 3f;
        public KeyCode interactKey = KeyCode.E;

        private Camera _camera;
        private Inventory _inventory;

        private void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
            _inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(interactKey))
            {
                TryInteract();
            }
        }

        private void TryInteract()
        {
            if (_camera == null)
            {
                return;
            }

            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                LootCrate crate = hit.collider.GetComponentInParent<LootCrate>();
                if (crate != null)
                {
                    crate.OpenCrate(_inventory);
                }
            }
        }
    }
}
