using UnityEngine;

public class Constants : MonoBehaviour
{
    public static Constants Instance;
    #region Strings
    public const string MUSIC_VOLUME_PREFS = "MUSIC_VOLUME_PREFS";
    public const string SFX_VOLUME_PREFS = "SFX_VOLUME_PREFS";
    #endregion
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
