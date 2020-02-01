using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractee : MonoBehaviour, Interactee
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnInteraction(Interactror interactror)
    {
        Debug.Log("GOT INTERACTED WITH!");
        Destroy(gameObject);
    }
}
