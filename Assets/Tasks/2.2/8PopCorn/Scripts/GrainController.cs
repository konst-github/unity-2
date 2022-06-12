using UnityEngine;

namespace PopCorn
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrainController : MonoBehaviour
    {
        private const float chance = 5; // 20%
        private const float cookedScale = 2;

        private Vector2 forceRange = new Vector2(20, 50);
        private int chanceMax = 100;
        private Vector2 cookingTimeRange = new Vector2(20, 60);

        private float cookingTimeCurrent = 0;

        private bool _isCooked = false;
        internal bool isCooked => _isCooked;

        private void Start()
        {
            cookingTimeCurrent = Random.Range(forceRange.x, forceRange.y);
        }

        private void Update()
        {
            if (!isCooked)
            {
                cookingTimeCurrent -= Time.deltaTime;
                CookIfReady();
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (!isCooked && collision.collider.CompareTag(Tags.grain))
            {
                GrainController grain = collision.collider.GetComponent<GrainController>();
                if(grain != null && grain.isCooked)
                {
                    CookIfLucky();
                }
            }
        }

        private void CookIfLucky()
        {
            bool isLucky = (Random.Range(0, chanceMax) % chance) == 0;
            if (isLucky)
            {
                Debug.Log("CookIfLucky: True");
                Cook();
            }
        }

        private void CookIfReady()
        {
            if (cookingTimeCurrent <= 0)
            {
                Debug.Log("CookIfReady: True");
                Cook();
            }
        }

        private void Cook()
        {
            _isCooked = true;
            transform.localScale *= cookedScale;
            GetComponent<Rigidbody>().AddForce(Random.insideUnitCircle * Random.Range(forceRange.x, forceRange.y));
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}