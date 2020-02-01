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

    GameObject computer = null;

    private bool _orderAwaiting;

    private bool _isOpen;

    // Start is called before the first frame update
    void Start()
    {
        if (itemSpawnPosition == null)
        {
            Debug.LogError("Dropoff requires an itemSpawnPosition");
        }

        _orderAwaiting = false;
        _isOpen = false;
    }

    void Update()
    {
        if (_isOpen && !_orderAwaiting)
        {
            StartCoroutine(MakeCustomerArrive());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            MakeCustomerLeave();
        }
    }

    public IEnumerator MakeCustomerArrive()
    {
        _orderAwaiting = true;
        characterAnimator.SetTrigger("New Order");
        yield return new WaitForSeconds(1);
        computer = Instantiate(characterComputer, itemSpawnPosition.position, itemSpawnPosition.rotation);
    }

    public void MakeCustomerLeave()
    {
        if (_orderAwaiting)
        {
            _orderAwaiting = false;
            characterAnimator.SetTrigger("Order Done");
            if (computer != null)
            {
                Destroy(computer);
            }
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
}
