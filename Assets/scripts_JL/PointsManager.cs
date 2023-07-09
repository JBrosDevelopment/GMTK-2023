using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public int Points { get; set; }
    public Text points;
    private void Update()
    {
        points.text = $"Points: {Points}";
    }
}
