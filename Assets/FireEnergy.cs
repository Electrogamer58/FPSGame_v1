using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnergy : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] EnergyBall energyBall;

    float fireRate = 3;
    public float nextFire = 2;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            Debug.Log("FIRE ENERGY");
            energyBall.userHasShot = true;
            animator.SetTrigger("EnergyShot");
            animator.SetBool("EnergyIsShot", true);
            energyBall.hitSomething = false;
            animator.SetBool("EnergyHitWall", false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time > nextFire)
            {
                Debug.Log("Meelee!");
                animator.SetBool("MeleeHit", true);
                nextFire = Time.time + fireRate;
            }
        }
        else if (Time.time < nextFire)
        {
            animator.SetBool("MeleeHit", false);
        }

    }

    private void LateUpdate()
    {
        energyBall.hitSomething = true;
    }

   
}
