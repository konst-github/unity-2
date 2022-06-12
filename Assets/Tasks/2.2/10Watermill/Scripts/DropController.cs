using UnityEngine;


namespace Watermill
{
    [RequireComponent(typeof(Rigidbody))]
    public class DropController : MonoBehaviour
    {
        [SerializeField]
        [Range(10, 5000)]
        private float forceZ = 500;

        private const float trapLevelY = -40;

        private void Start()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * forceZ);
        }

        private void Update()
        {
            if (transform.position.y <= trapLevelY)
            {
                Destroy(gameObject);
            }
        }
    }
}