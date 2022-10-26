using UnityEngine;

public class Level1_6ProgressController : MonoBehaviour
{
    public GameObject gun;

    void Start()
    {
        if (Player.progress_2 == true)
            Destroy(gun);
    }
    void Update()
    {
        if (gun != null && gun.activeSelf == false)
        {
            Player.progress_2 = true;

            FlashImage flashImage = FindObjectOfType<FlashImage>();

            flashImage.StartFlash(1, 255, Color.white, 1, MovePlayer);

            FindObjectOfType<AudioManager>().Play("ItemPickup");

            gameObject.SetActive(false);
        }
    }

    void MovePlayer()
    {
        Player player = FindObjectOfType<Player>();
        player.transform.position = new Vector2(55f, 0f);
    }

}
