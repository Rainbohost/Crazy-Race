﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour {

    public void PlayPressed()
    {
        SceneManager.LoadScene("TestMap");
    }
}
