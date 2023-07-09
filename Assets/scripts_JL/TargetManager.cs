using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public float RandomTimeSpanMin;
    public float RandomTimeSpanMax;
    public GameObject Target;
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
        yield return new WaitForSeconds(2f);
        //changed to AIDuckShot
        //PointsManager pm = GameObject.FindGameObjectWithTag("PointsManager").GetComponent<PointsManager>();
        //pm.AddPoints(10);
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
        AIDuckShot a = t.GetComponent<AIDuckShot>();
        a.Shot();
        StartCoroutine(waitDeath());
    }
}
