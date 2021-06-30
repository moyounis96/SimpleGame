using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float speed, jumpForce;
    public static bool isControlling = true;
    private Rigidbody _rigidbody;
    private Vector3 camForward;
    private float h, v;
    private Transform cam;
    private Vector3 groundNormal, move;
    private bool isGrounded;

    public static float BestScore = 0;
    public static float Score;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }
    void OnEnable()
    {
        BestScore = PlayerPrefs.GetFloat(Constants.BEST_SCORE_PREFS + SceneManager.GetActiveScene().buildIndex, 0);
        Score = 0;
        isControlling = true;
    }
    void Update()
    {
        if (!isControlling) return;
        Score += Time.deltaTime;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        move = v * camForward + h * cam.right; //vector(h*right,YYYYY,v.forward)
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, groundNormal);
        _rigidbody.position += move * Time.deltaTime * speed;
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.down * transform.localScale.y / 2), Vector3.down, out hitInfo, 0.5f))
        {
            groundNormal = hitInfo.normal;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case Constants.FINISH_TAG:
                GameManager.Instance.FinishLevel();
                break;
            case Constants.LAVA_TAG:
                GameManager.Instance.Lose();
                break;
            default:
                break;
        }
    }
}
