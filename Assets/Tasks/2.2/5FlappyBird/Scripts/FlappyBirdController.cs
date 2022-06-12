using UnityEngine;

namespace FlappyBird
{
    public class FlappyBirdController : MonoBehaviour
    {
        [SerializeField]
        private ObstacleController obstaclePrefab;

        [SerializeField]
        [Range(obstaclesTimeoutMin + 0.25f, 5.0f)]
        private float obstaclesTimeoutMax = 5f;

        private const float obstaclesTimeoutMin = 1.5f;

        private const float battleFieldWidth = 22f;
        private const float battleFieldHeight = 14f;
        private const float battleFieldHalfHeight = battleFieldHeight/2;

        private const float maxElevation = 1f;
        private Vector2 obstaclesElevation = new Vector2(maxElevation * 1.5f, maxElevation * 3);

        private Vector2 rangeX = new Vector2(0, battleFieldWidth);
        private Vector2 rangeY = new Vector2(0, battleFieldHeight);

        private float currentGateCentre = 0;

        private float timer = 0;
        
        private bool isTop = true;

        private void Start()
        {
            GenerateInitialGate();
            RelaunchTimer();
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                GenerateObstacle();
                RelaunchTimer();
            }
        }

        private void RelaunchTimer()
        {
            timer = Random.Range(obstaclesTimeoutMin, obstaclesTimeoutMax);
        }

        private void GenerateInitialGate()
        {
            currentGateCentre = battleFieldHalfHeight;
            float height = battleFieldHalfHeight - obstaclesElevation.y / 2;
            Vector3 scale = new Vector3(1, height, 1);
            InstantiateObstacle(new Vector3(rangeX.y, rangeY.y - height / 2, 0), scale);
            InstantiateObstacle(new Vector3(rangeX.y, rangeY.x + height / 2, 0), scale);
        }

        private void GenerateObstacle()
        {
            currentGateCentre += Utils.Random.Sign() * Random.Range(0, maxElevation);
            // Normlize gate centre coordinate
            currentGateCentre = Mathf.Max(rangeY.x + maxElevation, currentGateCentre);
            currentGateCentre = Mathf.Min(rangeY.y - maxElevation, currentGateCentre);

            float gateHeight = Random.Range(obstaclesElevation.x, obstaclesElevation.y);
            float topHeight = battleFieldHeight - currentGateCentre - gateHeight / 2;
            float bottomHeight = currentGateCentre - gateHeight / 2;
            
            Vector3 position = new Vector3(rangeX.y, isTop ? (currentGateCentre + gateHeight/2 + topHeight/2) : (currentGateCentre - gateHeight/2 - bottomHeight/2), 0);
            Vector3 scale = new Vector3(1, isTop ? topHeight : bottomHeight, 1);
            InstantiateObstacle(position, scale);
            
            isTop = !isTop;
        }

        private void InstantiateObstacle(Vector3 position, Vector3 scale)
        {
            ObstacleController obstacle = Instantiate(obstaclePrefab, transform);
            obstacle.SetPosition(position);
            obstacle.SetScale(scale);
        }
    }
}