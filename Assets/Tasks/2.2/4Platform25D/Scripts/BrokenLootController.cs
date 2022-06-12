using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody))]
    public class BrokenLootController : MonoBehaviour
    {
        private float trapY = -5;

        private void Update()
        {
            if(transform.position.y <= trapY)
            {
                Destroy(gameObject);
            }
        }

        public void AddForce(Vector3 force)
        {
            GetComponent<Rigidbody>().AddForce(force);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }
    }
}