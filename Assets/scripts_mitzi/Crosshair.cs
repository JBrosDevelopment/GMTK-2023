using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float Maxspeed = 5;
    public float acceleration = 1;
    public float raduis = 1;
    public float time_between_shots = 3;
    public float random_offest_number = 0.5f;
    Vector3 offset = new Vector2();
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeOffset());
        StartCoroutine(set_ready());
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float current_speed = acceleration * Vector2.Distance(transform.position, target.position) * Time.deltaTime;
        if (current_speed > Maxspeed) current_speed = Maxspeed;
        transform.position = Vector2.MoveTowards(transform.position, target.position + offset, Time.deltaTime * current_speed);
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
