using System.Collections.Generic;
using UnityEngine;

public class Level2_3ProgressController : MonoBehaviour
{
    public List<LeverController> Levers;
    [HideInInspector]
    public string _combination = "";
    private string _correctCombination = "213";
    void Start()
    {
        // Keep the handle inserted in the lever if it was done before
        if (Player.progress_5 == true)
        {
            EmptyLeverController emptyLever = FindObjectOfType<EmptyLeverController>();
            emptyLever.InsertHandle();
        }
        // Keep levers in ON state if the puzzle was done
        if (Player.progress_6 == true)
        {
            foreach (var lever in Levers)
                lever.Shift();
        }
    }

    void Update()
    {
        if (Player.progress_6 == false)
        {
            int leversShiftedCount = 0;
            foreach (var lever in Levers)
            {
                if (lever.State == true)
                    leversShiftedCount++;
            }
            if (leversShiftedCount == 3)
            {
                if (_combination == _correctCombination)
                {
                    if (Player.progress_6 == false)
                        FindObjectOfType<AudioManager>().Play("GateOpen");
                    Player.progress_6 = true;
                }
                else
                {
                    foreach (var lever in Levers)
                        lever.ResetState();
                    _combination = "";
                }
            }
        }
    }
}
