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
    public float spreadFactor = 0.2f;
    float fireRate = 5;

    public float reloadTime = 4;

    RaycastHit hitInfo;


    //void Update()
    //{
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
            //Shoot();
        //}
    //}

    //fire the weapon
    public void Shoot()
    {
        if (Time.time > reloadTime)
        {
            reloadTime = Time.time + fireRate;
            //calculate direction to shoot ray
            Vector3 rayDirection = enemyController.transform.forward;
            //rayDirection.x += Random.Range(-spreadFactor, spreadFactor);
            //rayDirection.y += Random.Range(-spreadFactor, spreadFactor);
            //rayDirection.z += Random.Range(-spreadFactor, spreadFactor);
            //cast debug ray
            Debug.DrawRay(eye.position, rayDirection * rayDistance, Color.red, 1);
            //cast real raycast
            if (Physics.Raycast(eye.position, rayDirection, out hitInfo, rayDistance, hitLayers))
            {
                Debug.Log("Hit a " + hitInfo.transform.name + "!");
                if (hitInfo.transform.tag == "Player")
                {
                    hitPlayerFeedback.transform.position = hitInfo.point;
                    ParticleSystem bloodspurt = hitPlayerFeedback.GetComponent<ParticleSystem>();
                    bloodspurt.Play();
                    PlayerMovementScript playerHit = hitInfo.transform.gameObject.GetComponent<PlayerMovementScript>();
                    if (playerHit != null)
                    {
                        playerHit.DamageTaken(weaponDamage);
                    }
                } //TODO add wall visual feedback
                if (hitInfo.transform.tag == "Wall")
                {
                    hitEnvironmentFeedback.transform.position = hitInfo.point;
                    ParticleSystem damageSpurt = hitEnvironmentFeedback.GetComponent<ParticleSystem>();
                    damageSpurt.Play();
                }


            }
            else
            {
                Debug.Log("Missed!");
            }
            
        }
    }
}
