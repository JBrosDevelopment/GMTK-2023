using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Duck : MonoBehaviour
{
    public bool Dead;
    public Animator anim;
    public SFXController sfx;
    Collider2D collider_;
    Rigidbody2D rb;
    [Range(1f, 10f)]
    public float speed = 10;
    [Range(1f, 1000f)]
    public float JumpSpeed = 10;
    public int MaxJumps = 2;
    public float drag = 10;
    public float _delta = 10000;
    [Tooltip("Shows: \nDeltatime, Jump(true/false), JumpCount, Can Jump")]
    int JumpCount = 0;
    bool isGrounded = true;
    bool Jump;
    bool jump_ready = false;
    bool gliding = false;
    AudioSource glide;
    private void Awake()
    {
        glide = sfx.GetSFX("glide");
        collider_ = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Dead) return;

        if (JumpCount >= MaxJumps) isGrounded = false;
        //can jump
        Jump = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && JumpCount < MaxJumps;
        if ((Input.GetKeyUp(KeyCode.UpArrow) && !Input.GetKey(KeyCode.W)) || (Input.GetKeyUp(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow)))
        {
            jump_ready = true;
        }

        //jump
        if (Jump && jump_ready)
        {
            sfx.Play("jump");
            jump_ready = false;
            JumpCount++;
            Jump = true;
            rb.AddForce(Vector2.up * JumpSpeed);
        }

        //movement left & right
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            if (isGrounded)
                rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("run", false);
        }

        //glide
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !jump_ready)
        {
            if (!glide.isPlaying) glide.Play();
            gliding = true;
            rb.drag = drag;
        }
        else
        {
            gliding = false;
            rb.drag = 0;
        }
        anim.SetBool("jump", Jump);
        anim.SetBool("grounded", isGrounded);
        anim.SetBool("glide", gliding);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground"))
        {
            gliding = false;
            JumpCount = 0;
            isGrounded = true;
            jump_ready = true;
            rb.drag = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground"))
            isGrounded = false;
    }
    public void Death()
    {
        rb.drag = 0;
        sfx.Play("death");
        GameObject go = GameObject.FindGameObjectWithTag("RoundManager");
        go.GetComponent<RoundManger>().Restart();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.angularVelocity = 20;
        collider_.enabled = false;
        anim.SetBool("dead", true);
        Dead = true;
    }
}
