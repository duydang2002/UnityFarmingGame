using DateTimeNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public RectTransform ClockFace;
    public TextMeshProUGUI Date, Time, Season, Week;

    public Image weatherSprite;
    public Sprite[] weatherSprites;

    private float startingRotation;

    public Light2D sunlight;

    public float nightIntensity;
    public float dayIntensity;
    public AnimationCurve dayNightCurve;


    private void Awake()
    {
        startingRotation = ClockFace.localEulerAngles.z;
        Debug.Log(startingRotation);
    }

    private void OnEnable()
    {
        TimeManagerScript.OnDateTimeChanged += UpdateDateTime;
    }

    private void OnDisable()
    {
        TimeManagerScript.OnDateTimeChanged -= UpdateDateTime;
    }

    bool lerpUp = true;

    public void UpdateDateTime(DateTime dateTime)
    {
        //Debug.Log(dateTime.ToString());
        Date.text = dateTime.DateToString();
        Time.text = dateTime.TimeToString();
        Season.text = dateTime.Season.ToString();
        Week.text = $"WK: {dateTime.CurrentWeek}";
        //weatherSprite.sprite = weatherSprites[(int)WeatherManager.currentWeather];

        float t = (float)dateTime.Hour / 24f;

        float newRotation = Mathf.Lerp(0, 360, t);
        ClockFace.localEulerAngles = new Vector3(0, 0, newRotation + startingRotation);

        float dayNightT = dayNightCurve.Evaluate(t);

        sunlight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, dayNightT);
    }
   
}
