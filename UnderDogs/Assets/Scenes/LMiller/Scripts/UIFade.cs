using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroupUI;
    [SerializeField] private UnityEngine.Video.VideoPlayer panel;
    [SerializeField] private bool fadeIn = true;
    [SerializeField] private bool firstFadeIn = true;

    public float fadeInDelay;
    private float delayTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroupUI.alpha = 0;
        panel.targetCameraAlpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(delayTimer < fadeInDelay)
        {
            delayTimer += Time.deltaTime;
        }

        if (panel.targetCameraAlpha < 1 && fadeIn == true && delayTimer >= fadeInDelay)
        {
            panel.targetCameraAlpha += Time.deltaTime;
            if (panel.targetCameraAlpha >= 1)
            {
                firstFadeIn = false;
            }
        }

        if (canvasGroupUI.alpha < 1 && fadeIn == true && delayTimer >= fadeInDelay && firstFadeIn == false)
        {
            canvasGroupUI.alpha += Time.deltaTime;
            if(canvasGroupUI.alpha >= 1)
            {
                fadeIn = false;
            }
        }
    }
}
