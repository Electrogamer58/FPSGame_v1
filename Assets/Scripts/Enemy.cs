using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyHealth = 50;
    [SerializeField] GameObject enemyVisual;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] Level01Controller level01Controller;
    [SerializeField] Transform playerTransform;
    [SerializeField] ViewRange viewRange;
    [SerializeField] EnemyFireWeapon enemyFire;




    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtPlayer();
    }

    public void TakeDamage(int _damageToTake)
    {
        enemyHealth -= _damageToTake; //TODO switch solid number with weapon script reference
        Debug.Log("Enemy Health at " + enemyHealth);
        if (enemyHealth <= 0)
        {
            enemyVisual.SetActive(false);
            deathParticles.Play();
            Debug.Log("Enemy Killed");
            DisableEnemy();
            level01Controller.IncreaseScore(50);
        }
    }

    void DisableEnemy()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;
        DelayHelper.DelayAction(this, DeleteSelf, 1);

    }

    void DeleteSelf()
    {
        Destroy(gameObject, 0);
    }

    void LookAtPlayer()
    {
       if (viewRange.seePlayer == true)
        {
            transform.LookAt(playerTransform);

            enemyFire.ShootBullet();
            
        }
    }
}
