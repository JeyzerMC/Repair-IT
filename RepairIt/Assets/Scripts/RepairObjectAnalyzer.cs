using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairObjectAnalyzer : ObjectContainer
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override bool TryAddObject(GameObject gameObject)
    {
        if(!gameObject.TryGetComponent<Analyzable>(out var analyzable))
        {
            Debug.LogError("Object is not Analyzable!");
            return false;
        }

        // He becomes our Child
        gameObject.transform.parent = transform;

        analyzable.OnAnalyze(this);

        return true;
    }

    public override bool OnInteraction(Interactror interactror)
    {
        // We don't have any object
        if(containedObjects.Count == 0)
        {
            return false;
        }

        // Give the object to the interactor
        interactror.InteractWith(containedObjects[0]);

        return false;
    }
}
