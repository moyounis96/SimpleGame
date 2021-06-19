using UnityEngine;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;
    public UITransition uITransition;
    private RTLTMPro.RTLTextMeshPro loadingText;
    public static bool isShown;
    public GameObject loopProgressBar;
    public Image progressBar;
    private AsyncOperation operation;
    bool isShowingOperationProgress;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        loadingText = uITransition.GetComponentInChildren<RTLTMPro.RTLTextMeshPro>();
    }
    private void Update()
    {
        if (isShowingOperationProgress && operation!=null)
        {
            progressBar.fillAmount = operation.progress * 100;
        }
    }
    public void Show(string loadingText = "", bool loop = true)
    {
        this.loadingText.text = loadingText;
        progressBar.gameObject.SetActive(!loop);
        loopProgressBar.SetActive(loop);
        isShown = true;
        uITransition.Show();
    }
    public void Show(string loadingText, AsyncOperation operation, bool autoHideAfter = false)
    {
        this.operation = operation;
        isShowingOperationProgress = true;
        operation.completed += delegate { 
            isShowingOperationProgress = false;
            if (autoHideAfter)
                Hide();
        };
        Show(loadingText, false);
    }
    public void Hide()
    {
        isShown = false;
        isShowingOperationProgress = false;
        operation = null;
        progressBar.fillAmount = 0;
        uITransition.Hide();
    }
}
