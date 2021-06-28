using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Controls")]
    public Camera Cam;
    [Range(0, 100)]
    public float OriginalFOV;
    [Range(0, 100)]
    public float ADS_FOV = 40;
    public float currentFOV;


    public void Start()
    {
        OriginalFOV = Camera.main.fieldOfView;
    }
}
