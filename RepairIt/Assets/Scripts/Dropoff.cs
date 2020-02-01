using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropoff : MonoBehaviour
{
    [SerializeField]
    Transform itemSpawnPosition = null;
    [SerializeField]
    Animator characterAnimator = null;
    [SerializeField]
    GameObject characterComputer = null;

    // Start is called before the first frame update
    void Start()
    {
        if (itemSpawnPosition == null)
        {
            Debug.LogError("Dropoff requires an itemSpawnPosition");
        }
    }

    public IEnumerator MakeCustomerArrive()
    {
        characterAnimator.SetTrigger("New Order");
        yield return new WaitForSeconds(1);
        Instantiate(characterComputer, itemSpawnPosition.position, itemSpawnPosition.rotation);
    }

    public void MakeCustomerLeave()
    {
        characterAnimator.SetTrigger("Order Done");
        Instantiate(characterComputer, itemSpawnPosition.position, itemSpawnPosition.rotation);
    }
}
