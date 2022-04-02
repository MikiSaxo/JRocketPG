using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatObjectScript : MonoBehaviour
{
    public float waterLevel = 0.0f;
    public float floatThreshold = 2.0f;
    public float waterDensity = 0.125f;
    public float downForce = 4.0f;

    float forceFactor;
    Vector3 floatForce;

    private bool up;
    private bool can_move = true;

    public float timer;
    public float variation;

    void FixedUpdate()
    {
        forceFactor = 1.0f - ((transform.position.y - waterLevel) / floatThreshold);

        if (forceFactor > 0.0f)
        {
            floatForce = -Physics.gravity * GetComponent<Rigidbody>().mass * (forceFactor - GetComponent<Rigidbody>().velocity.y * waterDensity);
            floatForce += new Vector3(0.0f, -downForce * GetComponent<Rigidbody>().mass, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
        }
    }

    private void Update()
    {
        if (can_move == true)
        {
            can_move = false;

            if (up == false)
            {
                StartCoroutine(Up());
            }
            else
            {
                StartCoroutine(Down());
            }
        }
    }

    IEnumerator Up()
    {
        waterLevel += variation;
        yield return new WaitForSeconds(timer);
        up = true;
        can_move = true;
    }

    IEnumerator Down()
    {
        waterLevel -= variation;
        yield return new WaitForSeconds(timer);
        up = false;
        can_move = true;
    }
}
