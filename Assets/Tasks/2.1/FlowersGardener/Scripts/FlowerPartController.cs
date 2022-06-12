using System.Collections.Generic;
using UnityEngine;

public class FlowerPartController : MonoBehaviour
{
    private const float scaleYMin = 0.5f;
    private const float scaleYMax = 1.5f;

    [SerializeField]
    [Range(scaleYMin, scaleYMax)]
    private float scaleY = 1.0f;

    [SerializeField]
    protected bool negativeXRotation = false;

    [SerializeField] private bool randomizeMaterialsOnStart = true;
    [SerializeField] private bool randomizeAnglesOnStart = true;
    [SerializeField] private bool randomizeScaleOnStart = true;

    [SerializeField] private MeshRenderer meshRenderer;

    // Filling out list of materials from the Inspector allows support of any amount of materials for random selection,
    // without need to have property per each material.
    [SerializeField] private List<Material> materials = new List<Material>();

    protected virtual void Start()
    {
        if (randomizeMaterialsOnStart) RandomizeMaterials();
        if (randomizeAnglesOnStart) RandomizeRotation();
        if (randomizeScaleOnStart) RandomizeScale();
    }

    public void RandomizeMaterials()
    {
        meshRenderer.material = RandomMaterial();
    }

    private Material RandomMaterial()
    {
        int materialIndex = Random.Range(0, materials.Count);
        return materials[materialIndex];
    }

    private void RandomizeScale()
    {
        float randomDiff = Random.Range(scaleYMin, scaleY);
        transform.localScale = new Vector3(1, randomDiff, 1);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    protected virtual void RandomizeRotation() {}

    protected void RandomizeRotation(Vector3 rangeMin, Vector3 rangeMax)
    {
        float randomDiffX = Random.Range(rangeMin.x, rangeMax.x) * (negativeXRotation ? -1 : 1);
        float randomDiffY = Random.Range(rangeMin.y, rangeMax.y) * Utils.Random.Sign();
        float randomDiffZ = Random.Range(rangeMin.z, rangeMax.z) * Utils.Random.Sign();
        transform.rotation = Quaternion.Euler(randomDiffX, randomDiffY, randomDiffZ);
    }
}
