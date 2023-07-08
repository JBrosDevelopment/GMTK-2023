using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Range(0f, 100f)]
    public float Hardness = 50f;
    public GameObject[] projectiles;
    public GameObject Warning;
    public float BallWarningTime = 1.3f;
    public Vector2 Force;
    public Vector2 LeftPosition;
    public Vector2 RightPosition;
    public float random = 50;
    public float warnOffset = 2;
    GameObject warn;

    bool left;
    bool haswaited = false;
    bool warning = false;
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        left = Random.Range(0, 2) == 1 ? false : true;
        if (left)
        {
            warn = Instantiate(Warning, new Vector2(LeftPosition.x - warnOffset, LeftPosition.y), Quaternion.identity);
        }
        else
        {
            warn = Instantiate(Warning, new Vector2(RightPosition.x + warnOffset, LeftPosition.y), Quaternion.identity);
        }
        yield return new WaitForSeconds(BallWarningTime);
        warning = true;
    }

    void Update()
    {
        float wait = Random.Range(0.5f, (Hardness / 50) * (2 * (Hardness / 100) * 3) + 1f - Hardness / 100);
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
