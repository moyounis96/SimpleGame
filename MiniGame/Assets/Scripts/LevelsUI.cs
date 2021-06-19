using UnityEngine;

public class LevelsUI : MonoBehaviour
{
    public static LevelsUI Instance;
    void Awake()
    {
        Instance = this;
    }
    public void MainMenuBtn()
    {
        GameManager.Instance.LoadScene(1);
    }
}
