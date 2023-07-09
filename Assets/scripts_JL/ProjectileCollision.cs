using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    bool waited;
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.4f);
        waited = true;
    }
    private void Awake()
    {
        waited = false;
        StartCoroutine(wait());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(gameObject);
            collision.gameObject.GetComponent<Main_Duck>().Death();
        }
        if (collision.gameObject.tag == "side" && waited)
        {
            Destroy(gameObject);
        }
    }
}
