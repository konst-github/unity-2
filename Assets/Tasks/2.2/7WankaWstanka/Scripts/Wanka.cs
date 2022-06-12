using UnityEngine;

namespace Wanka
{
    [RequireComponent(typeof(Rigidbody))]
    public class Wanka : MonoBehaviour
    {
        private Rigidbody rigidBody;

        private const float forceMultiplier = 10f;
        private const float torqueMultiplier = 50f;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.centerOfMass = new Vector3(0, -0.5f, 0);
        }

        private void OnMouseDown()
        {
            rigidBody.AddForce(Random.insideUnitSphere * forceMultiplier);
            rigidBody.AddTorque(Random.insideUnitSphere * torqueMultiplier);
        }
    }
}