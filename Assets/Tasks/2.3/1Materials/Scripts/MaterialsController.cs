using System.Collections;
using UnityEngine;

namespace Materials
{
    public class MaterialsController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer sphere1Renderer;
        [SerializeField] private MeshRenderer sphere2Renderer;

        [SerializeField] private Material materialRed;

        [SerializeField] private Color colorBlue;
        [SerializeField] private Color colorRed;
        [SerializeField] private Color colorGreen;
        [SerializeField] private Color colorPurple;

        private const string MaterialUpdateCoroutineName = "UpdateMaterial";
        private const float colorChangeTimeout = 2f;

        private UpdateStep step = UpdateStep.First;

        private void Start()
        {
            StartCoroutine(MaterialUpdateCoroutineName);
        }

        private IEnumerator UpdateMaterial()
        {
            switch(step)
            {
                case UpdateStep.First:
                    SetInitialMaterial();
                    step = UpdateStep.Second;
                    break;
                case UpdateStep.Second:
                    ChangeSharedMaterial();
                    step = UpdateStep.Next;
                    break;
                case UpdateStep.Next:
                    ChangeMaterial();
                    break;
                default: break;
            }

            yield return new WaitForSeconds(colorChangeTimeout);
        }

        private void SetInitialMaterial()
        {
            sphere1Renderer.material = materialRed;
            sphere2Renderer.material = materialRed;
        }

        private void ChangeSharedMaterial()
        {
            sphere1Renderer.sharedMaterial.color = Utils.Random.Bool() ? colorBlue : colorPurple;
        }

        private void ChangeMaterial()
        {
            // Yes, behaviour is different - only one sphere changes color now
            sphere1Renderer.material.color = Utils.Random.Bool() ? colorRed : colorGreen;
        }

        private enum UpdateStep
        {
            First,
            Second,
            Next
        }
    }
}