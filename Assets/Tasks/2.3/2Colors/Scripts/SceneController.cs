using UnityEngine;

namespace ColorsTask
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private SphereController spherePrefab;

        [SerializeField] private Transform ground;

        [SerializeField]
        [Range(2, 4)]
        private float sphereDiameter = 2;

        private void Start()
        {
            float width = ground.localScale.x;
            float height = ground.localScale.z;

            int columns = Mathf.FloorToInt(width / sphereDiameter);
            int rows = Mathf.FloorToInt(height / sphereDiameter);

            float sphereRadius = sphereDiameter / 2;

            Debug.Log($"width {width}, height {height}");
            Debug.Log($"rows {rows}, columns {columns}");

            float initialX = (width - columns * sphereDiameter) / 2 + sphereRadius;
            Vector3 position = new Vector3(initialX, sphereRadius, (height - rows * sphereDiameter) / 2 + sphereRadius);

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    SphereController newSphere = Instantiate(spherePrefab, transform);
                    newSphere.SetPositionAndScale(position, Vector3.one * sphereDiameter);
                    position.x += sphereDiameter;
                }

                position.x = initialX;
                position.z += sphereDiameter;
            }
        }
    }
}