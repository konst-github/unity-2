using UnityEngine;

public class HourMarkController : MonoBehaviour
{
    private const float cylinderScaleYBig = 0.05f;
    private const float cylinderScaleYSmall = 0.025f;
    private const float scaleXZ = 1f;
    private const float positionXZ = 0f;
    private const float positionYSmall = 0.93f;
    private const float positionYBig = 0.88f;
    private const float positionYStep = 0.05f;

    [SerializeField] private GameObject sphereTop;
    [SerializeField] private GameObject cylinder;
    [SerializeField] private GameObject sphereBottom;

    public void UpdateScale(bool isBigMark, Vector3 scale)
    {
        float minScale = Mathf.Min(scale.x, Mathf.Min(scale.y, scale.z));
        sphereTop.transform.localScale = new Vector3(scaleXZ, minScale / scale.y, scaleXZ);
        sphereBottom.transform.localScale = new Vector3(scaleXZ, minScale / scale.y, scaleXZ);

        sphereTop.transform.position = new Vector3(positionXZ, (positionYSmall + positionYStep), positionXZ);
        sphereBottom.transform.position = new Vector3(positionXZ, isBigMark ? positionYBig : positionYSmall, positionXZ);

        cylinder.transform.localScale = new Vector3(scaleXZ, isBigMark ? cylinderScaleYBig : cylinderScaleYSmall, scaleXZ);
        cylinder.transform.position = new Vector3(positionXZ, (isBigMark ? (positionYBig + positionYStep) : (positionYSmall + positionYStep/2)), positionXZ);

        transform.localScale = scale;
    }
}
