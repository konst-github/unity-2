using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void UpdateMaterial(Material material)
    {
        meshRenderer.material = material;
    }
}
