using UnityEngine;

public class PopUpWindow : MonoBehaviour
{
    public GameObject popUpBox;

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        Player.progress_1 = true;
        FindObjectOfType<AudioManager>().Play("ItemPickup");
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            popUpBox.SetActive(false);
    }
}
