using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{

#region public methods

    public void OnTitleClick()
    {
        SceneManager.LoadScene("");
    }

#endregion

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
