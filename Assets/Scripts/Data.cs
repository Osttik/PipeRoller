using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int Lenght { get; set; }
    public int Height { get; set; }
    public int HealthCount { get; set; }

    private void Start()
    {
        HealthCount = 2;
        Lenght = 5;
        Height = 5;
        DontDestroyOnLoad(this);
    }
}
