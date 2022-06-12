using UnityEngine;

namespace BlendTree
{
    public class BlendTreeController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private BlendMode blendMode = BlendMode.Tree1D;

        private const string KeyBlend = "BlendValue";
        private const string KeyBlendX = "BlendX";
        private const string KeyBlendY = "BlendY";

        private const int ValueUp = 1;
        private const int ValueDown = 0;

        private void Update()
        {
            switch(blendMode)
            {
                case BlendMode.Tree1D:
                    UpdateBlendTree1D();
                    break;
                case BlendMode.Tree2D:
                    UpdateBlendTree2D();
                    break;
                default: break;
            }
        }

        private void UpdateBlendTree1D()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("KeyCode.UpArrow");
                animator.SetFloat(KeyBlend, ValueUp);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("KeyCode.DownArrow");
                animator.SetFloat(KeyBlend, ValueDown);
            }
        }

        private void UpdateBlendTree2D()
        {
            float deltaTime = Time.deltaTime;
            float horzVal = Input.GetAxis(Tags.axisHorizontal);
            float blendValX = animator.GetFloat(KeyBlendX) + horzVal * deltaTime;
            blendValX = Mathf.Clamp(blendValX, -1, 1);
            animator.SetFloat(KeyBlendX, blendValX);

            float vertVal = Input.GetAxis(Tags.axisVertical);
            float blendValY = animator.GetFloat(KeyBlendY) + vertVal * deltaTime;
            blendValY = Mathf.Clamp(blendValY, -1, 1);
            animator.SetFloat(KeyBlendY, blendValY);
        }

        enum BlendMode
        {
            Tree1D,
            Tree2D
        }
    }
}