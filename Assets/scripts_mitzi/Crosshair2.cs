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
    //for temp animation
    SpriteRenderer sprite;
    Vector3 offset = new Vector2();
    Rigidbody2D rb;
    Transform target;
    Main_Duck duck;
    GameObject aiDuck;
    public GameObject GunShot;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
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
        sprite.color = Color.red;
        yield return new WaitForSeconds(1);
        ready_to_shoot = true;
    }
    bool shooting;
    bool onDuck;
    bool onbBgDuck;
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
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player")) onDuck = false;
        if (col.CompareTag("bgDuck"))
        {
            onbBgDuck = false;
        }
    }
    void shoot()
    {
        man.SetTrigger("shoot");
        //play gun sound
        sprite.color = Color.white;
        if (onDuck && !onbBgDuck)
            duck.Death();
        else if (onbBgDuck)
        {
            if(aiDuck != null)
            {
                AIDuckShot s = aiDuck.GetComponent<AIDuckShot>();
                if (s != null)
                    s.Shot();
            }
        }
        else Instantiate(GunShot, transform.position, Quaternion.identity);
        onbBgDuck = false;
        aiDuck = null;
        shooting = false;
        StartCoroutine(set_ready());
    }
}
