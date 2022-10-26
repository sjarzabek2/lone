using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            FindObjectOfType<Player>().RespawnPoint = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }
}
