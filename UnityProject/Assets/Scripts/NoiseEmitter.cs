using UnityEngine;

namespace Gamedoido
{
    public class NoiseEmitter : MonoBehaviour
    {
        public float footstepLoudness = 6f;
        public float weaponLoudness = 12f;

        public void EmitFootstep()
        {
            NoiseSystem.EmitNoise(transform.position, footstepLoudness, gameObject);
        }

        public void EmitWeaponNoise()
        {
            NoiseSystem.EmitNoise(transform.position, weaponLoudness, gameObject);
        }
    }
}
