using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("LevelController")]
    //[SerializeField] public ParticleSystem muzzleFlash = null;
    //[SerializeField] public AudioSource shootSound = null;
    //[SerializeField] public AudioClip shootClip = null;
    public Level01Controller level01Controller = null;

    [Header("Movement Settings")]
    public CharacterController controller;
    public float _defaultMoveSpeed;
    public float _moveSpeed = 12f;
    public float _slowedSpeed = 2f;
    public float _sprintSpeed = 18f;
    public float _gravity = -9.81f;
    public float _jumpHeight = 3f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float _groundDistance = -0.4f; //radius of sphere
    public LayerMask groundMask; //checks for collision with the floor specifically, in case it catches player collision first, which it will
    [SerializeField] FireWeapon fireWeapon;

    [Header("Health Settings")]
    [SerializeField] GameObject damagePanel;
    [SerializeField] AudioClip damageClip;
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar;
    public LayerMask hazardMask;

    [Header("Respawn Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    public bool isDead;

    [Header("Miscelaneous")]
    [SerializeField] AudioClip loseClip;
    public LayerMask enemyMask;

    Vector3 velocity;
    public bool isGrounded;
    bool isSprinting;
    bool isHurt;
    public bool isOnEnemy;
    

    private void Start()
    {
        _defaultMoveSpeed = _moveSpeed;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(Mathf.FloorToInt(maxHealth));
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, _groundDistance, groundMask); //checks for collision with floor using a small invisible sphere; returns true/false
        isOnEnemy = Physics.CheckSphere(groundCheck.position, _groundDistance, enemyMask);
        isHurt = Physics.CheckSphere(groundCheck.position, _groundDistance, hazardMask);

        Jump();

        Sprint();

        Move();

        Die();

        SimulateGravity();

        if (isHurt)
        {
            DamageTaken(0.9f);
        }


    }

    //private void OnTriggerEnter(Collider other) //damage taken upon collision with hazard volume
    //{
        //if (other.CompareTag("Hazard"))
        //{
           // DamageTaken(10);
        //}
    //}



      

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            Debug.Log("sprintin time!");
            _moveSpeed = _sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("walkin time!");
            _moveSpeed = _defaultMoveSpeed;
        }
    }

    void Jump()
    {
        if (isGrounded && velocity.y < 0 && fireWeapon.rightClickHeld == false) //when touching the ground, AND when velocity is at all greater than 0 (meaning player is being pushed by gravity), reset the velocity
        {
            velocity.y = -2f; //sticks player to ground
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 _moveDirection = transform.right * x + transform.forward * z; //find the move direction based on axis buttons pressed times their respective transforms
        controller.Move(_moveDirection * _moveSpeed * Time.deltaTime);
    }


    void Die()
    {
        if (currentHealth <= 0)
        {
            AudioHelper.PlayClip2D(loseClip, 2);
            isDead = true;
            level01Controller.inDeathMenu = true;
            Debug.Log("Dead");
            //player is dead
            //enable death menu [Respawn or Quit] --> in level controller
        }
    }

    public void Respawn()
    {
        if (isDead)
        {
            if (level01Controller._currentScore >= 50)
            {
                //this is what happens when player click respawn button
                //reset coord's
                //reset HP
                //TODO add 50 score penalty
                currentHealth = maxHealth;
                healthBar.SetHealth(Mathf.FloorToInt(currentHealth));
                player.transform.position = respawnPoint.transform.position;
                Physics.SyncTransforms();
                level01Controller.IncreaseScore(-50);
                isDead = false;
            }
        }
    }


    void SimulateGravity()
    {
        velocity.y += _gravity * Time.deltaTime; //need a velocity variable to simulate real gravity
        controller.Move(velocity * Time.deltaTime); //multiply times deltaTime twice, as is shown on velocity equation
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity); //force required to jump according to physics. (Square root of jump height x (-2) x gravity)
        }
    }

    public void DamageTaken(float damage)
    {
        StartCoroutine(DamageRoutine(damage));
        
    }

    private IEnumerator DamageRoutine(float damage)
    {
        if (!isDead)
        {
            AudioHelper.PlayClip2D(damageClip, 1);
        }
        damagePanel.SetActive(true);
        currentHealth -= damage;
        healthBar.SetHealth(Mathf.FloorToInt(currentHealth));
        yield return new WaitForSeconds(0.1f);
        damagePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOnEnemy)
        {
            if (other.tag.Equals("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(10);
            }
        }
    }
}
