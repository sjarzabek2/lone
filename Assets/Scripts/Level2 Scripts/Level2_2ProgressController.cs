using UnityEngine;

public class Level2_2ProgressController : MonoBehaviour
{
    public GameObject oldBackground;
    public GameObject newBackground;
    void Start()
    {
        if (Player.progress_6 == true)
        {
            oldBackground.SetActive(false);
            newBackground.SetActive(true);
        }
    }
}
