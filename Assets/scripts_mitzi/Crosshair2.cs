using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair2 : MonoBehaviour
{
    public float acceleration = 1;
    public float time_between_shots = 3;
    public float random_offest_number = 0.5f;
    public float Accuracy = 100;
    public float Shoot_Time = 1f;
    public Animator man;
    SpriteRenderer render;
    public Sprite big;
    public Sprite small;
    public SFXController sfx;
    Vector3 offset = new Vector2();
    Rigidbody2D rb;
    Transform target;
    Main_Duck duck;
    GameObject aiDuck;
    GameObject Tgt;
    public GameObject GunShot;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        Tgt = GameObject.FindGameObjectWithTag("TargetManager");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(changeOffset());
        StartCoroutine(set_ready());
        target = GameObject.FindGameObjectWithTag("Player").transform;
        duck = target.GetComponent<Main_Duck>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!shooting)
        {
            Vector2 Force = PosToForce(target.position + offset);
            if (Random.Range(0, 100) < Accuracy) Force *= 10;
            rb.AddForce(Force);
        }
    }
    Vector2 PosToForce(Vector2 pos)
    {
        float t = 0.5f;
        float x = pos.x - transform.position.x;
        float y = pos.y - transform.position.y;
        float ax = 2 * (x - rb.velocity.x * t) / Mathf.Pow(t, 2);
        float ay = 2 * (y - rb.velocity.y * t) / Mathf.Pow(t, 2);
        if (ay > acceleration) ay = acceleration;
        if (ax > acceleration) ax = acceleration;
        if (ay < -acceleration) ay = -acceleration;
        if (ax < -acceleration) ax = -acceleration;
        return new Vector2(ax, ay) * rb.mass;
    }
    IEnumerator changeOffset()
    {
        yield return new WaitForSeconds(0.5f);
        float y = Random.Range(0, random_offest_number);
        float x = Random.Range(0, random_offest_number);
        offset = new Vector3(x, y, 0);
        StartCoroutine(changeOffset());
    }

    bool ready_to_shoot = false;
    IEnumerator set_ready()
    {
        yield return new WaitForSeconds(time_between_shots);
        //animation
        render.sprite = small;
        sfx.Play("reload");
        yield return new WaitForSeconds(1);
        ready_to_shoot = true;
    }
    bool shooting;
    bool onDuck;
    bool onbBgDuck;
    bool onTarget;
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onDuck = true;
            if (ready_to_shoot)
            {
                rb.velocity = Vector2.zero;
                Invoke("shoot", Shoot_Time);
                ready_to_shoot = false;
                shooting = true;
            }
        }
        else if (col.CompareTag("bgDuck"))
        {
            onbBgDuck = true;
            if (ready_to_shoot)
            {
                aiDuck = col.gameObject;
                rb.velocity = Vector2.zero;
                Invoke("shoot", Shoot_Time);
                ready_to_shoot = false;
                shooting = true;
            }
        }
        else if (col.CompareTag("target"))
        {
            onTarget = true;
            if (ready_to_shoot)
            {
                rb.velocity = Vector2.zero;
                man.SetTrigger("shoot");
                Invoke("shoot", Shoot_Time);
                ready_to_shoot = false;
                shooting = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player")) onDuck = false;
        if (col.CompareTag("bgDuck"))
        {
            onbBgDuck = false;
        }
        if (col.CompareTag("target"))
        {
            onTarget = false;
        }
    }
    void shoot()
    {
        //play gun sound
        sfx.Play("shoot");
        render.sprite = big;
        if (onDuck && !onbBgDuck)
            duck.Death();
        else if (onbBgDuck)
        {
            if (aiDuck != null)
            {
                AIDuckShot s = aiDuck.GetComponent<AIDuckShot>();
                if (s != null)
                    s.Shot();
            }
        }
        else if (onTarget)
        {
            TargetManager t = Tgt.GetComponent<TargetManager>();
            t.Shot();
        }
        else Instantiate(GunShot, transform.position, Quaternion.identity);
        aiDuck = null;
        onTarget = false;
        onbBgDuck = false;
        shooting = false;
        StartCoroutine(set_ready());
    }
}
