using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RepairObjectAnalyzer : ObjectContainer
{
    [SerializeField]
    Transform depotSpot;

    [SerializeField]
    public float AnalyzeTime = 10f;

    private float currentTime = 0f;
    private bool AnalyzeFinished = false;
    private bool ContainsObject { get { return containedObjects.Count > 0; } }

    private ProgressBar progressBar;
    private GameObject progressionUI;

    void Start()
    {
        progressBar = GetComponentInChildren<ProgressBar>();
        progressionUI = transform.Find("ProgressionUI").gameObject;
        progressionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!AnalyzeFinished && ContainsObject && currentTime < AnalyzeTime)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= AnalyzeTime)
            {
                AnalyzeFinished = true;
                currentTime = 0;
                containedObjects[containedObjects.Count - 1].GetComponent<Analyzable>().OnAnalyzeFinished();
            }

            progressBar.Progress = currentTime / AnalyzeTime;
            Debug.Log(currentTime + " --> " + currentTime / AnalyzeTime);
        }
    }

    protected override bool TryAddObject(Takable gameObject, Interactror interactor)
    {
        if (!gameObject.TryGetComponent<Analyzable>(out var analyzable))
        {
            Debug.LogError("Object is not Analyzable!");
            return false;
        }

        if(containedObjects.Count == 1)
        {
            Debug.Log("There is already an object in the Analyzer!");
            return false;
        }

        progressBar.Progress = 0;
        progressionUI.SetActive(true);
        analyzable.OnAnalyze(this, interactor);
        gameObject.transform.position = depotSpot.position;
        gameObject.transform.rotation = depotSpot.rotation;

        return true;
    }

    protected override void OnInteractionImpl(Interactror interactror)
    {
        if(currentTime > 0 && !AnalyzeFinished)
        {
            Debug.LogError("CANCELING TIMER!");
            currentTime = 0;
            progressBar.Progress = 0;
            interactror.heldObject.GetComponent<Analyzable>().OnAnalyzeCancelled();
        }

        if(!ContainsObject)
        {
            progressionUI.SetActive(false);
        }
        AnalyzeFinished = false;
    }
}
