using UnityEngine;

namespace Animations
{
    public class SpriteController : MonoBehaviour
    {
        [SerializeField] private Renderer renderer;
        [SerializeField] private Animator animator;

        private MaterialPropertyBlock materialProperties;
        private const string triggerName = "ZoomInOut";

        private bool canCallTrigger = false;

        private void Start()
        {
            materialProperties = new MaterialPropertyBlock();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canCallTrigger = true;
            }
        }

        public void SetRandomColor()
        {
            materialProperties.SetColor(Tags.ShaderProperty.Color, Random.ColorHSV());
            renderer.SetPropertyBlock(materialProperties);
        }

        private void CallTrigger()
        {
            if (canCallTrigger)
            {
                canCallTrigger = false;
                animator.SetTrigger(triggerName);
            }
        }

    }
}