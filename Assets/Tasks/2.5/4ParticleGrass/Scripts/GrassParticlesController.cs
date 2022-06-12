using UnityEngine;

namespace Particles
{
    public class GrassParticlesController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particlesSystemPrefab;

        [SerializeField]
        [Range(5, 10)]
        private float particlesY = 5;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    Transform objecthit = hit.transform;
                    if (hit.transform.CompareTag(Tags.ground))
                    {
                        AddParticleSystem(hit.point);
                    }
                }
            }
        }

        private void AddParticleSystem(Vector3 point)
        {
            ParticleSystem ps = Instantiate(particlesSystemPrefab);
            point.y = particlesY;
            ps.transform.position = point;
            ps.Play();
        }
    }
}