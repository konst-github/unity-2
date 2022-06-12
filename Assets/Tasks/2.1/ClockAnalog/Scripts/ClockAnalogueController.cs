using System.Collections.Generic;
using UnityEngine;

public class ClockAnalogueController : MonoBehaviour
{
    protected struct SizeParams 
    {
        internal const float rotatoinStep = 0.1f;
        internal const float hoursArrowScale = 0.6f;
        internal const float minutesArrowScale = 0.8f;
        internal const float secondsArrowScale = 0.9f;
        internal const float hourMarksScale = 0.85f;

        internal const int clockFaceWeight = 2;

        internal const int arrowWeightMin = clockFaceWeight;
        internal const int arrowWeightMax = clockFaceWeight * 3;

        internal const int hourMarkBigWeightMin = clockFaceWeight;
        internal const int hourMarkBigWeightMax = arrowWeightMax;
        internal const int hourMarkSmallWeightMin = hourMarkBigWeightMin;
        internal const int hourMarkSmallWeightMax = hourMarkBigWeightMax;
    }

    [SerializeField] [Range(100, 600)] private int sphereDiameter = 200;

    [SerializeField] private GameObject arrowHour;
    [SerializeField] private int arrowHourWeight;

    [SerializeField] private GameObject arrowMinute;
    [SerializeField] private int arrowMinuteWeight;

    [SerializeField] private GameObject arrowSecond;
    [SerializeField] private int arrowSecondWeight;

    [SerializeField] private GameObject arrowsPin;
    [SerializeField] private int arrowsPinWeight;

    [SerializeField] private HourMarkController prefabHourMark;
    [SerializeField] private int hourMarkBigWeight;
    [SerializeField] private int hourMarkSmallWeight;

    private List<HourMarkController> hourMarkPrefabInstances = new List<HourMarkController>();

    private void Start()
    {
        // Sizes of all clock components are calculated in runtime based on the 'sphereDiameter' value provided in Unity editor.

        transform.localScale = new Vector3(sphereDiameter, sphereDiameter, SizeParams.clockFaceWeight);

        int weight = Mathf.Min(SizeParams.arrowWeightMax, Mathf.Max(arrowHourWeight, SizeParams.arrowWeightMin));
        arrowHour.transform.localScale   = new Vector3(weight, sphereDiameter * SizeParams.hoursArrowScale, weight);
        
        weight = Mathf.Min(SizeParams.arrowWeightMax, Mathf.Max(arrowMinuteWeight, SizeParams.arrowWeightMin));
        arrowMinute.transform.localScale = new Vector3(weight, sphereDiameter * SizeParams.minutesArrowScale, weight);

        weight = Mathf.Min(SizeParams.arrowWeightMax, Mathf.Max(arrowSecondWeight, SizeParams.arrowWeightMin));
        arrowSecond.transform.localScale = new Vector3(weight, sphereDiameter * SizeParams.secondsArrowScale, weight);

        weight = Mathf.Min(SizeParams.arrowWeightMax, Mathf.Max(arrowsPinWeight, SizeParams.arrowWeightMin));
        float doubleWeight = weight * 2;
        arrowsPin.transform.localScale = new Vector3(doubleWeight, doubleWeight, doubleWeight);

        int weightMarkBig = Mathf.Min(SizeParams.hourMarkBigWeightMax, Mathf.Max(hourMarkBigWeight, SizeParams.hourMarkBigWeightMin));
        int weightMarkSmall = Mathf.Min(SizeParams.hourMarkSmallWeightMax, Mathf.Max(hourMarkSmallWeight, SizeParams.hourMarkSmallWeightMin));

        for (int hour = 0; hour < Utils.Clock.Hrs_Half_Day; hour++)
        {
            bool isBigMark = (hour % 3 == 0);
            HourMarkController hourMark = Instantiate(prefabHourMark, Vector3.zero, Quaternion.identity);
            hourMarkPrefabInstances.Add(hourMark);

            weight = isBigMark ? weightMarkBig : weightMarkSmall;
            hourMark.UpdateScale(isBigMark, new Vector3(weight, sphereDiameter/2 * SizeParams.hourMarksScale, weight));

            float angleHour = hour * (Utils.Angles.Deg_360 / Utils.Clock.Hrs_Half_Day);
            hourMark.transform.rotation = new Quaternion().EulerZ(-angleHour);
        }
    }

    private void Update()
    {
        System.DateTime moment = System.DateTime.Now;

        // Hour's amgle should also be adjusted a bit to follow current minute value (f.e. for 13:30 minutes to be between 1 and 2 hour marks, but skipping it here)
        float angleHours = moment.Hour * Utils.Clock.Deg_per_Hr;
        arrowHour.transform.rotation = new Quaternion().EulerZ(-angleHours);

        // Same for minute arrow - angle should be adjusted to follow seconds arrow)
        float angleMinutes = moment.Minute * Utils.Clock.Deg_per_Min;
        arrowMinute.transform.rotation = new Quaternion().EulerZ(-angleMinutes);

        float angleSeconds = moment.Second * Utils.Clock.Deg_per_Sec;
        arrowSecond.transform.rotation = new Quaternion().EulerZ(-angleSeconds);

        Vector3 rotation = new Vector3(transform.rotation.x, transform.rotation.y + SizeParams.rotatoinStep, transform.rotation.z);
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }
}