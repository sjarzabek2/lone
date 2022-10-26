using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector2> path;
    private int currentIndex;
    public float Speed;
    public bool FollowPlayer;

    void Start()
    {
        currentIndex = 0;
    }

    public void Move()
    {
        if (FollowPlayer == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[currentIndex], Speed * Time.deltaTime);

            if ((Vector2)transform.position == path[currentIndex])
            {
                if (currentIndex < path.Count - 1)
                    currentIndex++;
                else if (currentIndex == path.Count - 1)
                    currentIndex = 0;
            } 
        }
        else
        {
            Vector2 playerPosition = FindObjectOfType<Player>().transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, Speed * Time.deltaTime);
        }
    }
}
