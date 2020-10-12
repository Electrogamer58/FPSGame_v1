using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyHealth = 50;
    [SerializeField] GameObject enemyVisual;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] Level01Controller level01Controller;


    // Update is called once per frame
    void Update()
    {
        
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
            DelayHelper.DelayAction(this, DisableEnemy, 3);
            level01Controller.IncreaseScore(50);
        }
    }

    void DisableEnemy()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;
    }
}
