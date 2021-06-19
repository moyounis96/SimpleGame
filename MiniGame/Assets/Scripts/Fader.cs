using UnityEngine;

public class Fader : MonoBehaviour
{
    public static Fader Instance;
    UITransition uITransition;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        uITransition = GetComponentInChildren<UITransition>();
    }
    
    public void Transition()
    {
        uITransition.Show();
        uITransition.Invoke("Hide", uITransition.Duration() + 0.5f);
    }
}
