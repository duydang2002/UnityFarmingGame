using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DateTimeNameSpace;

public class WeatherManager : MonoBehaviour
{

    [Header("=== Weather Management ===")]
    [SerializeField] private Weather currentWeather = Weather.Sunny;
    public Weather CurrentWeather => currentWeather;
    [Header("=== Weather Effects ====")]
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] ParticleSystem rainDropParticles;
    [SerializeField] ParticleSystem snowParticles;
    [Header("=== Debug Options ====")]
    public bool forceRain = false;
    public bool forceSunny = false;
    public bool forceSnow = false;

    public static Action<Weather> OnWeatherChange;
    private int currentWeatherTick = 0;
    private int ticksBetweenWeather = 25;
    public float timeBetweenTicks = 1;
    private float currentTimeBetweenTicks = 0;
    // Start is called before the first frame update
    void Start()
    {
        rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        rainDropParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ChangeWeather();
    }

    // Update is called once per frame


    private void ChangeWeather()
    {
        if ((Season)TimeManagerScript.season == Season.Winter)
        {
            currentWeather = Weather.Snow;
            rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            rainDropParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            snowParticles.Play();
        }
        else
        {
            currentWeather = GetRandomWeather();
            OnWeatherChange?.Invoke(currentWeather);
            switch (currentWeather)
            {
                case Weather.Sunny:
                    rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    rainDropParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    break;
                case Weather.Rainny:
                    snowParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    rainParticles.Play();
                    rainDropParticles.Play();
                    break;
                case Weather.Snow:
                    rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    rainDropParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    snowParticles.Play();
                    break;
                default:
                    break;
            }
        }
        Debug.Log($"Current weather {currentWeather}");
    }
    void Update()
    {
        currentTimeBetweenTicks += Time.deltaTime;
        if (currentTimeBetweenTicks >= timeBetweenTicks)
        {
            currentTimeBetweenTicks = 0;
            Tick();
        }
    }

    private void Tick()
    {
        currentWeatherTick++;
        if (currentWeatherTick >= ticksBetweenWeather)
        {
            currentWeatherTick = 0;
            ChangeWeather();
        }
        // Debug.Log($"Current weather {currentWeather}");
        // Debug.Log($"Tick: {currentWeatherTick}");
    }

    private Weather GetRandomWeather()
    {
        int randomWeather = 0;
        if (forceSunny) randomWeather = (int)Weather.Sunny;
        else if (forceRain) randomWeather = (int)Weather.Rainny;
        else if (forceSnow) randomWeather = (int)Weather.Snow;
        else randomWeather = UnityEngine.Random.Range(0, 2);
        return (Weather)randomWeather;
    }
    [System.Serializable]
    public enum Weather
    {
        Sunny = 0,
        Rainny = 1,
        Snow = 2,
    }
}
