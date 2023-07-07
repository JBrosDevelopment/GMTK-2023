using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Duck : MonoBehaviour
{
    GameObject duck;
    Transform position; 

    private void Awake()
    {
        duck = GetComponent<GameObject>();
    }
    private void Start()
    {
        position = GetComponent<Transform>();
    }
    void Update()
    {

    }
    private void FixedUpdate()
    {
        
    }
}
