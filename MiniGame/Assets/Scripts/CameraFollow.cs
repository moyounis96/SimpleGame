using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothFactor;
    private Transform target;
    private Vector3 offset;
    private Transform arrow;
    private Transform finishTransform;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).transform;
        offset = transform.position - target.position;
        arrow = transform.GetChild(0).GetChild(0);
        finishTransform = GameObject.FindGameObjectWithTag(Constants.FINISH_TAG).transform;
    }
    void Update()
    {
        arrow.LookAt(finishTransform);
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
    }
}
