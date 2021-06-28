using UnityEngine;

public class SOGunControlsYt : MonoBehaviour
{
    public MouseLook mouseCam;
    public SOGunControlsYt sOGunControlsThis;
    public RecoilControlsYt recoilControls;
    public CameraController cameraController;
    [Header("R To Reload, T To Toggle FireSelectMode")]

    [Header("The Weapons Variables")]
    public S_O_GunYt gunVariables;// Use A ScriptableObject//
    public int shotAmountPerBullet = 5;
    public int bulletFired = 0;
    public float bulletSpread = 5f;
    public float shotgunRechamberTime = 2;
    public float BoltActionRechamberTime = 1f;

    [Header("Ads Reference Position")]
    public ItemLocation itemLocation;// USED FOR ADS LOCATIONS//

    [Header("This Weapons Rigidbody")]
    public Rigidbody GunRb;

    [Header("GameObject References")]
    public GameObject GunMesh;
    public GameObject bulletPrefab; //THE BULLET PREFAB//

    public GameObject muzzleFlash;

    [Header("Transform References")]
    public Transform muzzleFlashPoint; //THE SPAWN POINT OF THE FLASH//
    public Transform bulletStartPointTform; //THE SPAWN POINT OF THE BULLET//

    [Header("Current Bullet Rigidbody")]
    public Rigidbody bulletRb;       // THE RIGIDBODY OF THE BULLET//

    [Header("Gun Audio")]

    public AudioSource gunShotSound;// SOUND OF THE WEAPON WHEN SHOOTING;


    [Header("Changing Gun Bools")]
    public bool AbleToFire = false; // IS THE WEAPON READY TO FIRE//
    public bool selectFireAutomatic = true;
    public bool CurrentlyReloading = false;
    public bool Equipped = false;
    public bool IsShooting = false;
    public bool CurrentlyADS = false;
    public bool hasAmmo = true;
    [Header("Static Gun Bools")] //Set These When You First Make The Gun//
    public bool semiAutomatic = false;//IS THE WEAPON SEMI AUTO//
    public bool boltAction = false;//IS THE WEAPON BOLT ACTION//
    public bool fullAuto = false;//IS THE WEAPON AUTOMATIC//
    public bool canSelectFire = false;//CHOOSE IF THE WEAPON HAS SELECT FIRE MODE//
    public bool shotgun = false;

    public void Start()
    {
        sOGunControlsThis = gameObject.GetComponent<SOGunControlsYt>();
        itemLocation = gameObject.GetComponent<ItemLocation>();
        GunRb = gameObject.GetComponent<Rigidbody>();
        cameraController.currentFOV = cameraController.OriginalFOV;
        bulletStartPointTform = GameObject.Find("BulletStartPoint").transform;
    }
    private void Update()
    {

        UpdateFunctions();
    }
    public void UpdateFunctions()
    {
        if (Equipped == true)
        {
           
            
            AmmunitionCheck();
            ReloadWeapon();
            FireButtonPressedAndReleased();
            FireSelectMode();
            AimDownSight();
        }
    }
    public void StartShooting()
    {
        if (selectFireAutomatic == true && Equipped == true)
        {
            
            InvokeRepeating("Shooting", gunVariables.DelayBeforeFirstShot, gunVariables.fireRate);

        }
        else if (selectFireAutomatic == false && Equipped == true)
        {
           
            Shooting();
        }
    }
    public void StopShooting()
    {
        IsShooting = false;
        CancelInvoke("Shooting");
    }
    public void Shooting()
    {
        recoilControls.AddRecoil();
        IsShooting = true;
        if (boltAction == true && CurrentlyReloading == false)
        {
            BoltActionLogic();
        }
        if (CurrentlyReloading == false)
        {
            if (shotgun == true)
            {
                ShotgunLogic();

            }
            else
            {
                BulletLogic();
            }
        }
    }
    public void AmmunitionCheck()
    {
        if (gunVariables.currentAmmo <= 0)
        {
            hasAmmo = false;
            AbleToFire = false;
        }
        if (gunVariables.currentAmmo <= 0 || CurrentlyReloading == true)
        {
            CancelInvoke("Shooting");
            AbleToFire = false;
        }
        else if (gunVariables.currentAmmo >= 1 && CurrentlyReloading == false)
        {
            hasAmmo = true;
            AbleToFire = true;
        }
    }
    public void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CurrentlyReloading = true;
            //Reloading.Play();
            Invoke("FillAmmo", gunVariables.reloadTime);
        }
    }
    public void FireButtonPressedAndReleased()
    {
        if (Input.GetButtonDown("Fire1") && AbleToFire == true)
        {
            StartShooting();

        }
        else if (Input.GetButtonDown("Fire1") && AbleToFire == false)
        {
            //gunOutOfAmmo.Play();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopShooting();

        }
    }
    public void FireSelectMode()
    {
        if (Input.GetKeyDown(KeyCode.T) && canSelectFire == true)
        {
            if (selectFireAutomatic == true)
            {
                selectFireAutomatic = false;
            }
            else if (selectFireAutomatic == false)
            {
                selectFireAutomatic = true;
            }
        }
    }
    public void FillAmmo()
    {
        CurrentlyReloading = false;
        gunVariables.currentAmmo = gunVariables.maxAmmo;
    }
    public void BoltActionChamberRound()
    {
        //RechamberSound.Play();        
        Invoke(nameof(RoundInChamber), BoltActionRechamberTime);
        CurrentlyReloading = true;
    }
    public void RoundInChamber()
    {
        CurrentlyReloading = false;
    }
    public void AimDownSight()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && CurrentlyADS == false)
        {
            cameraController.currentFOV = cameraController.ADS_FOV;
            Camera.main.fieldOfView = cameraController.ADS_FOV;
            CurrentlyADS = true;

            GunRb.transform.localPosition = itemLocation.ADSPosition;
            GunRb.transform.localEulerAngles = itemLocation.ADSRotation;

        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && CurrentlyADS == true)
        {
            cameraController.currentFOV = cameraController.OriginalFOV;
            Camera.main.fieldOfView = cameraController.OriginalFOV;
            CurrentlyADS = false;

            GunRb.transform.localPosition = itemLocation.equippedPosition;
            GunRb.transform.localEulerAngles = itemLocation.equippedRotation;

        }

    }
    public void ShotgunLogic()
    {
        if (bulletFired == shotAmountPerBullet)
        {
            MuzzleFlashLogic();
            StopShooting();
            bulletFired = 0;
            gunVariables.currentAmmo -= 1;
            ShotgunChamberRound();

        }
        else if (bulletFired <= shotAmountPerBullet && AbleToFire == true)
        {
            bulletFired += 1;
            Invoke("Shooting", 0);
        }

        Vector3 dir = new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
        var bullet = Instantiate(bulletPrefab, bulletStartPointTform.position, bulletStartPointTform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletStartPointTform.forward * gunVariables.bulletForce, ForceMode.Impulse);
        rb.AddForce(dir * gunVariables.bulletForce, ForceMode.Acceleration);



        Destroy(bullet, 6f);
        gunShotSound.Play();



    }
    public void ShotgunChamberRound()
    {
        if (gunVariables.currentAmmo >= 1 && IsShooting == false && CurrentlyReloading == false)
        {
            //RechamberSound.Play();
            Invoke(nameof(RoundInChamber), shotgunRechamberTime);

            CurrentlyReloading = true;
        }
    }
    public void BoltActionLogic()
    {

        {
            Invoke(nameof(BoltActionChamberRound), 0f);
        }
    }
    public void BulletLogic()
    {
        var bullet = Instantiate(bulletPrefab, bulletStartPointTform.position, bulletStartPointTform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletStartPointTform.forward * gunVariables.bulletForce, ForceMode.Impulse);

        MuzzleFlashLogic();

        Destroy(bullet, 6f);
        gunShotSound.Play();
        gunVariables.currentAmmo -= 1;

    }
    public void MuzzleFlashLogic()
    {
        var Flash = Instantiate(muzzleFlash,
                    new Vector3(muzzleFlashPoint.transform.position.x, muzzleFlashPoint.transform.position.y, muzzleFlashPoint.transform.position.z),
                    Quaternion.identity);

        Flash.transform.parent = muzzleFlashPoint.transform;
        Flash.transform.rotation = mouseCam.transform.rotation;
        Destroy(Flash, .2f);
    }
   

}
