using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance;
    [HideInInspector] public UITransition uITransition;
    private void Awake()
    {
        Instance = this;
        uITransition = GetComponentInChildren<UITransition>();
        LoadingScreen.Instance.Hide();
    }
    public void StartBtn()
    {
        GameManager.Instance.LoadScene(2);
    }
    public void SettingsBtn()
    {
        uITransition.Hide();
        SettingsUI.Instance.uITransition.Show();
    }
    public void EndBtn()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("End Game!");
#endif
    }
}
