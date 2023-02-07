using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 0.5f;
    public float moveRotaion = 5f;

    private float rotationSpeed = 5f;
    private bool isDust = false;

    private float horizontal;
    private float vertical;

    private Rigidbody2D rb;
    private ParticleSystem dust_ps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dust_ps = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        {
            if (!isDust) {
                dust_ps.Play();
                isDust = true;
            }
            if (horizontal > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -moveRotaion), rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, -moveRotaion), rotationSpeed * Time.deltaTime); 
            }
        }
        else
        {
            dust_ps.Stop();
            isDust = false;
            Quaternion rotationReset = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 5 * Time.deltaTime);
            transform.rotation = rotationReset;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * Speed, vertical * Speed);
    }
}
