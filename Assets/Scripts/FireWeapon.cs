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
    [SerializeField] AudioClip hitAudio;
    [SerializeField] AudioClip enemyHitAudio;
    [SerializeField] PlayerMovementScript player;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponCentered;
    [SerializeField] ParticleSystem muzzleFlash = null;
    [SerializeField] ParticleSystem muzzleFlashCentered = null;
    [SerializeField] AudioSource shootSound = null;
    [SerializeField] AudioClip shootClip = null;

    public float spreadFactor = 0.01f;
    public LineRenderer line;
    public bool rightClickHeld = false;
    int speedChange = 10;

    RaycastHit hitInfo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (player.isGrounded) //only allow when grounded
        {

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                AccuracyUp();
            }

            if (Input.GetKeyUp(KeyCode.Mouse1) && rightClickHeld)
            {
                AccuracyDown();
            }
        }

        
    }

    public void AccuracyUp()
    {
        rightClickHeld = true;
        player._moveSpeed = player._slowedSpeed;//slow down while clicked
        player._sprintSpeed -= speedChange; 

        //switch position of gun
        weapon.SetActive(false);
        weaponCentered.SetActive(true);
    }
    public void AccuracyDown()
    {
        rightClickHeld = false;
        player._moveSpeed = player._defaultMoveSpeed; //speed back up when released
        player._sprintSpeed += speedChange;

        //switch position of gun
        weapon.SetActive(true);
        weaponCentered.SetActive(false);

    }

    void drawLaser()
    {
        line.SetPosition(0, barrelEnd.position);
        line.SetPosition(1, hitInfo.point);
    }

    void deleteLaser()
    {
        line.positionCount = 0;
    }

    //fire the weapon
    public void Shoot()
    {

        //visual and audial feedback
        muzzleFlash.Play();
        muzzleFlashCentered.Play();
        shootSound.PlayOneShot(shootClip);

        //calculate direction to shoot ray
        Vector3 rayDirection = cameraController.transform.forward;
        if (rightClickHeld == false) { //make inaccurate if right click isn't held
            rayDirection.x += Random.Range(-spreadFactor, spreadFactor);
            rayDirection.y += Random.Range(-spreadFactor, spreadFactor);
            rayDirection.z += Random.Range(-spreadFactor, spreadFactor);
        }
        //cast debug ray
        Debug.DrawRay(barrelEnd.position, rayDirection * rayDistance, Color.blue, 1);
        
        //cast real raycast
        if (Physics.Raycast(cameraController.transform.position, rayDirection, out hitInfo, rayDistance, hitLayers))
        {
            AudioSource.PlayClipAtPoint(hitAudio, hitInfo.point, 1); //audial and visual feedback of a shot
            //laser
            line.positionCount = 2;
            drawLaser();
            DelayHelper.DelayAction(this, deleteLaser, 0.2f);

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
                    AudioHelper.PlayClip2D(enemyHitAudio, 1);
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
