using UnityEngine;

public class RecoilControlsYt : MonoBehaviour
{
    public MouseLook mouseLook;
    public SOGunControlsYt sOGunControls;
    public Transform cam;

    public float recoilUpAmount;
   
    public float recoilLeftAmountmin; 
    public float recoilLeftAmountmax;

    public float recoilRightAmountMin;
    public float recoilRightAmountMax;


    private Vector3 upRecoilamountV;
    private Vector3 leftRecoilamountV;
    private Vector3 RightRecoilamountV;

    private void Start()
    {
        upRecoilamountV = new Vector3(recoilUpAmount, 0, 0);
        leftRecoilamountV = new Vector3(0, Random.Range(-recoilLeftAmountmin, -recoilLeftAmountmax), 0);
        RightRecoilamountV = new Vector3(0, Random.Range(recoilRightAmountMin, recoilRightAmountMax), 0);
    }
    
    public void AddRecoil()
    {
        UpRecoil();
        LeftRecoil();
        RightRecoil();
    }
    public void UpRecoil()
    {
        float angleRecoilUpDistanceF = recoilUpAmount * Time.deltaTime;
        cam.transform.localRotation *= Quaternion.AngleAxis(angleRecoilUpDistanceF, upRecoilamountV); 
        
        mouseLook._mouseAbsolute.y += angleRecoilUpDistanceF;

    }
    public void LeftRecoil()
    {
        float angleRecoilLeft = Random.Range(-recoilLeftAmountmin, -recoilLeftAmountmax) * Time.deltaTime;
        
        cam.transform.localRotation *= Quaternion.AngleAxis(angleRecoilLeft, leftRecoilamountV);      
        mouseLook._mouseAbsolute.x += angleRecoilLeft;
       
    }
    public void RightRecoil()
    {
        float angleRecoilRight = Random.Range(recoilRightAmountMin, recoilRightAmountMax) * Time.deltaTime;

        cam.transform.localRotation *= Quaternion.AngleAxis(angleRecoilRight, RightRecoilamountV); 
        mouseLook._mouseAbsolute.x += angleRecoilRight;

    }
}




