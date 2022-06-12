using UnityEngine;

namespace Particles
{
    public class BubblesController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem bubblesParticleSystem;
        
        private Camera cam;
        private const float positionZ = 50;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = positionZ;
            Vector3 worldPoint = cam.ScreenToWorldPoint(mousePos);
            bubblesParticleSystem.transform.position = worldPoint;
        }
    }
}