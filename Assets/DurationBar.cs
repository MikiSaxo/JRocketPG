using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurationBar : MonoBehaviour
{
    public Slider Slider;
    public Gradient Grad;
    public Image Fill;
    [HideInInspector]
    public float _launchTime;
    public int DurationTime;

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
        Slider.maxValue = time;
        Slider.value = time;

        Fill.color = Grad.Evaluate(1f);
    }

    private void Update()
    {
        _launchTime -= Time.deltaTime / DurationTime;
        SetTime(_launchTime);
    }

    public void SetTime(float time)
    {
        Slider.value = time;
        Fill.color = Grad.Evaluate(Slider.normalizedValue);
    }
}
