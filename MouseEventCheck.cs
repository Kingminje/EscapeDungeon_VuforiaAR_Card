using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventCheck : MonoBehaviour
{
    public bool mouseTrigger = false;

    public ImageRollTheDice[] dices = null;

    private void OnMouseDown()
    {
        if (mouseTrigger == true)
        {
            for (int i = 0; i < dices.Length; i++)
            {
                dices[i].StartDiceRoll();
            }
            mouseTrigger = false;
        }
    }
}