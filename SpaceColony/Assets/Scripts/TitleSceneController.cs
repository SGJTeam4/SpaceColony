﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
#region propertys

    [SerializeField]
    Sprite[] titleLogoImages;

    [SerializeField]
    GameObject titleLogo;

    float deltaTime;
    float titleAnimationFrameRate = 1.0f / 60.0f * 3.0f;
    int titleAnimationCount;
    Image titleLogoImage;

#endregion

#region public methods

    public void OnTitleClick()
    {
        SceneManager.LoadScene("PlayScene");
    }

#endregion

    // Use this for initialization
	void Start () {
        PlayerPrefs.SetInt(PlayerPrefsKeys.FirstFlag, 0);


        PlayerPrefs.SetInt(PlayerPrefsKeys.Resource, 100);
        PlayerPrefs.SetInt(PlayerPrefsKeys.Population, 100);
        PlayerPrefs.SetInt(PlayerPrefsKeys.Environment, 100);

        PlayerPrefs.SetInt(PlayerPrefsKeys.LV_CollectResource, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.LV_RestoreEnvironment, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.LV_Equipment, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.LV_Medicine, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.LV_Food, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.LV_ReproduceResource, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.LV_Health, 1);



	}

    void Awake()
    {
        titleLogoImage = titleLogo.GetComponent<Image>();
        titleLogoImage.sprite = titleLogoImages[0];
    }
	
	// Update is called once per frame
	void Update () {

        if (titleAnimationCount + 1 >= titleLogoImages.Length)
        {
            return;
        }

        deltaTime += Time.deltaTime;

        if (deltaTime > titleAnimationFrameRate)
        {
            deltaTime = 0.0f;
            titleAnimationCount++;

            titleLogoImage.sprite = titleLogoImages[titleAnimationCount];
        }
	}
}
