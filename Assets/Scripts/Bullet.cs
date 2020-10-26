using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject bloodParticles;
    [SerializeField] GameObject wallParticles;
    public float moveSpeed = 10;
    public float bulletDamage;

    Rigidbody rb;

    PlayerMovementScript target = null;
    Vector3 moveDirection;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerMovementScript>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        Destroy(gameObject, 3f); //destroy after 3 seconds
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(bloodParticles, transform.position, transform.rotation);
            target.DamageTaken(bulletDamage);
            bloodParticles.transform.position = transform.position;
            ParticleSystem bloodspurt = bloodParticles.GetComponent<ParticleSystem>();
            bloodspurt.Play();
            Destroy(gameObject, 0);

        }

        if (other.tag == "Wall")
        {
            wallParticles.transform.position = transform.position;
            ParticleSystem wallspurt = wallParticles.GetComponent<ParticleSystem>();
            wallspurt.Play();
            Destroy(gameObject, 0);
        }
    }

}
