﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //params
    [SerializeField] int breakableBlocks;
    //cache
    SceneLoader sceneLoader;

    public void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void CountBlocksLeft()
    {
        breakableBlocks--;
        if (breakableBlocks<=0)
        {
            sceneLoader.LoadNextScene();
        }
    }

}
