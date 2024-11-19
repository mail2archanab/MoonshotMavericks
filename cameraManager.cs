using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
   public GameObject earthViewCamera;
   public GameObject moonViewCamera;
   public GameObject topViewCamera;
   public GameObject fullViewCamera;
   public GameObject LandingCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            earthViewCamera.SetActive(true);
            moonViewCamera.SetActive(false);
            topViewCamera.SetActive(false);
            fullViewCamera.SetActive(false);
            LandingCamera.SetActive(false);
        }
        if (Input.GetButtonDown("2Key"))
        {
            earthViewCamera.SetActive(false);
            moonViewCamera.SetActive(true);
            topViewCamera.SetActive(false);
            fullViewCamera.SetActive(false);
            LandingCamera.SetActive(false);
        }
        if (Input.GetButtonDown("3Key"))
        {
            earthViewCamera.SetActive(false);
            moonViewCamera.SetActive(false);
            topViewCamera.SetActive(true);
            fullViewCamera.SetActive(false);
            LandingCamera.SetActive(false);
        }
        if (Input.GetButtonDown("4Key"))
        {
            earthViewCamera.SetActive(false);
            moonViewCamera.SetActive(false);
            topViewCamera.SetActive(false);
            fullViewCamera.SetActive(true);
            LandingCamera.SetActive(false);
        }
        if (Input.GetButtonDown("5Key"))
        {
            earthViewCamera.SetActive(false);
            moonViewCamera.SetActive(false);
            topViewCamera.SetActive(false);
            fullViewCamera.SetActive(false);
            LandingCamera.SetActive(true);
        }
    }
}
