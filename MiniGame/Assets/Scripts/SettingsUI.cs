using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance;
    [HideInInspector] public UIFade UIFade;
    public Slider musicSlider, sfxSlider;
    private void Awake()
    {
        Instance = this;
        UIFade = GetComponentInChildren<UIFade>();
    }
    public void MainMenuBtn()
    {
        UIFade.Hide();
        MainMenuUI.Instance.UIFade.Show();
    }
    public void MusicSlider(float volume)
    {
        Debug.Log("Music volume: " + volume);
    }
    public void SfxSlider(float volume)
    {
        Debug.Log("SFX volume: " + volume);
    }
}
