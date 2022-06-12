using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private DigitController digitControllerTens;
    [SerializeField] private DigitController digitControllerOnes;

    public void SetUnitValue(int newValue) 
    {
        digitControllerTens.SetDigitValue(newValue / 10);
        digitControllerOnes.SetDigitValue(newValue % 10);
    }
}
