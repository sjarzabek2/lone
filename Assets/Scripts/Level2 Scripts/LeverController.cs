using UnityEngine;

public class LeverController : MonoBehaviour
{
    public Sprite LeverOff;
    public Sprite LeverOn;

    [HideInInspector]
    public bool State;
    void Update()
    {
        if (State == true)
            gameObject.GetComponent<SpriteRenderer>().sprite = LeverOn;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = LeverOff;
    }

    public void Shift()
    {
        if (Player.progress_5 == true)
        {
            FindObjectOfType<AudioManager>().Play("LeverCrank");
            State = true;
            GetComponentInChildren<CircleCollider2D>().enabled = false;

            if (Player.progress_6 == false)
            {
                Level2_3ProgressController ctrl = FindObjectOfType<Level2_3ProgressController>();
                ctrl._combination += gameObject.name[6];
            }
        }
        else
        {
            FindObjectOfType<PopUp>().StartPopUp("I can't move it");
        }
    }

    public void ResetState()
    {
        FindObjectOfType<AudioManager>().Play("LeverCrank");
        State = false;
        GetComponentInChildren<CircleCollider2D>().enabled = true;
    }
}
