using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Range(0f, 100f)]
    public float Hardness = 50f;
    public GameObject[] projectiles;
    public Vector2 Force;
    public Vector2 LeftPosition;
    public Vector2 RightPosition;
    public float random = 50;

    bool waited = false;
    bool haswaited = false;
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        waited = true;
        haswaited = false;
    }

    void Update()
    {
        float wait = Random.Range(0.5f, (Hardness / 50) * (2 * (Hardness / 100) * 3) + 3);
        if (!waited && !haswaited)
        {
            haswaited = true;
            StartCoroutine(Wait(wait));
        }
        if (!waited) return;
        int randomProjectile = Random.Range(0, projectiles.Length);
        GameObject project = Instantiate(projectiles[randomProjectile], transform);
        bool left = Random.Range(0, 2) == 1 ? false : true;
        project.transform.position = left ? LeftPosition : RightPosition;
        float range = Random.Range(Force.y - random, Force.y + random);
        if(left)
        {
            project.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force.x, range));
        }
        else
        {
            project.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force.x, range));
        }
        waited = false;
    }
}
