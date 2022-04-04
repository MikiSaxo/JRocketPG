using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;

    public Collider[] tonneaux;


    private void Awake()
    {
        if (GameData.initialized == true)
            transform.position = GameData.boatPos;
        else
            GameData.boatPos = transform.position;

        if (GameData.firstOpening == false)
            SceneManager.LoadScene(2);
    }
    private void Update()
    {
        GameData.boatPos = transform.position;

        //GameData.IsGPEActive[0] = false;

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //if()

            //    if (tonneaux[i] = )
            //    {
            //        GameData.IsGPEActive[0] = true;
            //    }
            //    if (tonneaux[i] = )
            //    {
            //        GameData.IsGPEActive[1] = true;
            //    }
            //    if (tonneaux[i] = )
            //    {
            //        GameData.IsGPEActive[2] = true;
            //    }

    }
}
