using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Interactror : MonoBehaviour
{
    public const float MAX_RAYCAST_DISTANCE = 0.9f;
    private PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetPlayerButtonDown("Fire1"))
        {
            TryInteract();
        }
        Debug.DrawRay(transform.position, transform.forward * MAX_RAYCAST_DISTANCE, Color.red, 0, false);
    }

    void TryInteract()
    {
        Debug.Log("Trying to interact");
        foreach (var hit in Physics.RaycastAll(transform.position, transform.forward, MAX_RAYCAST_DISTANCE).OrderBy(x => x.distance))
        {
            Debug.Log("Testing hit at distance: "+hit.distance);
            var collider = hit.collider;
            if (collider.TryGetComponent<Interactee>(out var interactee))
            {
                interactee.OnInteraction(this);
                break;
            }
        }
    }
}
