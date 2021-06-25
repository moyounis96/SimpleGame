using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int _lastUnlocked = 3;
    public UITransition winScreen, loseScreen;
    public TextMeshProUGUI winScoreText, winBestScoreText, loseScoreText, loseBestScoreText;
    public Button nextButton;
    public int LastUnlocked
    {
        get { return _lastUnlocked; }
    }

    public void IsLastLevel(bool isLastLevel)
    {
        if(isLastLevel)
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
    [ContextMenu("TestFinish")]
    public void FinishLevel()
    {
        Player.isControlling = false;
        UnlockCurrentLevel();
        winScoreText.text = "Score: " + Player.Score;
        winBestScoreText.text = "Best Score: " + Player.BestScore;
        winScreen.Show();
    }
    [ContextMenu("LOOOSE")]
    public void Lose()
    {
        Player.isControlling = false;
        loseScoreText.text = "Score: " + Player.Score;
        loseBestScoreText.text = "Best Score: " + Player.BestScore;
        loseScreen.Show();
    }
    
    public void NextLevelBtn()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        winScreen.Hide();
        loseScreen.Hide();
    }
    public void AgainBtn()
    {
        winScreen.Hide();
        loseScreen.Hide();
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadScene(int index)
    {
        Fader.Instance.Transition(delegate
        {
            SceneManager.LoadSceneAsync(index);
        });
    }
    public void UnlockCurrentLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(_lastUnlocked);
        if (_lastUnlocked <= currentScene)
        {
            PlayerPrefs.SetInt(Constants.LAST_UNLOCKED_PREFS, currentScene + 1);
            _lastUnlocked = currentScene + 1;
            Debug.Log(_lastUnlocked);
        }
    }
}
