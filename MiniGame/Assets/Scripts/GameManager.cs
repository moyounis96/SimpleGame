using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIFade winScreen, loseScreen;
    public TextMeshProUGUI winScoreText, winBestScoreText, loseScoreText, loseBestScoreText;
    public Button nextButton;
    private int _lastUnlocked = 3;
    public int LastUnlocked
    {
        get { return _lastUnlocked; }
    }
    public void IsLastLevel(bool isLastLevel)
    {
        if (isLastLevel)
        {
            nextButton.interactable = false;
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Coming Soon!";
        }
        else
        {
            nextButton.interactable = true;
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next Level";
        }
    }
    void Awake()
    {
        Instance = this;
        _lastUnlocked = PlayerPrefs.GetInt(Constants.LAST_UNLOCKED_PREFS, 3);
    }
    public void FinishLevel()
    {
        if (Player.Score < Player.BestScore || Player.BestScore == 0)
        {
            Player.BestScore = Player.Score;
            PlayerPrefs.SetFloat(Constants.BEST_SCORE_PREFS + SceneManager.GetActiveScene().buildIndex, Player.BestScore);
        }
        Player.isControlling = false;
        UnlockCurrentLevel();
        winScoreText.text = "Score: " + Player.Score.ToString("N2");
        winBestScoreText.text = Player.BestScore > 0 ? "Best Score: " + Player.BestScore.ToString("N2") : "";
        winScreen.Show();
    }
    public void Lose()
    {
        Player.isControlling = false;
        loseScoreText.text = "Score: " + Player.Score.ToString("N2");
        loseBestScoreText.text = Player.BestScore > 0 ? "Best Score: " + Player.BestScore.ToString("N2") : "";
        loseScreen.Show();
    }
    public void NextLevelBtn()
    {
        winScreen.Hide();
        loseScreen.Hide();
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void AgainBtn()
    {
        winScreen.Hide();
        loseScreen.Hide();
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadScene(int index)
    {
        winScreen.Hide();
        loseScreen.Hide();
        Fader.Instance.Transition(delegate
        {
            SceneManager.LoadSceneAsync(index);
        });
    }
    public void UnlockCurrentLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (_lastUnlocked <= currentScene)
        {
            PlayerPrefs.SetInt(Constants.LAST_UNLOCKED_PREFS, currentScene + 1);
            _lastUnlocked = currentScene + 1;
        }
    }
}
