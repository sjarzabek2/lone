using System.Collections.Generic;
using UnityEngine;

public class Level1_3ProgressController : MonoBehaviour
{
    [Header("Progress_1 variables")]
    public List<GameObject> Progress1ObjectsToDisable;
    public List<GameObject> Progress1ObjectsToEnable;

    [Header("Progress_8 variables")]
    public GameObject OrbPrefab;
    public GameObject MovingOrbTarget;
    private bool _goodToGo;
    private GameObject _movingOrb;

    [Header("Progress_9 variables")]
    public List<GameObject> Progress9ObjectsToDisable;
    public List<GameObject> Progress9ObjectsToEnable;


    void Start()
    {
        // Progress_1 handling
        if (Player.progress_1 == true)
        {
            foreach (GameObject obj in Progress1ObjectsToDisable)
                obj.SetActive(false);
            foreach (GameObject obj in Progress1ObjectsToEnable)
                obj.SetActive(true);
        }

        // Progress_8 handling
        if (Player.progress_7 == true && Player.progress_8 == false)
        {
            if (_movingOrb == null)
            {
                _movingOrb = Instantiate(OrbPrefab, gameObject.transform.position, Quaternion.identity);
                _goodToGo = true;
            }
        }

        // Progress_9 handling
        if (Player.progress_9 == true)
        {
            foreach (GameObject obj in Progress9ObjectsToDisable)
                obj.SetActive(false);
            foreach (GameObject obj in Progress9ObjectsToEnable)
                obj.SetActive(true);
        }
    }
    void Update()
    {
        // Progress_8 handling
        if (_goodToGo == true)
        {
            _movingOrb.transform.position = Vector2.MoveTowards(_movingOrb.transform.position, MovingOrbTarget.transform.position, 10f * Time.deltaTime);
            Vector2 delta = _movingOrb.transform.position - MovingOrbTarget.transform.position;
            Debug.Log(delta);
            if (delta.x < 0.5f && delta.y < 0.5f)
            {
                Destroy(_movingOrb);
                Player.progress_8 = true;
                _goodToGo = false;
            }
        }
    }
}
