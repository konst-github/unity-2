using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private UnitController unitControllerHours;
    [SerializeField] private UnitController unitControllerMinutes;
    [SerializeField] private UnitController unitControllerSeconds;

    private void Update()
    {
        System.DateTime moment = System.DateTime.Now;
        unitControllerHours.SetUnitValue(moment.Hour);
        unitControllerMinutes.SetUnitValue(moment.Minute);
        unitControllerSeconds.SetUnitValue(moment.Second);
    }
}
