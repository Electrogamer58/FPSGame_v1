using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform barrelEnd;
    [SerializeField] float rayDistance = 25;
    [SerializeField] GameObject hitEnemyFeedback;
    [SerializeField] GameObject hitEnvironmentFeedback;
    [SerializeField] int weaponDamage = 15;
    [SerializeField] LayerMask hitLayers;
    public float spreadFactor = 0.2f;

    RaycastHit hitInfo;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    //fire the weapon
    public void Shoot()
    {
        //calculate direction to shoot ray
        Vector3 rayDirection = cameraController.transform.forward;
        rayDirection.x += Random.Range(-spreadFactor, spreadFactor);
        rayDirection.y += Random.Range(-spreadFactor, spreadFactor);
        rayDirection.z += Random.Range(-spreadFactor, spreadFactor);
        //cast debug ray
        Debug.DrawRay(barrelEnd.position, rayDirection * rayDistance, Color.blue, 1);
        //cast real raycast
        if (Physics.Raycast(barrelEnd.position, rayDirection, out hitInfo, rayDistance, hitLayers))
        {
            Debug.Log("Hit a " + hitInfo.transform.name + "!");
            if (hitInfo.transform.tag == "Enemy")
            {
                hitEnemyFeedback.transform.position = hitInfo.point;
                ParticleSystem bloodspurt = hitEnemyFeedback.GetComponent<ParticleSystem>();
                bloodspurt.Play();
                Enemy enemyHit = hitInfo.transform.gameObject.GetComponent<Enemy>();
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(weaponDamage);
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
