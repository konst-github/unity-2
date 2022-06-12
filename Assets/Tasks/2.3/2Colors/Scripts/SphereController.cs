using UnityEngine;

namespace ColorsTask
{
    public class SphereController : MonoBehaviour
    {
        [SerializeField] private ColorSetMode colorSetMode;

        private void Start()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            switch(colorSetMode)
            {
                case ColorSetMode.PropertyBlock:
                    SetColorByPropertyBlock(meshRenderer);
                    break;
                case ColorSetMode.Copying:
                    SetColorByCopying(meshRenderer);
                    break;
                default: break;
            }
        }

        private void SetColorByPropertyBlock(MeshRenderer meshRenderer)
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            materialPropertyBlock.SetColor(Shader.PropertyToID(Tags.ShaderProperty.Color), Random.ColorHSV());
            materialPropertyBlock.SetFloat(Shader.PropertyToID(Tags.ShaderProperty.Metallic), Random.value);
            materialPropertyBlock.SetFloat(Shader.PropertyToID(Tags.ShaderProperty.Glossiness), Random.value);
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }

        private void SetColorByCopying(MeshRenderer meshRenderer)
        {
            Material material = new Material(meshRenderer.material);
            material.color = Random.ColorHSV();
            meshRenderer.material = material;
        }

        public void SetPositionAndScale(Vector3 position, Vector3 scale)
        {
            transform.position = position;
            transform.localScale = scale;
        }

        enum ColorSetMode
        {
            PropertyBlock,
            Copying
        }
    }
}