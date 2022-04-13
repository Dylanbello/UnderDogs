using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroupUI;

    [SerializeField] private bool fadeIn = true;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroupUI.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(canvasGroupUI.alpha < 1 && fadeIn == true)
        {
            canvasGroupUI.alpha += Time.deltaTime;

            if(canvasGroupUI.alpha >= 1)
            {
                fadeIn = false;
            }
        }
    }
}
