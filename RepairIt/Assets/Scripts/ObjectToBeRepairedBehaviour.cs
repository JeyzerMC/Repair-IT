using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToBeRepairedBehaviour : MonoBehaviour, Analyzable
{
    private Interactror analyzedBy;

    // Update is called once per frame
    //void Update()
    //{
    //    if(analyzedBy != null)
    //    {
    //        analyzedBy.transform.position = analyzedByInitialPosition;
    //        analyzedBy.transform.rotation = analyzedByInitialRotation;
    //    }
    //}

    public void OnAnalyze(RepairObjectAnalyzer analyzer, Interactror interactror)
    {
        analyzedBy = interactror;
        interactror.GetComponent<Character>().Freezed = true;
    }

    public void OnAnalyzeFinished()
    {
        UnfreezePlayer();
    }

    public void OnAnalyzeCancelled()
    {
        UnfreezePlayer();
    }

    private void UnfreezePlayer()
    {
        analyzedBy.GetComponent<Character>().Freezed = false;
        analyzedBy = null;
    }
}
