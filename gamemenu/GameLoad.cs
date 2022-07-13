using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameLoad : MonoBehaviour
{
    public string sceneName;
    public void loadScene()
    {
        Loading.loadingScene(sceneName);
    }
}
