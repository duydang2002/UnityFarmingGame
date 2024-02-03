using DateTimeNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public RectTransform ClockFace;
    public TextMeshProUGUI Date, Time, Season, Week, Day;

    public Image weatherSprite;
    public Sprite[] weatherSprites;

    private float startingRotation;

    public Light2D sunlight;

    public float nightIntensity;
    public float dayIntensity;
    public AnimationCurve dayNightCurve;


    private void Awake()
    {
        // Lay vi tri dau cua z (xoay quanh truc z)
        startingRotation = ClockFace.localEulerAngles.z;
        Debug.Log(startingRotation);
    }


    // Khi Action o TimeManagerScript xay ra (O Phuong thuc AdvanceTime)
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
        // Xu li khi thoi gian chay
        Date.text = dateTime.DateToString();
        Time.text = dateTime.TimeToString();
        Season.text = dateTime.Season.ToString();
        Day.text = $"Days: {dateTime.TotalNumDays.ToString()}";
        Week.text = $"WK: {dateTime.CurrentWeek}";
         
        //weatherSprite.sprite = weatherSprites[(int)WeatherManager.currentWeather];

        // chia ra lam 24 lan thay doi do co 24 gio
        float t = (float)dateTime.Hour / 24f;
        
        // Noi suy vi tri moi sau khi thoi gian thay doi
        float newRotation = Mathf.Lerp(0, 360, t);
        ClockFace.localEulerAngles = new Vector3(0, 0, newRotation + startingRotation);
        float dayNightT = dayNightCurve.Evaluate(t);
        sunlight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, dayNightT);

    }

}
