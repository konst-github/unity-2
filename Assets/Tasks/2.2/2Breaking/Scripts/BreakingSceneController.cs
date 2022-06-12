using System.Collections.Generic;
using UnityEngine;

public class BreakingSceneController : MonoBehaviour
{
    [SerializeField]
    [Range(10f, 20f)]
    protected float initialSize = 10f;

    [SerializeField] 
    private BreakableCube cubePrefab;

    private const float minScaleX = 0.1f;
    private const int forceMin = 100;
    private const int forceMax = 300;

    protected List<BreakableCube> cubes = new List<BreakableCube>();

    private RaycastHit hit;

    private void Start()
    {
        InstantiateNewCube(new Vector3(0, (initialSize / 2) + Vector3.one.y, 0), new Vector3(initialSize, initialSize, initialSize), Vector3.zero);
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TestCubeHit();
        }
    }

    protected void TestCubeHit()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform.CompareTag(Tags.cube))
        {
            BreakableCube cube = hit.transform.gameObject.GetComponent<BreakableCube>();

            Vector3 cubeScale = cube.transform.localScale;
            if (cubeScale.x <= minScaleX)
            {
                return;
            }
            cubeScale /= 2f;
            cube.transform.localScale = cubeScale;

            Vector3 position = cube.transform.position;
            Vector3 forceDirection = new Vector3(Utils.Random.Sign(), 1, Utils.Random.Sign());
            Vector3 newForce = new Vector3(forceDirection.x * Random.Range(forceMin, forceMax), Random.Range(forceMin, forceMax), forceDirection.z * Random.Range(forceMin, forceMax));
            Vector3 positionDelta = new Vector3(forceDirection.x * (cubeScale.x + Vector3.one.x), 0, forceDirection.z * (cubeScale.z + Vector3.one.z));

            InstantiateNewCube(position + positionDelta, cubeScale, newForce);
            InstantiateNewCube(position - positionDelta, cubeScale, Vector3.Scale(newForce, new Vector3(-1, 1, -1)));
        }
    }

    private void InstantiateNewCube(Vector3 position, Vector3 scale, Vector3 force)
    {
        BreakableCube newCube = Instantiate(cubePrefab, position, Quaternion.identity);
        newCube.SetScale(scale);
        newCube.AddForce(force);
        cubes.Add(newCube);
    }
}
