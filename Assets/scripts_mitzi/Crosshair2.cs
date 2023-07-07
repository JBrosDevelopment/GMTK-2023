using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair2 : MonoBehaviour
{
    public float Maxspeed = 5;
    public float acceleration = 1;
    public float raduis = 1;
    public float time_between_shots = 3;
    public float random_offest_number = 0.5f;
    public float Accuracy = 100;
    Vector3 offset = new Vector2();
    Rigidbody2D rb;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(changeOffset());
        StartCoroutine(set_ready());
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 Force = PosToForce(target.position+offset);
        if (Random.Range(0, 100) < Accuracy) Force *= 10;
        rb.AddForce(Force);
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
        for (int i = 0; i < time_between_shots; i++)
        {
            yield return new WaitForSeconds(1);
            //animation
        }
        ready_to_shoot = true;
    }
}
