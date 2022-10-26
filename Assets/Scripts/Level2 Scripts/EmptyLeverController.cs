using UnityEngine;

public class EmptyLeverController : MonoBehaviour
{
    public GameObject oldLever;
    public GameObject newLever;
    public void InsertHandle()
    {
        if (Player.progress_4 == true)
        {
            FindObjectOfType<AudioManager>().Play("ItemPickup");
            oldLever.SetActive(false);
            newLever.SetActive(true);
            Player.progress_5 = true;
        }
        else
        {
            FindObjectOfType<PopUp>().StartPopUp("This lever needs a handle");
        }
    }
}
