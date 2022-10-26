using UnityEngine;

public class Level2_4ProgressController : MonoBehaviour
{
    public GameObject LeverHandle;

    void Update()
    {
        if (Player.progress_4 == false)
        {
            if (LeverHandle.activeSelf == false)
            {
                FindObjectOfType<AudioManager>().Play("ItemPickup");
                Player.progress_4 = true;
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
            LeverHandle.SetActive(false);
        }
        
    }
}
