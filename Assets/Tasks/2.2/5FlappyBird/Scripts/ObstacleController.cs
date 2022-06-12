using UnityEngine;

namespace FlappyBird
{
    public class ObstacleController : MonoBehaviour
    {
        private const float trapX = -10f;
        private const float gameSpeed = 2;

        private void Update()
        {
            float positionDelta = gameSpeed * Time.deltaTime;
            transform.position += (Vector3.left * positionDelta);

            if(transform.position.x <= trapX)
            {
                Destroy(gameObject);
            }
        }

        public void SetPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public void SetScale(Vector3 newScale)
        {
            transform.localScale = newScale;
        }
    }
}