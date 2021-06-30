using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    Transform player, finish;
    Vector3 center;
    private Camera cam;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).transform;
        finish = GameObject.FindGameObjectWithTag(Constants.FINISH_TAG).transform;
        center = Vector3.Lerp(player.position, finish.position, 0.5f);
        transform.position = new Vector3(center.x,transform.position.y,center.z);
    }
    private void Update()
    {
        if (!Player.isControlling) return;
        Bounds b = new Bounds(finish.position, Vector3.one);
        b.Encapsulate(player.position);
        cam.orthographicSize = Mathf.Clamp(-(new Vector3(b.size.x,0,b.size.z).magnitude - 10), -80,-30);
        center = Vector3.Lerp(player.position, finish.position, 0.5f);
        transform.position = new Vector3(center.x, transform.position.y, center.z);
    }
}
