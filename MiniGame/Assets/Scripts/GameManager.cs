using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int _lastUnlocked = 3;
    public int LastUnlocked
    {
        get { return _lastUnlocked; }
    }

    public void FinishLevel()
    {
    }

    public void Lose()
    {
    }

    void Awake()
    {
        Instance = this;
        _lastUnlocked = PlayerPrefs.GetInt(Constants.LAST_UNLOCKED_PREFS, 3);
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
