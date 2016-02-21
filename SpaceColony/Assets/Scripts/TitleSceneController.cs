using UnityEngine;
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
