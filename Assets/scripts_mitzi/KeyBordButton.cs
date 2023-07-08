using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class KeyBordButton : MonoBehaviour
{
    public UnityEvent button;
    public UnityEvent button2;
    public Animator animator;
    public bool custom_highlight;
    public bool changeTextColor;
    public Text text;
    public Color hover;
    public Color normal;
    public void click()
    {
        button.Invoke();
        animator.SetTrigger("click");
    }
    public void click2()
    {
        button2.Invoke();
        animator.SetTrigger("click");
    }
}
