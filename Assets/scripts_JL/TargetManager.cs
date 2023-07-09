using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public float RandomTimeSpanMin;
    public float RandomTimeSpanMax;
    public GameObject Target;
    public GameObject Point;
    public float Top;
    public float Left;
    public float Right;
    bool waited;
    bool shot = true;
    GameObject t;

    IEnumerator wait()
    {
        yield return new WaitForSeconds(Random.Range(RandomTimeSpanMin, RandomTimeSpanMax));
        waited = true;
    }
    IEnumerator waitDeath()
    {
        yield return new WaitForSeconds(1f);
        PointsManager pm = Point.GetComponent<PointsManager>();
        pm.Points++;
        Destroy(t);
        t = null;
        shot = true;
    }

    void Update()
    {
        if (shot)
        {
            StartCoroutine(wait());
            if (waited)
            {
                shot = false;
                if (t == null) t = Instantiate(Target, new Vector2(Random.Range(Left, Right), Top), Quaternion.identity);
                waited = false;
            }
        }
    }

    public void Shot()
    {
        shot = false;
        Animator animator = t.GetComponent<Animator>();
        animator.Play("Hit", 0, 0f);
        StartCoroutine(waitDeath());
    }
}
