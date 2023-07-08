using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject button;
    public Color color;
    bool menual;
    void Start()
    {
        if (buttons.Length == 0)
        {
            buttons = GameObject.FindGameObjectsWithTag("Button");
        }
        else menual = true;
        transform.position = buttons[0].transform.position;
        button = buttons[0];
        KeyBordButton k = button.GetComponent<KeyBordButton>();
        if (!k.custom_highlight) button.GetComponent<Image>().color = color;
        else if (k.changeTextColor)
        {
            k.text.color = button.GetComponent<KeyBordButton>().hover;
        }
        else
        {
            print(1);
            button.GetComponent<Image>().color = button.GetComponent<KeyBordButton>().hover;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (menual)
        {
            button.GetComponent<Image>().color = color;
        }
        if (Input.GetKeyDown(KeyCode.Return)) click();
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject a = BR();
            Select(a);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject a = BL();
            Select(a);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject a = BU();
            Select(a);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject a = BD();
            Select(a);
        }

    }
    public void Select(GameObject button)
    {
        if (button != null)
        {
            Clear();
            transform.position = button.transform.position;
            this.button = button;
            KeyBordButton k = this.button.GetComponent<KeyBordButton>();
            if (k.custom_highlight)
            {
                if (k.changeTextColor) k.text.color = k.hover;
                else this.button.GetComponent<Image>().color = k.hover;
            }
            else this.button.GetComponent<Image>().color = color;
        }
    }
    void click()
    {
        button.GetComponent<KeyBordButton>().click();
    }
    void Clear()
    {
        ButtonSelect a = null;
        a = GameObject.Find("P2Select").GetComponent<ButtonSelect>();
        for (int i = 0; i < buttons.Length; i++)
        {
            KeyBordButton k = buttons[i].GetComponent<KeyBordButton>();
            if (k.custom_highlight)
            {
                if (k.changeTextColor)
                {
                    k.text.color = k.normal;
                }
                else
                {
                    buttons[i].GetComponent<Image>().color = k.normal;
                }
            }
            else buttons[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            buttons[i].GetComponent<Image>().color = Color.white;
        }
    }
    public GameObject BR()
    {
        GameObject thing = null;
        for (int i = 0; i < buttons.Length; i++)
        {
            Vector3 p = buttons[i].transform.position;
            if (transform.position.x < p.x && button != buttons[i])
            {
                if (thing == null) thing = buttons[i];
                else if (p.x < thing.transform.position.x &&
                    p.x > button.transform.position.x &&
                    button.transform.position.y == p.y) thing = buttons[i];
                else if (thing.transform.position.y != button.transform.position.y && p.y == button.transform.position.y) thing = buttons[i];
            }
            if (thing != null)
            {
                if (thing.transform.position.x == p.x)
                    if (Vector2.Distance(transform.position, p) < Vector2.Distance(transform.position, thing.transform.position)) thing = buttons[i];
            }
        }
        if (thing == null) thing = button;
        return thing;
    }
    public GameObject BL()
    {
        GameObject thing = null;
        for (int i = 0; i < buttons.Length; i++)
        {
            Vector3 p = buttons[i].transform.position;
            if (transform.position.x > p.x && button != buttons[i])
            {
                if (thing == null) thing = buttons[i];
                else if (p.x > thing.transform.position.x &&
                    p.x < button.transform.position.x &&
                    button.transform.position.y == p.y) thing = buttons[i];
                else if (thing.transform.position.y != button.transform.position.y && p.y == button.transform.position.y) thing = buttons[i];
            }
            if (thing != null)
            {
                if (thing.transform.position.x == buttons[i].transform.position.x)
                    if (Vector2.Distance(transform.position, buttons[i].transform.position) < Vector2.Distance(transform.position, thing.transform.position)) thing = buttons[i];
            }
        }
        if (thing == null) thing = button;
        return thing;
    }
    public GameObject BU()
    {
        GameObject thing = null;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (button.transform.position.y < buttons[i].transform.position.y && button != buttons[i])
            {
                if (thing == null) thing = buttons[i];
                else if (buttons[i].transform.position.y < thing.transform.position.y && buttons[i].transform.position.y > button.transform.position.y) thing = buttons[i];
            }
        }
        if (thing == null) thing = button;
        return thing;
    }
    public GameObject BD()
    {
        GameObject thing = null;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (button.transform.position.y > buttons[i].transform.position.y && button != buttons[i])
            {
                if (thing == null) thing = buttons[i];
                else if (buttons[i].transform.position.y > thing.transform.position.y && buttons[i].transform.position.y < button.transform.position.y) thing = buttons[i];
            }
        }
        if (thing == null) thing = button;
        return thing;
    }
}