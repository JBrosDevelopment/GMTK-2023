using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDuckShot : MonoBehaviour
{
    // WHEN AI DUCK GETS SHOT, PLAY ANIMATION
    public GameObject gunshot;
    public Animator animator;
    public Collider2D col;
    public float raise;

    public void Shot()
    {
        StartCoroutine(fold());
    }
    IEnumerator fold()
    {
        col.enabled = false;
        animator.SetBool("hit", true);
        yield return new WaitForSeconds(raise);
        animator.SetBool("hit", false);
        col.enabled = true;
    }
}
