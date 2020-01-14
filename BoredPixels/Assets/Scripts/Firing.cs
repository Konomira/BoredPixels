using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Firing : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gunPop;
    public GameObject projectile;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        audioSource.PlayOneShot(gunPop);
        var go = GameObject.Instantiate(projectile, transform.GetChild(0).GetChild(0).GetChild(0).position, Quaternion.identity);

        Vector2 vec = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        go.GetComponent<Rigidbody2D>().AddForce(vec * 20.0f,ForceMode2D.Impulse);
    }
}
