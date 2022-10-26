using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector2 NewLocation;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == collision.CompareTag("Player"))
        {
            FlashImage flashImage = FindObjectOfType<FlashImage>();

            flashImage.StartFlash(.5f, 255f, Color.black, .5f, MovePlayer);
        }
    }

    void MovePlayer()
    {
        Player player = FindObjectOfType<Player>();
        player.transform.position = NewLocation;
    }
}
