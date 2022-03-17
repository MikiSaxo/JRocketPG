using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStepSlider : MonoBehaviour
{
    public GameObject[] StepPoints;
  

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < StepPoints.Length; i++)
        {
            if (other == StepPoints[i].GetComponent<Collider>())
            {
                StepPoints[i].SetActive(false);
                QTE.Instance.StageFailed++;
                //Debug.Log(QTE.Instance.StageFailed);
                return;
            }
        }
    }

}
