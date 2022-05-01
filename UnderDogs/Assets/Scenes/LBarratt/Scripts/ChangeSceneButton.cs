using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{

    public void ChangeScene(string sceneName)
    {
        //LevelManager.Instance.LoadScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
