using UnityEngine;

public class SegmentController : MonoBehaviour
{
    [SerializeField] private Material materialOff;
    [SerializeField] private Material materialOn;

    [SerializeField] private MeshRenderer segmentLeft;
    [SerializeField] private MeshRenderer segmentCenterTop;
    [SerializeField] private MeshRenderer segmentCenterBottom;
    [SerializeField] private MeshRenderer segmentRight;

    [SerializeField] [Range(0, 6)] public int bitIndex;

    public void SetIsOn(bool isOn) 
    {
        SetSegments(isOn ? materialOn : materialOff);
    }

    private void SetSegments(Material material) 
    {
        segmentLeft.material = material;
        segmentCenterTop.material = material;
        segmentCenterBottom.material = material;
        segmentRight.material = material;
    }
}
