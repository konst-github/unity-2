using UnityEngine;

namespace PopCorn
{
    public class PotController : MonoBehaviour
    {
        [SerializeField]
        [Range(10, 5000)]
        private int numberOfGrainsToCook = 1000;

        [SerializeField]
        private GrainController grainPrefab;

        private RangeInt positionRange = new RangeInt(10, 25);

        private void Start()
        {
            GenerateGrains();
        }

        private void Update()
        {
 
        }

        private void GenerateGrains()
        {
            for (int index = 0; index < numberOfGrainsToCook; index++)
            {
                GrainController newGrain = Instantiate(grainPrefab, transform);
                newGrain.SetPosition(Random.insideUnitSphere * Random.Range(positionRange.start, positionRange.end));
            }
        }
    }
}