using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject MiniMap;
    public Camera LargeCamera;

    private void Start()
    {
        MainCamera.enabled = true;
        LargeCamera.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MainCamera.enabled = !MainCamera.enabled;
            MiniMap.SetActive(!MiniMap.activeSelf);
            LargeCamera.enabled = !LargeCamera.enabled;
            if(Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
