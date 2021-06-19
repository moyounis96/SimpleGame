using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashUI : MonoBehaviour
{
    UITransition splash;
    void Awake()
    {
        splash = GetComponentInChildren<UITransition>();
    }
    void Start()
    {
        Invoke("LoadMenu", 3);
    }
    void LoadMenu()
    {
        Fader.Instance.Transition(delegate
        {
            splash.Hide();
            SceneManager.LoadSceneAsync(1);
        });
        
    }
}
