using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Gun")]
public class S_O_GunYt : ScriptableObject
{
    public float bulletForce; //THE AMOUNT OF FORCE APPLIED TO THE BULLET//
    public float fireRate = 0.5F; //TIME BETWEEN BULLETS BEING FIRED//
    public float reloadTime = 1f; //TIME TO RELOAD WEAPON//
    public float DelayBeforeFirstShot = 0f; //TIME DELAY BEFORE FIRST SHOT//
    public float currentAmmo;
    public float maxAmmo = 30f;

    public float recoilUpDistance;

    public float recoilLeftAmountmin;
    public float recoilLeftAmountmax;

    public float recoilRightAmountMin;
    public float recoilRightAmountMax;



}
