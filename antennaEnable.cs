using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antennaEnable : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject antennaTxt;
    public void whenButtonClicked()
    {
        if (antennaTxt.activeInHierarchy == true)
            antennaTxt.SetActive(false);
        else
            antennaTxt.SetActive(true);
    }
}
