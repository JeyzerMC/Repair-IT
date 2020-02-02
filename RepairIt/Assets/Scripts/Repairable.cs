using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Takable))]
public class Repairable : MonoBehaviour
{
    public List<string> requirements;
    [NonSerialized]
    public Takable takable;

    private void Start()
    {
        takable = GetComponent<Takable>();
    }
}
