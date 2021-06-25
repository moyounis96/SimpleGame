using UnityEngine;

public class Constants : MonoBehaviour
{
    public static Constants Instance;
    #region Strings
    public const string PLAYER_TAG = "Player";
    public const string LAVA_TAG = "Lava";
    public const string FINISH_TAG = "Finish";
    public const string MUSIC_VOLUME_PREFS = "MUSIC_VOLUME_PREFS";
    public const string SFX_VOLUME_PREFS = "SFX_VOLUME_PREFS";
    public const string LAST_UNLOCKED_PREFS = "LAST_UNLOCKED_PREFS";
    public const string BEST_SCORE_PREFS = "BEST_SCORE_PREFS_LEVEL_"; //use this with level number always;
    #endregion
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
