using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Projectile")
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
