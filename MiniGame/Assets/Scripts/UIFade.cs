using UnityEngine;

[RequireComponent (typeof (CanvasGroup))]
public class UIFade : MonoBehaviour {
    [HideInInspector] public CanvasGroup canvasGroup;
    [HideInInspector] public bool shown = false;
    public bool controlActiveState = true;
    public float duration = 0.3f;
    [Range (0, 1)] public float alpha = 1;
    void Awake () {
        canvasGroup = GetComponent<CanvasGroup> ();
        if (alpha <= 0)
            Hide ();
        else
            Show ();
        shown = canvasGroup.alpha == 1;
        SetActiveSelf (shown);
    }
    void Update () {
        if (canvasGroup.alpha == 1 && alpha == 1)
            return;
        canvasGroup.alpha = Mathf.Clamp01 (canvasGroup.alpha + ((alpha == 1) ? 1 : -1) * Time.deltaTime / duration);
        canvasGroup.interactable = alpha > 0.5f;
        canvasGroup.blocksRaycasts = alpha > 0.5f;
        if (canvasGroup.alpha == 0) {
            SetActiveSelf (false);
        }
    }
    public void Show () {
        SetActiveSelf (true);
        shown = true;
        alpha = 1;
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup> ();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void Hide () {
        shown = false;
        alpha = 0;
    }

    public float Duration () {
        return duration;
    }
    public void SetActiveSelf(bool active)
    {
        if (controlActiveState) gameObject.SetActive(active);
    }
}
