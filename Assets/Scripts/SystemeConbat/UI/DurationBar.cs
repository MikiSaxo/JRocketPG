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
    public float LaunchTime;
    public int DurationTime;
    public GameObject FollowSlider;
    public GameObject StartPointSlider;
    public GameObject EndPointSlider;
    public float[] PositionStepsSlider;
    public GameObject[] StepsSlider;

    public static DurationBar Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetmaxTime(1);
        SetStepsPositions();
    }

    public void SetmaxTime(int time)
    {
        Slider.maxValue = time;
        Slider.value = time;

        Fill.color = Grad.Evaluate(1f);
    }

    public void SetStepsPositions()
    {
        for (int i = 0; i < PositionStepsSlider.Length; i++)
        {
            //StepsSlider[i].transform.position = Vector3.Lerp(EndPointSlider.transform.position, StartPointSlider.transform.position, PositionStepsSlider[i]);
            StepsSlider[i].SetActive(true);
        }
    }

    private void Update()
    {
        LaunchTime -= Time.deltaTime / DurationTime;
        SetTime(LaunchTime);
        FollowSlider.transform.position = Vector3.Lerp(EndPointSlider.transform.position, StartPointSlider.transform.position, LaunchTime);

        if (LaunchTime <= 0)
        {
            QTE.Instance.EndOfQTE();
        }
    }

    public void SetTime(float time)
    {
        Slider.value = time;
        Fill.color = Grad.Evaluate(Slider.normalizedValue);
    }
}
