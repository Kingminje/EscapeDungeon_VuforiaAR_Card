using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCardDB : MonoBehaviour
{
    public int cardPower = 0;

    private int power = 0;

    public int totalePower
    {
        get { return this.power; }
        set { power = value; }
    }
}