using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnergy : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            animator.SetTrigger("EnergyShot");
        }
        
    }
}
