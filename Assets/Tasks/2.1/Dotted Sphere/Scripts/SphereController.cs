using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private const float timeIntervalMaterialChange = 1f;

    [SerializeField] 
    [Range(100, 250)] 
    private float sphereSize = 250;

    [SerializeField]
    [Range(10, 50)]
    [Tooltip("Latitudes count also includes the poles.")]
    private int latitudesCount = 10;  // Horizontal

    [SerializeField]
    [Range(10, 50)]
    [Tooltip("Count of cubes per each slice.")]
    private int longitudesCount = 10; // Vertical

    [SerializeField] 
    private CubeController cubePrefab;

    [SerializeField] 
    [Range(0.1f, 10f)]
    [Tooltip("Angle in degrees to rotate sphere during each frame update.")]
    private float rotationAnglePerFrame = 0.1f;

    private List<CubeController> cubes = new List<CubeController>();

    // This is just to play with the sphere's appearance.
    private float timeInterval = 0;
    [SerializeField] private List<Material> materials = new List<Material>();

    private void Start()
    {
        transform.localScale = Vector3.one * sphereSize;
        GenerateCubes();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationAnglePerFrame, 0));
        RandomizeMaterial();
    }

    private void GenerateCubes()
    {
        float anglePerLatitude = Utils.Angles.Deg_Half_Circle / latitudesCount;
        float anglePerLongitude = Utils.Angles.Deg_Circle / longitudesCount;

        float sphereRadius = sphereSize / 2;

        for (int latitude = 0; latitude <= latitudesCount; latitude++)
        {
            float angleLatFinal = anglePerLatitude * latitude;
            float angleLatRad = Utils.Angles.DegreesToRadians(angleLatFinal);

            float y = sphereRadius * Mathf.Cos(angleLatRad);

            float sliceRadius = sphereRadius * Mathf.Sin(angleLatRad);

            for (int longitude = 0; longitude < longitudesCount; longitude++)
            {
                float angleLonFinal = anglePerLongitude * longitude;
                float angleLonRad = Utils.Angles.DegreesToRadians(angleLonFinal);

                float x = sliceRadius * Mathf.Cos(angleLonRad);
                float z = sliceRadius * Mathf.Sin(angleLonRad);

                Vector3 position = new Vector3(x, y, z);

                CubeController cube = Instantiate(cubePrefab, position, Quaternion.identity);

                // Apply instantiated cube to sphere as a child, so by rotating sphere we'll rotate all cubes at once
                cube.transform.parent = transform;
                cube.transform.rotation = Quaternion.Euler(0, -angleLonFinal, -angleLatFinal);
                cubes.Add(cube);

                if (latitude == 0 || latitude == latitudesCount)
                {
                    // North and South Poles should have just 1 cube
                    break;
                }
            }
        }
    }

    private void RandomizeMaterial() 
    {
        timeInterval += Time.deltaTime;

        if (timeInterval >= timeIntervalMaterialChange)
        {
            timeInterval = 0;
            Material randomMaterial = RandomMaterial();
            foreach (CubeController cube in cubes)
            {
                cube.UpdateMaterial(randomMaterial);
            }
        }
    }
    
    private Material RandomMaterial()
    {
        int materialIndex = Random.Range(0, materials.Count);
        return materials[materialIndex];
    }
}
