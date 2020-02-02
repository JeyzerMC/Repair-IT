using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropoff : ObjectContainer
{
    [SerializeField]
    Transform itemSpawnPosition = null;

    [SerializeField]
    Animator characterAnimator = null;

    [SerializeField]
    Takable characterComputer = null;

    Takable computer = null;

    private bool _orderAwaiting;

    private bool _isOpen; 

    private float _availabilityTime;

    // Start is called before the first frame update
    void Start()
    {
        if (itemSpawnPosition == null)
        {
            Debug.LogError("Dropoff requires an itemSpawnPosition");
        }

        _orderAwaiting = false;
        _isOpen = false;
        _availabilityTime = 0;
    }

    void Update()
    {
        if (_isOpen && !_orderAwaiting && Time.time > _availabilityTime)
        {
            StartCoroutine(MakeCustomerArrive());
        }
    }

    public IEnumerator MakeCustomerArrive()
    {
        _orderAwaiting = true;
        characterAnimator.SetTrigger("New Order");
        yield return new WaitForSeconds(1);
        computer = Instantiate(characterComputer, itemSpawnPosition.position, itemSpawnPosition.rotation, transform);
        // TODO: Generate random requirements
        containedObjects.Add(computer);
        computer.EnsurePlaced();
    }

    public void MakeCustomerLeave()
    {
        if (_orderAwaiting)
        {
            Debug.Log("Arriving....");
            characterAnimator.SetTrigger("Order Done");
            if (computer != null)
            {
                Destroy(computer);
            }

            _orderAwaiting = false;
            int rTime = Random.Range(5, 10);
            _availabilityTime = Time.time + rTime;
        }
    }

    public void Open()
    {
        Debug.Log("Dropoff is open!");
        _isOpen = true;
    }

    public bool IsDropoffOpen()
    {
        return _isOpen;
    }

    public bool IsOrderWaiting()
    {
        return _orderAwaiting;
    }

    protected override bool TryAddObject(Takable gameObject, Interactror interactror)
    {
        // Verify if this is the correct script
        if (gameObject == computer)
        {
            return true;
        }
        return false;
    }
}
