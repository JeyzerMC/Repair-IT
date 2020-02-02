using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class OrderStation : Interactee
{
    [SerializeField]
    DeliveryboxBehaviour boxPrefab = null;
    TruckThrower _truckThrower;

    [SerializeField]
    Takable content;

    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        // Lets find the truck thrower
        _truckThrower = GameObject.FindObjectOfType<TruckThrower>();

        var constraint = GetComponentInChildren<LookAtConstraint>();
        ConstraintSource source = new ConstraintSource();
        source.weight = 1;
        source.sourceTransform = Camera.main.transform;
        constraint.SetSources(new List<ConstraintSource> { source });

        var text = GetComponentInChildren<Text>();
        text.text = content.tag;

        _anim = GetComponentInChildren<Animator>();
    }

    public override void OnInteraction(Interactror interactror)
    {
        _anim.SetTrigger("Order");
        _truckThrower.ThrowBox(boxPrefab.GetComponent<Rigidbody>()).GetComponent<DeliveryboxBehaviour>().content = content;
    }
}
