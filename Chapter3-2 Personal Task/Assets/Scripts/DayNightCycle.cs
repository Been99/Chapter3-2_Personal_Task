using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f; // 0.5f일 때 90도이고 정오로 표현할 예정
    private float timeRate;
    public Vector3 noon; // Vector3(90,0,0)

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMutiplier;


    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }


    void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMutiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient gradient,AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4f;
        lightSource.color = gradient.Evaluate(time);
        lightSource.intensity = intensity;

        // 90도는 정오이고 이를 0.5로 셋팅해야하는데
        // 360도를 기준으로 보면 0.5는 180도이기에 0.25를 빼서 계산하고
        // 그리고 밤은 1이 되어야 하는데 위의 이유와 같기 때문에 마찬가지로 0.75를 뺌.
        // noon * 4f = 90도 * 4 = 360도

        GameObject go = lightSource.gameObject;
        if(lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if(lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
