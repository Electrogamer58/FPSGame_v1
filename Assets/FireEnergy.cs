using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnergy : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] EnergyBall energyBall;
    [SerializeField] Camera camera;

    float fireRate = 3;
    public float nextFire = 2;
    RaycastHit hitInfo;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            Debug.Log("FIRE ENERGY");
            energyBall.userHasShot = true;
            animator.SetTrigger("EnergyShot");
            animator.SetBool("EnergyIsShot", true);
            energyBall.hitSomething = false;
            animator.SetBool("EnergyHitWall", false);


            Vector3 rayDirection = camera.transform.forward;
            Debug.DrawRay(camera.transform.position, rayDirection * 10, Color.cyan, 1);
            if (Physics.Raycast(camera.transform.position, rayDirection, out hitInfo, 15)){ //check with a raycast just in case
                if (hitInfo.transform.tag.Equals("Teleporter"))
                {
                    DelayHelper.DelayAction(this, Teleport, 0.5f);
                    
                }
            }
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

    void Teleport()
    {
        transform.position = hitInfo.collider.transform.position;
    }
   
}
