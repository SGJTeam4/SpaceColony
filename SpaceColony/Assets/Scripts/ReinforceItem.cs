using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 各強化項目クラス
/// </summary>
public class ReinforceItem : MonoBehaviour {


    [SerializeField]
    private Text systemNameText;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Text infomationText;

    [SerializeField]
    private Text levelUpButtonLabel;


    public void SetParameter(string in_systemName, string in_prefsKey, string in_detailSentence, string in_buttonLabel){
        this.systemNameText.text = in_systemName;
        this.levelText.text = "Lv." + PlayerPrefs.GetInt(in_prefsKey, 1);
        this.infomationText.text = in_detailSentence;
        this.levelUpButtonLabel.text = in_buttonLabel;
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
