using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Duck : MonoBehaviour
{
    public static bool Dead;
    GameObject duck;
    Rigidbody2D rb;
    [Range(0.1f, 10f)]
    public float _speed = 10;
    [Range(0.1f, 10f)]
    public float _JumpSpeed = 10;
    public int MaxJumps = 2;
    public float JumpCooldown = 0.2f;
    public float ThirdJumpDrag = 10;
    public float _delta = 10000;
    [Tooltip("Shows: \nDeltatime, Jump(true/false), JumpCount, Can Jump")]
    public bool ShowDebug = false;
    float JumpSpeed;
    int JumpCount = 0;
    float speed;
    float delta;
    bool isGrounded = true;
    float XInput;
    bool Jump;
    bool isJumping = false;
    float jumpTimer = 0f;
    bool dragging = false;

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private void Awake()
    {
        duck = gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Dead) return;

        delta = Time.deltaTime * _delta;
        speed = _speed / 5;
        JumpSpeed = _JumpSpeed * 10;
        if (JumpCount >= MaxJumps) isGrounded = false;
        if (ShowDebug)
        {
            Debug.Log("Delta: " + delta);
            Debug.Log("Jump: " + Jump);
            Debug.Log("JumpCount: " + JumpCount);
            Debug.Log("IsGrounded: " + isGrounded);
        }
        XInput = Input.GetAxis("Horizontal");
        Jump = (Input.GetAxis("Jump") > 0 || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && isGrounded ? true : false;
    }

    void FixedUpdate()
    {
        if (Dead) return;

        rb.rotation = 0;
        rb.velocity = new Vector2(XInput * speed * delta, rb.velocity.y);

        if(XInput == 0 && dragging)
        {
            rb.drag = 0;
        }
        else if (XInput != 0 && dragging)
        {
            rb.drag = ThirdJumpDrag;
        }

        if (isJumping && Time.time >= jumpTimer + JumpCooldown)
        {
            isJumping = false;
        }

        if (Jump && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            JumpCount++;
            Jump = false;
            isJumping = true;
            jumpTimer = Time.time;
            rb.AddForce(transform.up * JumpSpeed * delta);
        }
        else if(JumpCount >= MaxJumps && (Input.GetAxis("Jump") > 0 || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Time.time >= jumpTimer)
        {
            dragging = true;
            isJumping = false;
            Jump = false;
            jumpTimer = Time.time;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.drag = ThirdJumpDrag;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dragging = false;
        JumpCount = 0;
        isGrounded = true;
        isJumping = false;
        rb.drag = 0;
    }

    public void Death()
    {
        Dead = true;
    }
}
