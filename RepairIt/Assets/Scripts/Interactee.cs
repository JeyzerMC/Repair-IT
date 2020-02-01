public interface Interactee
{
    /// <summary>
    /// Gets called when an Interactor tries to interact with the current interactee
    /// </summary>
    /// <param name="interactror">The interactor that is trying to interact with us</param>
    void OnInteraction(Interactror interactror);
}
