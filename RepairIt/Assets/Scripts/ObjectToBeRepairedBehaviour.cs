using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToBeRepairedBehaviour : MonoBehaviour, Analyzable
{
    private Vector3 analyzedByInitialPosition;
    private Quaternion analyzedByInitialRotation;
    private Interactror analyzedBy;
    private RepairObjectAnalyzer analyzer;

    // Update is called once per frame
    void Update()
    {
        if(analyzedBy != null)
        {
            analyzedBy.transform.position = analyzedByInitialPosition;
            analyzedBy.transform.rotation = analyzedByInitialRotation;
        }
    }
    public void OnAnalyze(RepairObjectAnalyzer analyzer, Interactror interactror)
    {
        this.analyzer = analyzer;
        analyzedBy = interactror;
        analyzedByInitialPosition = analyzedBy.transform.position;
        analyzedByInitialRotation = analyzedBy.transform.rotation;
    }

    public void OnAnalyzeFinished()
    {
        analyzedBy = null;
    }

    public void OnAnalyzeCancelled()
    {
        analyzedBy = null;
    }
}
