using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        
        [SerializeField] 
        private JumpMethod jumpMethod = JumpMethod.Transform;

        private const float trapLevel = -5;
        private const int restoreYScale = 2;

        private const int scaleFactor = 2;

        [SerializeField]
        [Range(10, 60)]
        private float transformPerSecond = 45f;

        [SerializeField]
        [Range(2, 10)]
        private float moveSpeed = 5f;

        private float jumpDuration;

        private void Update()
        {
            CheckForJump();

            if (jumpMethod == JumpMethod.Transform && jumpDuration > 0)
            {
                float jumpDelta = transformPerSecond * Time.deltaTime;
                jumpDuration -= Time.deltaTime;
                transform.position += (Vector3.up * jumpDelta);
            }


            CheckForMove();

            CheckForTrapLevel();
        }

        private void CheckForJump()
        {
            if (Input.GetButtonDown(Tags.buttonsGroupJump))
            {
                switch (jumpMethod)
                {
                    case JumpMethod.Transform: // Change position
                        jumpDuration = 1;
                        break;
                    case JumpMethod.AddForce: // Add force
                        rigidBody.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
                        break;
                    case JumpMethod.Velocity:
                        rigidBody.velocity += Vector2.up * moveSpeed;
                        break;
                }
            }
        }

        private void CheckForMove()
        {
            rigidBody.AddForce(Vector2.right * Input.GetAxis(Tags.axisHorizontal) * scaleFactor);
        }

        private void CheckForTrapLevel()
        {
            if (transform.position.y <= trapLevel)
            {
                transform.position = new Vector3(0, Mathf.Abs(trapLevel * restoreYScale), 0);
            }
        }

        private enum JumpMethod : int
        {
            Transform,
            AddForce,
            Velocity
        }
    }
}