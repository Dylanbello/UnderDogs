using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        ChangeScene("LBarratt");
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
