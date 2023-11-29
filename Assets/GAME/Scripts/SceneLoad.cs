using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public string nextScene;

    public void Load()
    {
        LoadingSceneController.LoadScene(nextScene);
    }

}
