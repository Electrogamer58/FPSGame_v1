using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIfHit : MonoBehaviour
{
    [SerializeField] Transform respawn;
    [SerializeField] Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            player.transform.position = respawn.position;
            Physics.SyncTransforms();
        }
    }
}
