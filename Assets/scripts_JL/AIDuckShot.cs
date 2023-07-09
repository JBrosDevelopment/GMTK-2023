using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDuckShot : MonoBehaviour
{
    // WHEN AI DUCK GETS SHOT, PLAY ANIMATION
    public bool flying;
    public GameObject gunshot;
    public Animator animator;
    public SpriteRenderer sprite;
    public int goldWorth = 50;
    public int NormalWorth = 5;
    int pointsWorth = 5;
    public Collider2D col;
    public float raise;
    private void Start()
    {
        pointsWorth = NormalWorth;
    }
    public void ChangeGold()
    {
        sprite.color = Color.yellow;
        pointsWorth = goldWorth;
    }
    public void Shot()
    {
        PointsManager pm = GameObject.FindGameObjectWithTag("PointsManager").GetComponent<PointsManager>();
        pm.AddPoints(pointsWorth);
        StartCoroutine(fold());
    }
    IEnumerator fold()
    {
        col.enabled = false;
        animator.SetBool("hit", true);
        for (int i = 0; i < 6; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
        if (!flying)
        {
            yield return new WaitForSeconds(raise);
            for (int i = 0; i < 6; i++)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
                yield return new WaitForSeconds(0.1f);
            }
            col.enabled = true;
            sprite.color = Color.white;
            pointsWorth = NormalWorth;
            animator.SetBool("hit", false);
        }
    }
}
