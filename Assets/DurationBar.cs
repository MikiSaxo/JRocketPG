using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurationBar : MonoBehaviour
{
    public Slider slider;
    public Gradient grad;
    public Image fill;
    public float time;

    public static DurationBar Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetmaxTime(1);
    }


    public void SetmaxTime(int time)
    {
        slider.maxValue = time;
        slider.value = time;

        fill.color = grad.Evaluate(1f);
    }

    private void Update()
    {
        time -= Time.deltaTime / 10;
        SetTime(time);
    }

    public void SetTime(float time)
    {
        slider.value = time;
        fill.color = grad.Evaluate(slider.normalizedValue);
    }
}
