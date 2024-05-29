using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;


    void Start()
    {
        curValue = startValue;
    }


    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
        // curValue + value 의 값이 maxValue를 초과하면 maxValue가 할당되게 설정
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
        // curValue - value 의 값이 0를 미만이면 0 할당되게 설정
    }
}
