using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCount : MonoBehaviour
{
    public int childCount;

    private void Awake()
    {
        this.childCount = transform.childCount;
    }
}