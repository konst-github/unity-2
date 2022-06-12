using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObstacleController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;

    private float forceMin = 100;
    [SerializeField] private Vector3 forceRange;

    [SerializeField]
    [Range(-10, -20)]
    private float trapLevel = -20;

    private const int restoreYScale = 2;

    private void Update()
    {
        if (transform.position.y <= trapLevel)
        {
            transform.position = new Vector3(0, Mathf.Abs(trapLevel * restoreYScale), 0);
            ResetVelocity();
        }
    }

    private void OnMouseDown()
    {
        Vector3 force = Vector3.one * forceMin + new Vector3(Random.Range(0, forceRange.x), Random.Range(0, forceRange.y), Random.Range(0, forceRange.z));
        rigidBody.AddForce(force);
        rigidBody.AddTorque(force);
    }

    private void ResetVelocity()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
