using System.Collections.Generic;
using UnityEngine;

namespace Conveyor
{
    public class ConveyorController : MonoBehaviour
    {
        [SerializeField]
        private GameObject elementPrefab;

        [SerializeField]
        [Range(20, 40)]
        private int elementsInConveyor = 20;

        [SerializeField]
        private GameObject sphere;

        private List<GameObject> elements = new List<GameObject>();

        private const float stepPerSecond = 2.5f;
        private float currentStep = 0;

        private const float spaceBetweenCubes = 1.25f;
        private Vector2 cubesOffset = new Vector2(spaceBetweenCubes, 0.3f);
        
        private float ballOffset = spaceBetweenCubes * 4;
        private const float ballTrapY = -20;

        private void Start()
        {
            InstantiateConveyor();
            PositionBall();
        }

        private void Update()
        {
            currentStep = (Time.time * stepPerSecond);

            foreach (GameObject element in elements)
            {                
                float posY = Mathf.Sin(element.transform.position.x + currentStep) - cubesOffset.y * elements.IndexOf(element);
                element.transform.position = new Vector3(element.transform.position.x, posY, 0);
            }

            if(sphere.transform.position.y < ballTrapY)
            {
                PositionBall();
            }
        }

        private void InstantiateConveyor()
        {
            float posX = -ConveyorHalfWidth();
            for (int index = 0; index < elementsInConveyor; index++)
            {
                GameObject element = Instantiate(elementPrefab, transform);
                element.transform.position = Vector3.right * posX;
                posX += cubesOffset.x;
                elements.Add(element);
            }
        }

        private void PositionBall()
        {
            float posX = ConveyorHalfWidth() - ballOffset;
            sphere.transform.position = new Vector3(posX, cubesOffset.x, 0);
        }

        private float ConveyorHalfWidth()
        {
            return (cubesOffset.x * elementsInConveyor) / 2;
        }
    }
}