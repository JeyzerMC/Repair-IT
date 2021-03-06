﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{

    private float[] spawnTimes;

    private Dropoff[] dropoffs;

    private int nbDropoffs;

    private List<int> waitingDropoffs;

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

        waitingDropoffs = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nbDropoffs < dropoffs.Length && Time.time > spawnTimes[nbDropoffs])
        {
            Debug.Log("Opening random dropoff");

            int idx = Random.Range(0, dropoffs.Length);

            if (!dropoffs[idx].IsDropoffOpen())
            {
                dropoffs[idx].Open();
                waitingDropoffs.Add(idx);
                nbDropoffs++;
            }
        }

        if (Input.GetKeyDown(KeyCode.L) && waitingDropoffs.Count > 0)
        {
            int idx = Random.Range(0, waitingDropoffs.Count);
            int customer = waitingDropoffs[idx];
            Debug.Log($"Removing customer #{customer}");
            dropoffs[customer].MakeCustomerLeave();
            waitingDropoffs.RemoveAt(idx);
        }
    }

    public List<string> GenerateRequirements()
    {
        List<string> requirements = new List<string>();
        int difficulty = 4;
        int numberOfRequirements = Random.Range(1, difficulty);
        string[] possibilities = { "Ram", "Cpu", "Psu", "Fan", "Wire" };
        for (int i = 0; i < numberOfRequirements; ++i)
        {
            requirements.Add(possibilities[Random.Range(0, possibilities.Length)]);
        }

        return requirements;
    }
}
