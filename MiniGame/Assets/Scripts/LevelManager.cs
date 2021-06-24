using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public UITransition mapPanel;
    private UITransition uITransition;
    private bool showMap = false;
    void Awake()
    {
        uITransition = GetComponentInChildren<UITransition>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!mapPanel.shown)
            {
                ShowMap();
                showMap = true;
            }
        }
        else if (showMap && mapPanel.shown)
        {
            HideMap();
            showMap = false;
        }
    }
    public void ShowMap()
    {
        mapPanel.Show();
        uITransition.Hide();
    }
    public void HideMap()
    {
        uITransition.Show();
        mapPanel.Hide();
    }
    public void LevelsBtn()
    {
        GameManager.Instance.LoadScene(2);
    }
    public void MainMenuBtn()
    {
        GameManager.Instance.LoadScene(1);
    }
}
