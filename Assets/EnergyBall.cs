using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    //Script for energy ball behavior
    // Update is called once per frame
    [SerializeField] PlayerMovementScript player = null;
    [SerializeField] AudioClip audioFeedback = null;
    public int weaponDamage = 30;

    Vector3 buffer;
    Animator animator;
    public LayerMask enemyMask;
    public bool hitSomething = false;
    public bool userHasShot = false;
    public bool reachedPeak = false;

    RaycastHit hitInfo;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("EnergyIsShot", false);

        if (other.tag.Equals("Enemy"))
        {
            hitSomething = true;
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(20);
            AudioHelper.PlayClip2D(audioFeedback, 2);
            buffer = other.transform.position;
            other.transform.position = player.transform.position;
            player.transform.position = buffer;
            
            userHasShot = false;
        }

        if (other.tag.Equals("Wall"))
        {
            hitSomething = true;
            
            animator.SetTrigger("EnergyExplosion");
            animator.SetBool("EnergyHitWall", true);
            
            userHasShot = false;
        }

        if (other.tag.Equals("Teleporter"))
        {
            hitSomething = true;
            Debug.Log("Teleporter Activated");
            player.transform.position = other.transform.position;
            player.transform.position = other.transform.position;
            player.transform.position = other.transform.position;
            player.transform.position = other.transform.position;
            userHasShot = false;
        }
    }


    //Attempts to make collision more 'accurate'
    private void LateUpdate()
    {
        if (!hitSomething && userHasShot)
        {
            Debug.DrawRay(transform.position, transform.forward * 1, Color.green, 3);
            Debug.DrawRay(transform.position, transform.right * 1, Color.green, 3);
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 3) || Physics.Raycast(transform.position, transform.right, out hitInfo, 3))
            {
                if (hitInfo.transform.tag.Equals("Enemy"))
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(20);
                    }
                    AudioHelper.PlayClip2D(audioFeedback, 2);
                    buffer = hitInfo.collider.transform.position;
                    hitInfo.collider.transform.position = player.transform.position;
                    player.transform.position = buffer;
                    hitSomething = true;
                    userHasShot = false;
                }

                if (hitInfo.transform.tag.Equals("Wall"))
                {
                    hitSomething = true;
                    Animator animator = gameObject.GetComponent<Animator>();
                    animator.SetTrigger("EnergyExplosion");
                    animator.SetBool("EnergyHitWall", true);

                    userHasShot = false;
                }

                if (hitInfo.transform.tag.Equals("Teleporter"))
                {
                    hitSomething = true;
                    player.transform.position = hitInfo.collider.transform.position;
                    userHasShot = false;
                }
            }
        }

        if (reachedPeak)
        {
            hitSomething = Physics.CheckSphere(transform.position, 40, enemyMask);
        }

    }

}
