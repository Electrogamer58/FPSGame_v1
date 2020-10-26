using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnergy : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] EnergyBall energyBall;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            energyBall.userHasShot = true;
            animator.SetTrigger("EnergyShot");
            energyBall.hitSomething = false;

            
        }
        
    }

    private void LateUpdate()
    {
        energyBall.hitSomething = true;
    }

   
}
