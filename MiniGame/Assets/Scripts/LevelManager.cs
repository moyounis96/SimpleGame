using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public UIFade mapPanel;
    public bool isLastLevel = false;
    private UIFade uIFade;
    private bool q_showMap = false;
    public static Vector3 groundMoveDirection;
    public static float groundShrinkSpeed;

    void Awake()
    {
        uIFade = GetComponentInChildren<UIFade>();
        GameManager.Instance.IsLastLevel(isLastLevel);
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        groundShrinkSpeed = Random.Range(0.01f, levelIndex/10.0f);
        groundMoveDirection = Vector3.right * Random.Range(0.0f, levelIndex) + Vector3.forward * Random.Range(0.0f, levelIndex);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!mapPanel.shown)
            {
                ShowMap();
                q_showMap = true;
            }
        }
        else if (q_showMap && mapPanel.shown)
        {
            HideMap();
            q_showMap = false;
        }
    }
    public void ShowMap()
    {
        mapPanel.Show();
        uIFade.Hide();
    }
    public void HideMap()
    {
        uIFade.Show();
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
