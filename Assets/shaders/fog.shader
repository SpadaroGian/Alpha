using UnityEngine;

public class SunLightController : MonoBehaviour
{
    public Light sun; 
    public Gradient sunColor;
    public AnimationCurve sunIntensity; 

    [Header("Time Settings")]
    [Range(0, 24)] public float timeOfDay = 12f; 
    public float dayLengthInSeconds = 120f; 

    private float timeMultiplier; 

    void Start()
    {
        timeMultiplier = 24f / dayLengthInSeconds; 
    }

    void Update()
    {
        timeOfDay += Time.deltaTime * timeMultiplier;
        if (timeOfDay >= 24f)
        {
            timeOfDay -= 24f; 
        }
        UpdateSunLight();
    }

    void UpdateSunLight()
    {
        if (sun != null)
        {
            float sunAngle = (timeOfDay / 24f) * 360f - 90f;
            sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
            sun.color = sunColor.Evaluate(timeOfDay / 24f);
            sun.intensity = sunIntensity.Evaluate(timeOfDay / 24f);
        }
    }
}
