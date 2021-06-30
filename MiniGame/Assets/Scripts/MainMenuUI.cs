using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance;
    [HideInInspector] public UIFade UIFade;
    private void Awake()
    {
        Instance = this;
        UIFade = GetComponentInChildren<UIFade>();
    }
    public void StartBtn()
    {
        GameManager.Instance.LoadScene(2);
    }
    public void SettingsBtn()
    {
        UIFade.Hide();
        SettingsUI.Instance.UIFade.Show();
    }
    public void EndBtn()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("End Game!");
#endif
    }
}
