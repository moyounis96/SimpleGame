using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashUI : MonoBehaviour
{
    UIFade splash;
    void Awake()
    {
        splash = GetComponentInChildren<UIFade>();
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
