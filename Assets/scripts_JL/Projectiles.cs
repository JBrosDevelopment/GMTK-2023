using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Range(0f, 10f)]
    public float RandomTimeSpanMax = 5f;
    public GameObject[] projectiles;
    public GameObject Warning;
    public float BallWarningTime = 1.3f;
    public Vector2 Force;
    public Vector2 LeftPosition;
    public Vector2 RightPosition;
    public Vector2 LeftWarning;
    public Vector2 RightWarning;
    public float random = 50;
    GameObject warn;
    [HideInInspector] public GameObject[] allprojectiles;

    bool left;
    bool haswaited = false;
    bool warning = false;
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        left = Random.Range(0, 2) == 1 ? false : true;
        if (left)
        {
            warn = Instantiate(Warning, LeftWarning, Quaternion.identity);
        }
        else
        {
            warn = Instantiate(Warning, RightWarning, Quaternion.identity);
        }
        yield return new WaitForSeconds(BallWarningTime);
        warning = true;
    }

    void Update()
    {
        float wait = Random.Range(1f, RandomTimeSpanMax);
        if (!haswaited)
        {
            haswaited = true;
            StartCoroutine(Wait(wait));
        }
        if (warning && haswaited)
        {
            Destroy(warn);
            float range = Random.Range(Force.y - random, Force.y + random);
            int randomProjectile = Random.Range(0, projectiles.Length);
            GameObject project = Instantiate(projectiles[randomProjectile], transform);
            allprojectiles.Append(project);
            project.transform.position = left ? LeftPosition : RightPosition;
            if (left)
            {
                project.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force.x, range));
            }
            else
            {
                project.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force.x, range));
            }
            warning = false;
            haswaited = false;
        }
    }
}
