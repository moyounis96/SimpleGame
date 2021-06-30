using UnityEngine;

public class Ground : MonoBehaviour
{
    bool playerOn;
    private Vector3 oriScale;
    private float shrinkSpeed;
    private Vector3 moveDirection;

    private void Awake()
    {
        transform.localScale += Vector3.right * Random.Range(0, 5.0f) + Vector3.forward * Random.Range(0, 5.0f);
        oriScale = transform.localScale;
    }
    private void Start()
    {
        shrinkSpeed = LevelManager.groundShrinkSpeed;
        moveDirection = LevelManager.groundMoveDirection;
    }
    private void Update()
    {
        if (transform.localScale.x <= 1f)
        {
            GetComponent<Renderer>().material.color = Color.red;
            Destroy(gameObject, 3f);
            Destroy(this);
        }
        //transform.Translate(moveDirection * Time.deltaTime, Space.Self);
        if (playerOn)
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.up * transform.localScale.y, Time.deltaTime * shrinkSpeed);
        else if ((transform.localScale - oriScale).sqrMagnitude > 2)
            transform.localScale = Vector3.Lerp(transform.localScale, oriScale, Time.deltaTime * shrinkSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.PLAYER_TAG))
            playerOn = true;
        if (!collision.collider.CompareTag(Constants.LAVA_TAG))
            moveDirection = -moveDirection;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.PLAYER_TAG))
            playerOn = false;
    }

}
