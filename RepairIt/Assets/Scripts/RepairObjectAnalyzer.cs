using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairObjectAnalyzer : ObjectContainer
{
    [SerializeField]
    Transform depotSpot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override bool TryAddObject(Takable gameObject)
    {
        if(!gameObject.TryGetComponent<Analyzable>(out var analyzable))
        {
            Debug.LogError("Object is not Analyzable!");
            return false;
        }

        analyzable.OnAnalyze(this);
        gameObject.transform.position = depotSpot.position;
        gameObject.transform.rotation = depotSpot.rotation;

        return true;
    }

    protected override void OnInteractionImpl(Interactror interactror)
    {
        // TODO: Launch processing timer
    }
}
