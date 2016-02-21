using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

/// <summary>
/// 強化画面の制御用クラス
/// </summary>
public class ReinforceSceneController : MonoBehaviour {

    public Transform[] displayPlace = new Transform[3];

    // 各システムオブジェクトのインスタンス
    private GameObject[] reinforceItem = new GameObject[3];


    private Button[] levelUpButton = new Button[3];


    // 左上のボタンラベル
    public Text categoryLabel;


    public GameObject itemPrefab;


    // おためしよう
    public Text[] categoryName = new Text[3];


	// Use this for initialization
	void Start () {
        TestInitParameter();

        SetDisplay("Resource");
        //SetDisplay("Population");
        //SetDisplay("Environment");

        
	}
	
	// Update is called once per frame
	void Update () {
	    SetMyParameter();
	}



    public void TestInitParameter(){
        PlayerPrefs.SetInt("Resource", 100);
        PlayerPrefs.SetInt("Population", 100);
        PlayerPrefs.SetInt("Environment", 100);
    }


    public void SetMyParameter(){
        this.categoryName[0].text = PlayerPrefs.GetInt("Resource").ToString();
        this.categoryName[1].text = PlayerPrefs.GetInt("Population").ToString();
        this.categoryName[2].text = PlayerPrefs.GetInt("Environment").ToString();

    }


    /// <summary>
    /// いわゆるアクティベート用
    /// </summary>
    /// <param name="in_categoryName">資源,人口,環境のどれか</param>
    public void SetDisplay(string in_categoryName)
    {
        switch (in_categoryName)
        {
            case "Resource":
                this.categoryLabel.text = "資源";
                CreateEachItem(0, this.displayPlace[0], "資源採取", "CollectResource", "自然環境に手を加え、エネルギー資源を得る", "Environment", this.levelUpButton[0]);
                CreateEachItem(1, this.displayPlace[1], "環境修復", "RestoreEnvironment", "植林技術などで、環境の回復能力を上げる", "Resource", this.levelUpButton[1]);
                CreateEachItem(2, this.displayPlace[2], "装備", "Equipment", "隕石の飛来により失われる命を減らすことができる", "Resource", this.levelUpButton[2]);                   
                break;

            case "Population":
                this.categoryLabel.text = "人口";
                CreateEachItem(0, this.displayPlace[0], "医学", "Medicina", "医療技術を発展させ、病気により失われる命を減らすことができる", "Resource", this.levelUpButton[0]);
                CreateEachItem(1, this.displayPlace[1], "食料", "Food", "災害時の食糧難により失われる命を減らすことができる", "Resource", this.levelUpButton[1]);
                break;

            case "Environment":
                this.categoryLabel.text = "環境";
                CreateEachItem(0, this.displayPlace[0], "資源再生", "ReproduceResource", "再生可能エネルギーの導入により、資源の消費量を減らす", "Resource", this.levelUpButton[0]);
                CreateEachItem(1, this.displayPlace[1], "衛生", "Health", "衛生技術を発展させ、疫病や災害の発生率を下げる", "Resource", this.levelUpButton[1]);                   
                break;
        }

    }


    /// <summary>
    /// 各システムオブジェクトを作成
    /// </summary>
    /// <param name="in_item">システムオブジェクトのインスタンス用</param>
    /// <param name="in_displayTrans">生成位置</param>
    /// <param name="in_systemName">システムオブジェクトの名前</param>
    /// <param name="in_prefsKey">PlayerPrefsのキーの名前</param>
    /// <param name="in_detailSentence">説明文</param>
    /// <param name="in_buttonLabel">レベルアップボタンのラベル名</param>
    public void CreateEachItem(int in_systemObjectIndex, Transform in_displayTrans, string in_systemName, string in_prefsKey, string in_detailSentence, string in_requiredCategory, Button in_levelUpButton)
    {
        // ここでin_item じゃなくて this.reinforceItem[in_indexnum] で呼べばいいかな
        this.reinforceItem[in_systemObjectIndex] = Instantiate(this.itemPrefab);
        this.reinforceItem[in_systemObjectIndex].transform.SetParent(in_displayTrans, false);
        in_levelUpButton = this.reinforceItem[in_systemObjectIndex].GetComponent<ReinforceItem>().SetParameter(this, in_systemName, in_prefsKey, in_detailSentence, in_requiredCategory);
    }



    public void MoveToMain()
    {
        SceneManager.LoadScene("PlayScene");
    }


    public void CheckButtonActivates()
    {
        for(int i=0; i<3; i++){
            if (this.reinforceItem[i] != null) this.reinforceItem[i].GetComponent<ReinforceItem>().CheckButtonActivate();
        }
    }
}
