using UnityEngine;

namespace Particles
{
    public class FireworksController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem psFireworks;

        private const float forceMultiplier = 50;

        private void Start()
        {
            psFireworks.transform.position = Vector3.forward * forceMultiplier;
        }
    }
}