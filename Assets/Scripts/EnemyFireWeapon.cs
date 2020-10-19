using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWeapon : MonoBehaviour
{
    [SerializeField] Enemy enemyController;
    [SerializeField] Transform eye;
    [SerializeField] float rayDistance = 15;
    [SerializeField] GameObject hitPlayerFeedback;
    [SerializeField] GameObject hitEnvironmentFeedback;
    [SerializeField] int weaponDamage = 1;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] GameObject bullet;

    public float spreadFactor = 0.1f;
    float fireRate = 5;

    public float nextFire = 4;

    RaycastHit hitInfo;


    //void Update()
    //{
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
            //Shoot();
        //}
    //}

    //fire the weapon
    public void ShootBullet()
    {
        if (Time.time > nextFire)
        {

            nextFire = Time.time + fireRate;
            Instantiate(bullet, transform.position, Quaternion.identity);
            Instantiate(hitPlayerFeedback, transform.position, Quaternion.identity);
            Instantiate(hitEnvironmentFeedback, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
            
        }
    }
}
