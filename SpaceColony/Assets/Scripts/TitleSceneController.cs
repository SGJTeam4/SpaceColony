﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour {

    public void OnTitleClick()
    {
        SceneManager.LoadScene("PlayScene");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
