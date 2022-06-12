using UnityEngine;

public class StamenController : FlowerPartController
{
    private const float rotationXMin = 3;
    [SerializeField]
    [Range(rotationXMin + 1, 30)]
    private float rotationXMax = 4;

    private const float rotationYMin = 0;
    [SerializeField]
    [Range(rotationYMin + 1, 180)]
    private float rotationYMax;

    private const float rotationZMin = 0;
    [SerializeField]
    [Range(rotationZMin + 1, 30)]
    private float rotationZMax;

    [SerializeField] private FlowerPartController antherController;
    [SerializeField] private FlowerPartController filamentController;

    protected override void Start()
    {
        base.Start();
        antherController.RandomizeMaterials();
        filamentController.RandomizeMaterials();
    }

    protected override void RandomizeRotation()
    {
        Vector3 min = new Vector3(rotationXMin, rotationYMin, rotationZMin);
        Vector3 max = new Vector3(rotationXMax, rotationYMax, rotationZMax);
        RandomizeRotation(min, max);
    }
}
