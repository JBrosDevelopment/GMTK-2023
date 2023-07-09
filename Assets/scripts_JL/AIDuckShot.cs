using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDuckShot : MonoBehaviour
{
    // WHEN AI DUCK GETS SHOT, PLAY ANIMATION
    public GameObject gunshot;
    public Animator animator;
    public SpriteRenderer sprite;
    int pointsWorth = 5;
    public Collider2D col;
    public float raise;

    public void ChangeGold()
    {
        sprite.color = Color.yellow;
        pointsWorth = 50;
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
        yield return new WaitForSeconds(raise);
        sprite.color = Color.white;
        pointsWorth = 5;
        animator.SetBool("hit", false);
        col.enabled = true;
    }
}
