using UnityEngine;

public class LeafController : FlowerPartController
{
    private const float rotationXMin = 45;
    [SerializeField]
    [Range(rotationXMin + 1, 85)]
    private float rotationXMax = 4;

    private const float rotationYMin = 0;
    [SerializeField]
    [Range(rotationYMin + 1, 20)]
    private float rotationYMax;

    private const float rotationZMin = 0;
    [SerializeField]
    [Range(rotationZMin + 1, 10)]
    private float rotationZMax;

    protected override void RandomizeRotation()
    {
        Vector3 min = new Vector3(rotationXMin, rotationYMin, rotationZMin);
        Vector3 max = new Vector3(rotationXMax, rotationYMax, rotationZMax);
        RandomizeRotation(min, max);
    }
}
