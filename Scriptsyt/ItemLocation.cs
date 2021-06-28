using UnityEngine;

public class ItemLocation : MonoBehaviour
{
    [Space(10)]
    public Vector3 equippedPosition;
    public Vector3 equippedRotation;
    [Space(10)]
    public Vector3 ADSPosition;
    public Vector3 ADSRotation;

    public float XPosition, YPosition, ZPosition;
    public float XRotation, YRotation, ZRotation;

    public float XADSPosition, YADSPosition, ZADSPosition;
    public float XADSRotation, YADSRotation, ZADSRotation;

    private void Start()
    {
        equippedPosition = new Vector3(XPosition, YPosition, ZPosition);
        equippedRotation = new Vector3(XRotation, YRotation, ZRotation);

        ADSPosition = new Vector3(XADSPosition, YADSPosition, ZADSPosition);
        ADSRotation = new Vector3(XADSRotation, YADSRotation, ZADSRotation);
    }

}


