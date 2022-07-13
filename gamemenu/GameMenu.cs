using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private float timing, timer = 0.3f;
    private Button[] btn;
    private int btnIndex = 0;

    public GameObject vrUI;
    void Start()
    {
        btn = GetComponentsInChildren<Button>();
        btn[btnIndex].Select();
        vrUI.SetActive(PanelSel.isVR);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("home");
        }
        timing += Time.deltaTime;
        if(Control.block())
        {
            if(timing >= timer)
            {
                timing = 0;
                btnIndex = (btnIndex + 1 < btn.Length ? (btnIndex + 1) : 0);
                btn[btnIndex].Select();      
            }
        }
        if(Control.attack())
        {
            btn[btnIndex].GetComponent<GameLoad>().loadScene();
        }
    }
}
