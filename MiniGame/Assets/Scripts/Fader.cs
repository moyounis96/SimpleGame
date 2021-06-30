using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public static Fader Instance;
    public float duration;
    [HideInInspector] public UIFade uIFade;
    private Action targetAction;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        uIFade = GetComponentInChildren<UIFade>();
        duration = uIFade.Duration();
        SceneManager.sceneLoaded += delegate { if(SceneManager.GetActiveScene().buildIndex != 0) Invoke("Hide", uIFade.Duration() + 0.5f); };
    }
    public void Transition()
    {
        CancelInvoke();
        uIFade.Show();
        Invoke("Hide", duration + 0.5f);
    }
    public void Transition(Action action)
    {
        CancelInvoke();
        targetAction = action;
        Invoke("DoAction", duration);
        uIFade.Show();
    }
    private void DoAction()
    {
        targetAction.Invoke();
        Hide();
    }
    private void Hide()
    {
        uIFade.Hide();
    }
}
