using UnityEngine;

namespace Animations
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private const string animationStateKey = "AnimationStep";
        private const int maxState = 4;
        private const int initialState = 1;
        private int state = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState();
            }
        }

        private void ChangeState()
        {
            state += 1;
            if (state > maxState)
            {
                state = initialState;
            }
            animator.SetInteger(Animator.StringToHash(animationStateKey), state);
        }
    }
}