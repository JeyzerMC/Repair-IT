using UnityEngine;
using UnityEngine.UI;

public class CustomRenderQueue : MonoBehaviour
{

    void Start()
    {
        Image image = GetComponent<Image>();
        Material existingGlobalMat = image.materialForRendering;
        Material updatedMaterial = new Material(existingGlobalMat);
        updatedMaterial.SetInt("unity_GUIZTestMode", (int)UnityEngine.Rendering.CompareFunction.Always);
        image.material = updatedMaterial;
    }
}
