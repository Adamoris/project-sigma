﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proceed : MonoBehaviour
{
    public LevelLoader levelLoader;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            levelLoader.LoadLevel(1);
        }
    }
}
