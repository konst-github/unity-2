using UnityEngine;

namespace FlappyBird
{
    [RequireComponent(typeof(Rigidbody))]
    public class JumpingBallController : MonoBehaviour
    {
        private Rigidbody rigidBody;

        private bool canJump = true;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (canJump && Input.GetButtonDown(Tags.buttonsGroupJump))
            {
                rigidBody.AddForce(Vector3.up * 120);
                rigidBody.velocity = Vector3.zero;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(canJump && collision.gameObject.tag.Equals(Tags.cube))
            {
                canJump = false;
            }
        }
    }
}