
using UnityEngine;

public abstract class Interactee : MonoBehaviour
{
    /// <summary>
    /// Gets called when an Interactor tries to interact with the current interactee
    /// </summary>
    /// <param name="interactror">The interactor that is trying to interact with us</param>
    /// <returns>Whether the interaction "succeeded" (a.k.a , am I holding the object or not)</returns>
    public abstract void OnInteraction(Interactror interactror);
}
