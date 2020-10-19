using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] float _delay;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _delay);
    }

}
