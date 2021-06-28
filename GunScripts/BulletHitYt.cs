using UnityEngine;

public class BulletHitYt : MonoBehaviour
{

    public GameObject bullet;

    public GameObject enemyHit;
    public GameObject metalHit;
    public GameObject concreteHit;
    public GameObject woodHit;
    public GameObject dirtHit;


    public float bulletDamage = 5f;


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Instantiate(enemyHit, new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z), Quaternion.identity);
            //EnemyHealth = collision.gameObject.GetComponentInChildren<EnemyHealthStat>();
            //ai = collision.gameObject.GetComponentInChildren<AI>();
            //EnemyHealth.EnemyHealthCheck();
            //EnemyHealth.EnemyDeath();
            //EnemyHealth.damage = bulletDamage;
            //EnemyHealth.ai = ai;
        }
        
        else if (collision.gameObject.CompareTag("Metal"))
        {
            Instantiate(metalHit, new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z), Quaternion.identity);

        }
        else if (collision.gameObject.CompareTag("Concrete"))
        {
            Instantiate(concreteHit, new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z), Quaternion.identity);

        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            Instantiate(woodHit, new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z), Quaternion.identity);

        }
        else if (collision.gameObject.CompareTag("Dirt"))
        {
            Instantiate(dirtHit, new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z), Quaternion.identity);
        }
        Destroy(bullet);
    }
}