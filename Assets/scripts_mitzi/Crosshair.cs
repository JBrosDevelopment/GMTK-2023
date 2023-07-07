using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Crosshair : MonoBehaviour
{
    public float Maxspeed = 5;
    public float acceleration = 1;
    public float raduis = 1;
    public float time_between_shots = 3;
    float current_speed = 0;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(set_ready());
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime);
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
