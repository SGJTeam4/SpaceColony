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


    // ボタンのアクティベートを呼び出すよう
    private ReinforceSceneController rsCtrl;

    public Button SetParameter(ReinforceSceneController in_rsCtrl, string in_systemName, string in_prefsKey, string in_detailSentence, string in_requiredCategory){
        this.rsCtrl = in_rsCtrl;
        
        this.systemNameText.text = in_systemName;
        this.levelText.text = "Lv." + PlayerPrefs.GetInt(in_prefsKey, 1);
        this.infomationText.text = in_detailSentence;
        this.requiredCategory = in_requiredCategory;

        if (in_requiredCategory == "Resource") this.levelUpButtonLabel.text = "資源" + GetChangeValue(in_prefsKey).ToString();
        else if (in_requiredCategory == "Environment") this.levelUpButtonLabel.text = "環境" + GetChangeValue(in_prefsKey).ToString();
       
        
        this.prefsName = in_prefsKey;

        CheckButtonActivate();

        return this.levelUpButton;
    }

    public void CheckButtonActivate()
    {
        if ((PlayerPrefs.GetInt(this.requiredCategory) + GetChangeValue(this.prefsName)) <= 0)
        {
            this.levelUpButton.GetComponent<Button>().interactable = false;
        }
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
        PlayerPrefs.SetInt(this.requiredCategory, currentValue + GetChangeValue(this.prefsName));

        this.rsCtrl.CheckButtonActivates();
    }





}
