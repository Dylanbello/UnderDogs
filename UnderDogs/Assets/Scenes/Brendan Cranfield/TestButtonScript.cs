using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestButtonScript : MonoBehaviour
{
    bool isPaused = false;

    public UnityEvent onPaused;
    public UnityEvent onResume;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) Pause();
            else Resume();
        }
    }

    public void Pause()
    {
        if (isPaused) return;

        Time.timeScale = 0;
        onPaused.Invoke();
        isPaused = true;
    }

    public void Resume()
    {
        if (!isPaused) return;

        Time.timeScale = 1;
        onResume.Invoke();
        isPaused = false;
    }

    public void Quit()
    {
        Debug.Log("Application has quit");
    }

    public void Settings()
    {
        Debug.Log("Settings opened");
    }
}
