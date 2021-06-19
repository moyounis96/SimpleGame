using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance;
    [HideInInspector] public UITransition uITransition;
    public Slider musicSlider, sfxSlider;
    private void Awake()
    {
        Instance = this;
        uITransition = GetComponentInChildren<UITransition>();
        musicSlider.value = AudioManager.Instance.musicSource.volume;
        sfxSlider.value = AudioManager.Instance.sfxSource.volume;
    }
    public void MainMenuBtn()
    {
        uITransition.Hide();
        MainMenuUI.Instance.uITransition.Show();
    }
    public void MusicSlider(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }
    public void SfxSlider(float volume)
    {
        AudioManager.Instance.SetSfxVolume(volume);
    }
}
