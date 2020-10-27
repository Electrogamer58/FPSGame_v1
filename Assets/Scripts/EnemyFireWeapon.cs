using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWeapon : MonoBehaviour
{
    [SerializeField] Enemy enemyController;
    [SerializeField] Transform eye;
    //[SerializeField] float rayDistance = 15;
    [SerializeField] GameObject hitPlayerFeedback;
    [SerializeField] GameObject hitEnvironmentFeedback;
    //[SerializeField] int weaponDamage = 1;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioSource bulletSound;
    

    public float spreadFactor = 0.1f;
    float fireRate = 5;

    public float nextFire = 4;

    RaycastHit hitInfo;

    //fire the weapon
    public void ShootBullet()
    {
        if (Time.time > nextFire)
        {

            //nextFire = Time.time + fireRate;
            bulletSound.PlayOneShot(bulletSound.clip, 1);
            Instantiate(bullet, eye.position, Quaternion.identity);
            Debug.Log("Enemy Shoot!");
            //Instantiate(hitPlayerFeedback, transform.position, Quaternion.identity);
            //Instantiate(hitEnvironmentFeedback, transform.position, Quaternion.identity);
            enemyController.chargeParticles.Stop();
            Debug.Log("Particles Stopped");
            nextFire = Time.time + fireRate;
            
        }
    }
}
