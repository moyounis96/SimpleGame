using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }
    public void LoadScene(int index)
    {
        Fader.Instance.Transition(delegate
        {
            SceneManager.LoadSceneAsync(index);
        });
    }
}
