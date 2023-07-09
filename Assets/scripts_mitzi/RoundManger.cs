using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManger : MonoBehaviour
{
    public GameObject Player;
    public round[] rounds;
    public Text timer;

    void Start()
    {
        Instantiate(Player);
        StartCoroutine(waves());
    }
    IEnumerator waves()
    {
        foreach(round r in rounds)
        {
            GameObject man = Instantiate(r.man, transform.position, Quaternion.identity);
            for (int i = 0; i < r.time; i++)
            {
                yield return new WaitForSeconds(1);
                timer.text = (r.time - i).ToString();
            }
            Destroy(man);
            yield return new WaitForSeconds(1f);
        }
    }
    public void Restart()
    {
        return;
        Destroy(Player);
        foreach(round r in rounds)
        {
            Destroy(r.man);
        }

        StopAllCoroutines();
        StartCoroutine(restart());
    }
    IEnumerator restart()
    {
        PointsManager pm = GameObject.FindGameObjectWithTag("PointsManager").GetComponent<PointsManager>();
        pm.Points = 0;
        yield return new WaitForSeconds(1);
        Instantiate(Player);
        StartCoroutine(waves());
    }
}
