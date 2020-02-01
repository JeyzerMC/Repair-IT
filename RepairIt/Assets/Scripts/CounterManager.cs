﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{

    private float[] spawnTimes;

    private Dropoff[] dropoffs;

    private int nbDropoffs;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimes = new float[] 
        {
            5.0f,
            10.0f,
            15.0f,
            20.0f,
            30.0f
        };

        dropoffs = FindObjectsOfType<Dropoff>();

        nbDropoffs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (nbDropoffs < 5 && Time.time > spawnTimes[nbDropoffs])
        {
            Debug.Log("Opening random dropoff");

            int idx = Random.Range(0, 5);

            if (!dropoffs[idx].IsDropoffOpen())
            {
                dropoffs[idx].Open();
                nbDropoffs++;
            }
        }
    }
}