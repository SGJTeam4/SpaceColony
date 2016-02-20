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

    [SerializeField]
    private Button levelUpButton;

    /// <summary>消費する項目名</summary>
    private string requiredCategory;

    private string prefsName; 

    public void SetParameter(string in_systemName, string in_prefsKey, string in_detailSentence, string in_requiredCategory){
        this.systemNameText.text = in_systemName;
        this.levelText.text = "Lv." + PlayerPrefs.GetInt(in_prefsKey, 1);
        this.infomationText.text = in_detailSentence;
        this.requiredCategory = in_requiredCategory;
        this.levelUpButtonLabel.text = in_requiredCategory + GetChangeValue(in_prefsKey).ToString();
        this.prefsName = in_prefsKey;


        // 使えない時はグレー
        if ((PlayerPrefs.GetInt(in_requiredCategory) - GetChangeValue(in_prefsKey)) <= 0)
        {
            this.levelUpButton.GetComponent<Button>().interactable = false;
        }

        //this.levelUpButton.onClick.AddListener(() => OnPressedLevelUp());
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /// <summary>
    /// レベルアップによって変動する値を取得
    /// </summary>
    /// <param name="in_systemName">システムの名前(分岐するためです)</param>
    /// <returns></returns>
    public int GetChangeValue(string in_systemName)
    {

        int value = 0;

        int level = PlayerPrefs.GetInt(in_systemName, 1);

        if (in_systemName == "CollectResource") return -10;
        else if (in_systemName == "RestoreEnvironment" || in_systemName == "Health") return -1 * (10 + (5 * level));
        else if (in_systemName == "Equipment" || in_systemName == "Medicina" || in_systemName == "Food" || in_systemName == "ReproduceResource") return -1 * (20 + (5 * level));

        return value;
    }


    public void OnPressedLevelUp()
    {
        // 押されたボタンによって値を変更
        int currentValue = PlayerPrefs.GetInt(this.requiredCategory);
        PlayerPrefs.SetInt(this.requiredCategory, currentValue - GetChangeValue(this.prefsName));
    }


}
