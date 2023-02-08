using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkingSpeed = 4f;
    public float runningSpeed = 4.5f;

    public float Stamina = 20f;
    public float moveRotaion = 5f;

    public float runningDustEmission = 25f;
    public float walkingDustEmission = 5f;

    public KeyCode runningKey = KeyCode.LeftControl;

    private float rotationSpeed = 5f;
    private float currentSpeed = 0f;
    private bool isDust = false;
    private bool lastRotatedLeft = false;

    private float horizontal;
    private float vertical;

    private Rigidbody2D rb;
    private ParticleSystem dust_ps;

    void Start()
    {
        currentSpeed = walkingSpeed;
        rb = GetComponent<Rigidbody2D>();
        dust_ps = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        var dustEmission = dust_ps.emission;

        if (Input.GetKeyDown(runningKey))
        {
            dustEmission.rateOverTime = runningDustEmission;
            currentSpeed = runningSpeed;
        }
        else if (Input.GetKeyUp(runningKey))
        {
            dustEmission.rateOverTime = walkingDustEmission;
            currentSpeed = walkingSpeed;
        }

        if (horizontal != 0 || vertical != 0)
        {
            if (!isDust)
            {
                dust_ps.Play();
                isDust = true;
            }
            if (horizontal > 0)
            {
                RotateCharacter(new Vector3(0, 0, -moveRotaion), rotationSpeed);
                lastRotatedLeft = false;
            }
            else
            {
                RotateCharacter(new Vector3(0, 180, -moveRotaion), rotationSpeed);
                lastRotatedLeft = true;
            }
        }
        else
        {
            dust_ps.Stop();
            isDust = false;
            RotateCharacter(lastRotatedLeft ? new Vector3(0, 180, 0) : Vector3.zero, rotationSpeed);
        }

        if (vertical == 0) RotateCharacter(lastRotatedLeft ? new Vector3(0, 180, 0) : Vector3.zero, rotationSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * currentSpeed, vertical * currentSpeed);
    }

    private void RotateCharacter(Vector3 rotation, float rotationSpeed)
    {
        Quaternion rotateQuanternion = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotation), rotationSpeed * Time.deltaTime);
        transform.rotation = rotateQuanternion;
    }
}
