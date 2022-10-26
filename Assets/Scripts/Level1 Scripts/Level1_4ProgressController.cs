using System.Collections.Generic;
using UnityEngine;

public class Level1_4ProgressController : MonoBehaviour
{
    public List<GameObject> ObjectsToDisable = new List<GameObject>();
    public List<GameObject> ObjectsToEnable = new List<GameObject>();
    void Start()
    {
        if (Player.progress_8 == true)
        {
            foreach (GameObject obj in ObjectsToDisable)
                obj.SetActive(false);
            foreach (GameObject obj in ObjectsToEnable)
                obj.SetActive(true);
        }
    }
}
