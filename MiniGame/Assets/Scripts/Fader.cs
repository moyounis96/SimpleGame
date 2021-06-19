using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public static Fader Instance;
    public float duration;
    [HideInInspector] public UITransition uITransition;
    private Action targetAction;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        uITransition = GetComponentInChildren<UITransition>();
        duration = uITransition.Duration();
        SceneManager.sceneLoaded += delegate { if(SceneManager.GetActiveScene().buildIndex != 0) Invoke("Hide", uITransition.Duration() + 0.5f); };
    }
    public void Transition()
    {
        CancelInvoke();
        uITransition.Show();
        Invoke("Hide", duration + 0.5f);
    }
    public void Transition(Action action)
    {
        CancelInvoke();
        targetAction = action;
        Invoke("DoAction", duration);
        uITransition.Show();
    }
    private void DoAction()
    {
        targetAction.Invoke();
        Hide();
    }
    private void Hide()
    {
        uITransition.Hide();
    }
}
