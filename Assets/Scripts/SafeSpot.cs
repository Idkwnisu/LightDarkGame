﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DarkCycleDegrading.instance.Safe();
    }

    private void OnTriggerExit(Collider other)
    {
        DarkCycleDegrading.instance.Unsafe();
    }
}
