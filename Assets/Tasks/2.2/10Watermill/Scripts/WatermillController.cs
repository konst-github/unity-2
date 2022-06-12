using UnityEngine;

namespace Watermill
{
    public class WatermillController : MonoBehaviour
    {
        [SerializeField]
        private DropController dropPrefab;

        private const float dropInterval = 0.1f;
        private float dropInstantiateTimeout = 0;

        private Vector3 initialDropPosition = new Vector3(12, 50, 0);

        private void Update()
        {
            dropInstantiateTimeout += Time.deltaTime;
            if (dropInstantiateTimeout >= dropInterval)
            {
                dropInstantiateTimeout = 0;
                DropController drop = Instantiate(dropPrefab, transform);
                drop.transform.position = initialDropPosition;
            }
        }
    }
}