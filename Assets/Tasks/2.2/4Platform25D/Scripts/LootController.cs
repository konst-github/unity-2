using UnityEngine;

namespace Platformer
{
    public class LootController : MonoBehaviour
    {
        [SerializeField] 
        private BrokenLootController brokenLootPrefab;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Break();
        }

        private void Break()
        {
            float forceValue = 200f;
            float doubleForceValue = forceValue * 2;

            Vector3 scale = transform.localScale / 2;
            Vector3 force = new Vector3(forceValue, doubleForceValue, forceValue);

            float xRight = transform.position.x + scale.x;
            float xLeft = transform.position.x - scale.x;
            float y = transform.position.y + transform.localScale.y / 4;
            float zRight = transform.position.z + scale.z;
            float zLeft = transform.position.z - scale.z;

            InstantiateNewCube(new Vector3(xRight, y, zRight), scale, force);
            InstantiateNewCube(new Vector3(xLeft, y, zRight), scale, Vector3.Scale(force, new Vector3(-1, 1, 1)));
            InstantiateNewCube(new Vector3(xRight, y, zLeft), scale, Vector3.Scale(force, new Vector3(1, 1, -1)));
            InstantiateNewCube(new Vector3(xLeft, y, zLeft), scale, Vector3.Scale(force, new Vector3(-1, 1, -1)));

            Destroy(gameObject);
        }

        private void InstantiateNewCube(Vector3 position, Vector3 scale, Vector3 force)
        {
            BrokenLootController newCube = Instantiate(brokenLootPrefab, position, Quaternion.identity);
            newCube.SetScale(scale);
            newCube.AddForce(force);
        }
    }
}