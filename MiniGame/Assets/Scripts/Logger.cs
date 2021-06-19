using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
	public static Logger Instance;
	public Text icon;
    public TMP_Text typeTitle;
    public Color errorColor, infoColor, warningColor, successColor;
	private UITransition uITransition;
	public TMP_Text text;
	private string previousMessage;
    private Image img;

	private void Awake()
	{
		Instance = this;
		uITransition = GetComponent<UITransition>();
        img = GetComponent<Image> ();
	}
	public void ShowMessage(string msg, LoggerType type = LoggerType.error, float duration = 5f)
	{
		if (msg.Equals(previousMessage))
		{
			Debug.Log("duplicated error");
			return;
		}
        ((UIFade)uITransition).canvasGroup.alpha = 0;
		if(type == LoggerType.info) {
            icon.text = "";
            typeTitle.text = RTLTMPro.LanguageManager.GetString ("INFO");
            icon.color = infoColor;
        } else if( type == LoggerType.warning) {
            icon.text = "";
            typeTitle.text = RTLTMPro.LanguageManager.GetString ("WARNING");
            icon.color = warningColor;
        } else if (type == LoggerType.success) {
            icon.text = "";
            typeTitle.text = RTLTMPro.LanguageManager.GetString ("SUCCESS");
            icon.color = successColor;
        } else {
            icon.text = "";
            typeTitle.text = RTLTMPro.LanguageManager.GetString ("Error");
            icon.color = errorColor;
        }
        typeTitle.color = icon.color;
        img.color = icon.color;
		uITransition.CancelInvoke();
		previousMessage = msg;
		Invoke("ForgetPreviousMessage", duration + uITransition.Duration());
		text.text = RTLTMPro.LanguageManager.GetString (msg);
        text.GetComponent<ContentSizeFitter>().enabled = false;
        GetComponent<ContentSizeFitter>().enabled = false;
        Invoke ("ShowText", 0.05f);
		uITransition.Invoke("Hide", duration);
	}
    private void ShowText () {
        text.GetComponent<ContentSizeFitter>().enabled = true;
        Invoke("SetPerfectSize", 0.05f);
    }
    void SetPerfectSize()
    {
        GetComponent<ContentSizeFitter>().enabled = true;
        uITransition.Show ();
    }
    private void ForgetPreviousMessage()
	{
		previousMessage = "";
	}
}
public enum LoggerType
{
	error,
	info,
	warning,
    success
}