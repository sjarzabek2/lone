using System.Collections.Generic;
using UnityEngine;

public class Level1_5ProgressController : MonoBehaviour
{
    public List<GameObject> objectsToEnable;
    void Start()
    {
        if (Player.progress_7 == true)
        {
            foreach (GameObject obj in objectsToEnable)
                obj.SetActive(true);
        }
    }

}
