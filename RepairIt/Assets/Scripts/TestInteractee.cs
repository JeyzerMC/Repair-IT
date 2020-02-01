using UnityEngine;

public class TestInteractee : MonoBehaviour, Interactee
{

    private bool isPickedUp = false;

    public bool OnInteraction(Interactror interactror)
    {
        Debug.Log(transform.parent);
        if (!isPickedUp)
        {
            Debug.Log("I got picked up!");
            transform.parent = interactror.transform;

            // TODO: DISABLE THE RIGIDBODY COMPONENT (Disable != Delete)
            isPickedUp = true;
            return true;
        }

        isPickedUp = false;
        transform.parent = null;

        return false;
    }
}
