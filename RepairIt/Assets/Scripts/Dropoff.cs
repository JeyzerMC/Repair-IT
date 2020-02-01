using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropoff : MonoBehaviour
{
    [SerializeField]
    Transform itemSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (itemSpawnPosition == null)
        {
            Debug.LogError("Dropoff requires an itemSpawnPosition");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeCustomerArrive()
    {

    }
}
