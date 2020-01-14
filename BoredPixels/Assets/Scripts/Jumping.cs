using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Jumping : MonoBehaviour
{
    // jump force
    // gravity
    public AudioSource audioSource;
    public AudioClip jumpAudio;
    float jumpForcePercentage = 0.0f;
    float maxJumpForce = 30.0f;

    public Sprite stand, crouch, jump;

    bool drawGizmos = false;
    public Text jumpForceText;
    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            drawGizmos = true;
            // Start jumping
            if (jumpForcePercentage < 100.0f)
                jumpForcePercentage += Time.deltaTime * 50.0f;
            else
                jumpForcePercentage = 100.0f;

            GetComponent<SpriteRenderer>().sprite = crouch;
        }
        jumpForceText.text = jumpForcePercentage.ToString("#.##");


        if (Input.GetMouseButtonUp(1))
        {
            drawGizmos = false;
            Jump(jumpForcePercentage);
            jumpForcePercentage = 0.0f;

            GetComponent<SpriteRenderer>().sprite = jump;
            audioSource.PlayOneShot(jumpAudio);
        }
    }

    void Jump(float force)
    {
        Vector2 vec = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Vector2 vel = vec * (maxJumpForce * (jumpForcePercentage / 100.0f));
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(vel, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        List<Vector2> points = new List<Vector2>();
        for(int i = 1; i < jumpForcePercentage / 10.0f; i++)
        {
            points.Add(Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), i / 10.0f));
        }

        if (drawGizmos)
        {
            foreach (var p in points)
            {
                Gizmos.DrawSphere(p, 0.1f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = stand;
    }
}
