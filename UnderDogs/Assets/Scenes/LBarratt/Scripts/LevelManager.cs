using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;
    private bool isLoading = false;
    
    // Reference To Youtube (Tarodev) Tutorial Link Used To Help Create This Script: https://www.youtube.com/watch?v=OmobsXZSRKo
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene()
    {

        _target = 1;
        _progressBar.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync("MasterScene", LoadSceneMode.Single);
        Debug.Log(scene.progress);
        isLoading = true;
        scene.allowSceneActivation = false;
        
        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
        } while (_progressBar.fillAmount < 1f);

        //await Task.Delay(1000);
        scene.allowSceneActivation = true;

        _loaderCanvas.SetActive(false);
    }



    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 0.2f * Time.deltaTime);
    }
}
