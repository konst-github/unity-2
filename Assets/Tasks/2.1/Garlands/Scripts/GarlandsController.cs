using System.Collections.Generic;
using UnityEngine;

public class GarlandsController : MonoBehaviour
{
    [SerializeField]
    [Range(10, 50)]
    private int columns = 30;

    [SerializeField]
    [Range(10, 50)]
    private int rows = 30;

    [SerializeField]
    [Range(0.05f, 0.5f)]
    private float marginBetweenCubes = 0.3f;

    [SerializeField]
    [Tooltip("When TRUE - will rotate cubes by X and Z axises, when FALSE - only by Y.")]
    private bool rotateByXZ = false;

    [SerializeField]
    private GameObject cubePrefab;

    private const float cubeSize = 1f;

    private List<GameObject> cubes = new List<GameObject>();

    private void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //RotateCubes(mouseWorldPosition);
        if (rotateByXZ)
        {
            RotateCubesByXZ(mouseWorldPosition);
        }
        else
        {
            RotateCubesByY(mouseWorldPosition);
        }
    }

    private void RotateCubesByY(Vector3 mouseWorldPosition)
    {
        int maxSteps = 10;
        float distanceMin = cubeSize * 1.5f;
        float distanceMax = distanceMin * maxSteps;
        float maxAngle = Utils.Angles.Deg_180;
        float angleDelta = maxAngle / (distanceMax - distanceMin);

        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                int index = column * rows + row;
                GameObject cube = cubes[index];
                Vector2 distance = cube.transform.position - mouseWorldPosition;
                float radiusFromMouse = Mathf.Abs(distance.magnitude);

                float angle;
                if (radiusFromMouse < distanceMin)
                {
                    angle = maxAngle;
                }
                else if (radiusFromMouse > distanceMax)
                {
                    angle = 0;
                }
                else
                {
                    angle = Mathf.Clamp(maxAngle - angleDelta * radiusFromMouse, 0, maxAngle);
                }

                cube.transform.rotation = Quaternion.Euler(Utils.Angles.Deg_90, angle, 0);
            }
        }
    }

    private void RotateCubesByXZ(Vector3 mouseWorldPosition)
    {
        float distanceScale = 0.65f;
        float distanceFull = FullDistance();
        float anglePerOneX = Utils.Angles.Deg_90 / (distanceFull * distanceScale);

        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                int index = column * rows + row;
                GameObject cube = cubes[index];
                Vector2 distance = cube.transform.position - mouseWorldPosition;
                float angleX = -anglePerOneX * distance.y - Utils.Angles.Deg_90;
                float angleZ = anglePerOneX * distance.x;
                cube.transform.rotation = Quaternion.Euler(angleX, 0, angleZ);
            }
        }
    }

    private void GenerateGrid()
    {
        float x = -(GridWidth() - cubeSize) / 2;
        float y = -(GridHeight() - cubeSize) / 2;

        float step = cubeSize + marginBetweenCubes;

        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                Vector3 position = new Vector3(x + column * step, y + row * step, 0);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.transform.rotation = new Quaternion().EulerX(Utils.Angles.Deg_90);
                cubes.Add(cube);
            }
        }
    }

    private float GridWidth()
    {
        return (cubeSize * columns) + (marginBetweenCubes * (columns - 1));
    }

    private float GridHeight()
    {
        return (cubeSize * rows) + (marginBetweenCubes * (rows - 1));
    }

    private float FullDistance()
    {
        return Mathf.Min(GridWidth(), GridHeight());
    }
}
