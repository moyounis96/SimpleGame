using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothFactor;
    private Transform target;
    private Vector3 offset;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).transform;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
    }
}
