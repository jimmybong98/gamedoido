using System;
using UnityEngine;

namespace Gamedoido
{
    public static class NoiseSystem
    {
        public struct NoiseEvent
        {
            public Vector3 position;
            public float loudness;
            public GameObject source;
        }

        public static event Action<NoiseEvent> NoiseEmitted;

        public static void EmitNoise(Vector3 position, float loudness, GameObject source)
        {
            NoiseEmitted?.Invoke(new NoiseEvent
            {
                position = position,
                loudness = loudness,
                source = source
            });
        }
    }
}
