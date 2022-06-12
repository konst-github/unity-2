using UnityEngine;

public class BreakableCube : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody rigidBodyRef;

    private const float trapY = -10;

    private void Update()
    {
        if (transform.position.y < trapY)
        {
            transform.position = new Vector3(0, Mathf.Abs(trapY) * 2, 0);
        }
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void AddForce(Vector3 force)
    {
        rigidBodyRef.AddForce(force);
    }

    public void AddTorque(Vector3 torque)
    {
        rigidBodyRef.AddTorque(torque);
    }
}
