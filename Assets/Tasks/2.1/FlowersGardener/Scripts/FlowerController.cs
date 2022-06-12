using UnityEngine;

public class FlowerController : MonoBehaviour
{
    [SerializeField] private FlowerPartController leftLeafController;
    [SerializeField] private FlowerPartController rightLeafController;
    [SerializeField] private StamenController leftStamenController;
    [SerializeField] private StamenController rightStamenController;

    private const float scaleDelta = 0.005f;
    private const float scaleMin = 0.5f;
    private const float scaleMinRange = 0.5f;
    private const float scaleMax = 5f;
    private const float scaleMaxRange = 5f;
    private const float scaleMaxToDestroy = scaleMax + scaleMaxRange;

    [SerializeField]
    [Tooltip("Flower scale range between 1 and 5.\nFinal scale will be randomly selected between 0.5 (minimum scale) and this value")]
    [Range(scaleMin + scaleMinRange, scaleMax)]
    private float scaleRange;

    private Vector3 scaleChange;

    private void Start()
    {
        RandomizeSubComponents();
        SetScale();
        scaleChange = new Vector3(scaleDelta, scaleDelta, scaleDelta);
    }
    
    private void Update() 
    {
        if(transform.localScale.x < scaleMaxToDestroy) 
        {
            transform.localScale += scaleChange;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void RandomizeSubComponents()
    {
        leftLeafController.SetActive(RandomIsActive());
        rightLeafController.SetActive(RandomIsActive());
        leftStamenController.SetActive(true); 
        rightStamenController.SetActive(RandomIsActive());
    }

    private bool RandomIsActive()
    {
        return (Random.Range(0, 100) % 2) == 0;
    }

    private void SetScale()
    {
        float finalScale = Random.Range(scaleMin, scaleRange);
        Debug.Log("finalScale = " + finalScale);
        transform.localScale = new Vector3(finalScale, finalScale, finalScale);
    }
}
