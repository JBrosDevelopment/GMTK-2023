using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManger : MonoBehaviour
{
    public round[] rounds;
    public Text timer;

    void Start()
    {
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
}
