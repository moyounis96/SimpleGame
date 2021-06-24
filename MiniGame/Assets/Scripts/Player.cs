using UnityEngine;

public class Player : MonoBehaviour
{
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
