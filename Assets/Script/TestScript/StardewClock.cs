using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StardewClock : MonoBehaviour
{
    TimeManager tm;
    
    public Image nightBackground;
    public RectTransform hand;

    const float hoursToDegrees = 180 / 24;

    // Start is called before the first frame update
    void Start()
    {
        tm = FindObjectOfType<TimeManager>();
        nightBackground.fillAmount = tm.nightDuration / 2;
    }

    // Update is called once per frame
    void Update()
    {
        hand.localRotation = Quaternion.Euler(0, 0, 90 - hoursToDegrees * ((tm.GetHour() + TimeManager.hoursInDay - tm.sunriseHour) % TimeManager.hoursInDay));
    }
}
