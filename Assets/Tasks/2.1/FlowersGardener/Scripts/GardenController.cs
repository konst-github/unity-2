using System.Collections.Generic;
using UnityEngine;

public class GardenController : MonoBehaviour
{
    [SerializeField] private GameObject gardenGround;

    [SerializeField] private FlowerController flowerPrefab;

    
    [SerializeField] 
    [Tooltip("If TRUE - new flower is placed anywhere in the garden.\n" +
        "If FALSE - new flower is placed at mouse click point.")]
    private bool plantFlowerRandomly = true;

    private List<FlowerController> flowers = new List<FlowerController>();

    private float flowerPlacementRange = 25f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MOUSE IS DOWN - plantFlowerRandomly " + plantFlowerRandomly);
            if (plantFlowerRandomly)
            {
                AddFlower(GetRandomVector(flowerPlacementRange, true));
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    Transform objecthit = hit.transform;
                    Debug.Log("MOUSE IS DOWN - RAY CHECK " + hit.transform.gameObject.tag);
                    if (hit.transform.CompareTag(Tags.ground))
                    {
                        Debug.Log("MOUSE IS DOWN - RAY HIT " + hit.point.ToString());
                        AddFlower(hit.point);
                    }
                }
            }
        }
    }

    private void AddFlower(Vector3 position)
    {
        FlowerController flower = Instantiate(flowerPrefab, position, Quaternion.identity);
        flowers.Add(flower);
    }

    private Vector3 GetRandomVector(float maxRange, bool ignoreY)
    {
        Vector3 vector = Random.insideUnitSphere * maxRange;
        vector.y = ignoreY ? 0 : vector.y;
        return vector;
    }
}
