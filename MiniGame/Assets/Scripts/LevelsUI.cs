using UnityEngine;
using UnityEngine.UI;

public class LevelsUI : MonoBehaviour
{
    public static LevelsUI Instance;
    public Button[] levelBtns;
    void Awake()
    {
        Instance = this;
        for (int i = levelBtns.Length - 1; i > GameManager.Instance.LastUnlocked - 3; i--)
        {
            levelBtns[i].interactable = false;
        }
    }
    public void MainMenuBtn()
    {
        GameManager.Instance.LoadScene(1);
    }
    public void LoadLevelBtn(int index)
    {
        GameManager.Instance.LoadScene(index);
    }
}
