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


    /// <summary>レベルが上がる上限値</summary>
    private int levelLimit;

    /// <summary>システムの名前</summary>
    private string systemName;

    // ボタンのアクティベートを呼び出すよう
    private ReinforceSceneController rsCtrl;

    public Button SetParameter(ReinforceSceneController in_rsCtrl, string in_systemName, string in_prefsKey, string in_detailSentence, string in_requiredCategory, int in_levelLimit){
        this.rsCtrl = in_rsCtrl;
        this.systemName = in_systemName;
        this.systemNameText.text = in_systemName;
        this.levelText.text = "Lv." + PlayerPrefs.GetInt("Lv_"+in_prefsKey, 1);
        this.infomationText.text = in_detailSentence;
        this.requiredCategory = in_requiredCategory;

        if (in_requiredCategory == "Resource") this.levelUpButtonLabel.text = "資源" + GetChangeValue(in_prefsKey).ToString();
        else if (in_requiredCategory == "Environment") this.levelUpButtonLabel.text = "環境" + GetChangeValue(in_prefsKey).ToString();


        this.levelLimit = in_levelLimit;

        this.prefsName = in_prefsKey;

        CheckButtonActivate();

        return this.levelUpButton;
    }

    /// <summary>
    /// コストが足りないorレベルMAXのときはボタンを無効化
    /// </summary>
    public void CheckButtonActivate()
    {
        if ((PlayerPrefs.GetInt(this.requiredCategory) + GetChangeValue(this.prefsName)) <= 0)
        {
            this.levelUpButton.GetComponent<Button>().interactable = false;
        }
        string pKey = "Lv_"+this.prefsName;
        if (PlayerPrefs.GetInt(pKey) >= this.levelLimit) this.levelUpButton.GetComponent<Button>().interactable = false;
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

        int level = PlayerPrefs.GetInt("Lv_"+in_systemName);

        if (in_systemName == "CollectResource") return -10;
        else if (in_systemName == "RestoreEnvironment" || in_systemName == "ReproduceResource") return -1 * (5 + (5 * level));
        else if (in_systemName == "Equipment" || in_systemName == "Medicine" || in_systemName == "Food" || in_systemName == "Health") return -1 * (15 + (5 * level));

        return value;
    }


    public void OnPressedLevelUp()
    {
        // 押されたボタンによって値を変更
        int currentValue = PlayerPrefs.GetInt(this.requiredCategory);
        PlayerPrefs.SetInt(this.requiredCategory, currentValue + GetChangeValue(this.prefsName));


        LevelUPProcess(this.prefsName);
        LevelTextUpdate();

        // 資源採取の場合
        if (this.prefsName == PlayerPrefsKeys.CollectResource)
        {
            CollectResource();
        }

        ButtonLabelUpdate();

        // ボタンの無効化チェックを呼び出す
        this.rsCtrl.CheckButtonActivates();
    }



    public void CollectResource()
    {
        int result = PlayerPrefs.GetInt(PlayerPrefsKeys.Resource)+(int)(0.1 * PlayerPrefs.GetInt(PlayerPrefsKeys.Population));

        if (result >= 100)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.Resource, 100);
        }
        else PlayerPrefs.SetInt(PlayerPrefsKeys.Resource, result);
    }

    /// <summary>
    /// レベルの値を上げる
    /// </summary>
    /// <param name="in_systemName"></param>
    public void LevelUPProcess(string in_systemName)
    {
        string pKey = "Lv_"+in_systemName;

        if( PlayerPrefs.GetInt(pKey) < this.levelLimit)
        PlayerPrefs.SetInt(pKey, PlayerPrefs.GetInt(pKey) + 1);
    }

    public void LevelTextUpdate()
    {
        this.levelText.text = "Lv." + PlayerPrefs.GetInt("Lv_" + this.prefsName, 0);
    }


    public void ButtonLabelUpdate()
    {
        if (this.requiredCategory == "Resource") this.levelUpButtonLabel.text = "資源" + GetChangeValue(this.prefsName).ToString();
        else if (this.requiredCategory == "Environment") this.levelUpButtonLabel.text = "環境" + GetChangeValue(this.prefsName).ToString();
    }


}
