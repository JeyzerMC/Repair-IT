using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    [SerializeField]
    public Camera Camera;

    [SerializeField]
    public float RotationX = 0;

    [SerializeField]
    public float RotationY = 0;

    [SerializeField]
    public float RotationZ = 0;

    [SerializeField]
    public bool DynamicUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        LookAtCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (DynamicUpdate)
        {
            LookAtCamera();
        }
    }

    private void LookAtCamera()
    {
        if (Camera == null)
        {
            this.transform.LookAt(Camera.main.transform.position);
            this.transform.rotation = Quaternion.Euler(RotationX, RotationY, RotationZ);
        }
        else
        {
            this.transform.LookAt(Camera.transform.position);
            this.transform.rotation = Quaternion.Euler(RotationX, RotationY, RotationZ);
        }
    }
}
