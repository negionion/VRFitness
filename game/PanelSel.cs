using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSel : MonoBehaviour
{
    public static bool isVR = false;
    // Start is called before the first frame update
    void Start()
    {
        if(isVR)
        {
            this.GetComponentInChildren<Text>().text = "VR";
        }
        else
        {
            this.GetComponentInChildren<Text>().text = "Phone";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void panelSW()
    {
        if(isVR)
        {
            isVR = false;
            this.GetComponentInChildren<Text>().text = "Phone";

        }
        else
        {
            isVR = true;
            this.GetComponentInChildren<Text>().text = "VR";
        }
    }
}
