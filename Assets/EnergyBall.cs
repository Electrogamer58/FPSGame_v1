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


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(20);
            AudioHelper.PlayClip2D(audioFeedback, 2);
            buffer = other.transform.position;
            other.transform.position = player.transform.position;
            player.transform.position = buffer;
        }

        if (other.tag.Equals("Wall"))
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.ResetTrigger("EnergyShot");
        }
    }
}
