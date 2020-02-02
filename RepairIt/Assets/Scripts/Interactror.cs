using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Interactror : MonoBehaviour
{
    public const float MAX_RAYCAST_DISTANCE = 1.2f;

    private PlayerInput input;
    [NonSerialized]
    public Takable heldObject = null;
    [NonSerialized]
    public Transform Hands;

    public float minWhiskerAngle = 65;
    public float maxWhiskerAngle = 90;
    public int whiskerNumber = 5;

    public bool IsHoldingObject { get { return heldObject != null; } private set { } }

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        Hands = transform.Find("Hands");
        if (Hands == null)
        {
            Debug.LogError("Interactor doesn't have \"Hands\"");
        }
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetPlayerButtonDown("Fire1"))
        {
            TryInteract();
            if (_anim != null)
            {
                _anim.SetTrigger("Pickup");
            }
        }

        RaycastWhiskers(true, Hands.position,
            Quaternion.AngleAxis(minWhiskerAngle, Hands.right) * Hands.forward * MAX_RAYCAST_DISTANCE,
            Quaternion.AngleAxis(maxWhiskerAngle, Hands.right) * Hands.forward * MAX_RAYCAST_DISTANCE,
            whiskerNumber);
    }

    void TryInteract()
    {
        var hits = RaycastWhiskers(false, Hands.position,
            Quaternion.AngleAxis(minWhiskerAngle, Hands.right) * Hands.forward * MAX_RAYCAST_DISTANCE,
            Quaternion.AngleAxis(maxWhiskerAngle, Hands.right) * Hands.forward * MAX_RAYCAST_DISTANCE,
            whiskerNumber);
        Debug.Log($"{hits.Count()} Hits!");

        Interactee target = null;

        foreach (var hit in hits)
        {
            Debug.Log("Testing hit at distance: " + hit.distance);
            var collider = hit.collider;
            if (IsHoldingObject && collider.TryGetComponent<ObjectContainer>(out var container))
            {
                target = container;
                break;
            }
            else if (collider.TryGetComponent<Interactee>(out var interactee))
            {
                target = interactee;
                break;
            }
        }

        if (target != null)
        {
            target.OnInteraction(this);
        }
        else if (IsHoldingObject)
        {
            heldObject.OnInteraction(this);
        }
    }

    public IEnumerable<RaycastHit> RaycastWhiskers(bool drawOnly, Vector3 position, Vector3 fromDirection, Vector3 toDirection, int number)
    {
        List<RaycastHit> hits = new List<RaycastHit>();

        for (int i = 0; i < number; ++i)
        {
            float progress = i / ((float)number - 1);
            Vector3 direction = Vector3.Slerp(fromDirection, toDirection, progress);
            Debug.DrawRay(
                position,
                direction,
                Color.Lerp(Color.red, Color.white, progress),
                1, // 1 second display for debugging
                false);
            if (!drawOnly)
            {
                var hit = Physics.RaycastAll(position, direction, direction.magnitude).OrderBy(x => x.distance);
                hits.AddRange(hit);
            }

        }

        return hits.Distinct(new DistinctRaycastHitComparer());
    }

    class DistinctRaycastHitComparer : IEqualityComparer<RaycastHit>
    {
        public bool Equals(RaycastHit x, RaycastHit y)
        {
            return x.transform == y.transform;
        }

        public int GetHashCode(RaycastHit obj)
        {
            return obj.transform.GetHashCode();
        }
    }
}
