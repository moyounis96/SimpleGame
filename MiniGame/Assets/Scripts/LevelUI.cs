using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public void UnlockBtn()
    {
        GameManager.Instance.UnlockCurrentLevel();
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
