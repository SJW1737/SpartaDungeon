using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue = 100f;
    public float startValue = 100f;
    public Image uiBar;

    private void Start()
    {
        curValue = Mathf.Clamp(startValue, 0, maxValue);
        UpdateBar();
    }

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
        UpdateBar();
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0f);
        UpdateBar();
    }

    private void UpdateBar()
    {
        if (uiBar != null && maxValue > 0f)
            uiBar.fillAmount = curValue / maxValue;
    }
}
