using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 強化画面の制御用クラス
/// </summary>
public class ReinforceSceneController : MonoBehaviour {
    /*
    public class Item(){
        	//string in_systemName, int in_level, string in_detailSentence, string in_buttonLabel
        private string _systemName;
        private string _detailSentence;
        private string _buttonLabel;

        public string systemName{get;}
        public string detailSentece{get;}
        public string buttnLabel{get;}

        public Item(string in_name, string in_detailSentece, string in_buttonLabel){
            this._systemName = in_name;
            this._detailSentence = in_detailSentece;
            this._buttonLabel = in_buttonLabel;
            
        }

    }

    */

    public Transform[] displayPlace = new Transform[3];

    // 各システムオブジェクトのインスタンス
    private GameObject[] reinforceItem = new GameObject[3]; 


    // 左上のボタンラベル
    public Text categoryLabel;


    public GameObject itemPrefab;


	// Use this for initialization
	void Start () {
        //SetDisplay("Resource");
        //SetDisplay("Population");
        SetDisplay("Environment");
	}
	
	// Update is called once per frame
	void Update () {
	
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
                CreateEachItem(this.reinforceItem[0], this.displayPlace[0], "資源採取", "Lv_ReproduceResource", "自然環境に手を加え、エネルギー資源を得る", "環境" + GetChangeValue("ReproduceResource"));
                CreateEachItem(this.reinforceItem[1], this.displayPlace[1], "環境修復", "Lv_RestoreResource", "植林技術などで、環境の回復能力を上げる", "資源" + GetChangeValue("_RestoreResource"));
                CreateEachItem(this.reinforceItem[2], this.displayPlace[2], "装備", "Lv_Equipment", "隕石の飛来により失われる命を減らすことができる", "資源" + GetChangeValue("Equipment"));                   
                break;

            case "Population":
                this.categoryLabel.text = "人口";
                CreateEachItem(this.reinforceItem[0], this.displayPlace[0], "医学", "Lv_Medicina", "医療技術を発展させ、病気により失われる命を減らすことができる", "資源" + GetChangeValue("Medicina"));
                CreateEachItem(this.reinforceItem[1], this.displayPlace[1], "食料", "Lv_Food", "災害時の食糧難により失われる命を減らすことができる", "資源" + GetChangeValue("Food"));
                break;

            case "Environment":
                this.categoryLabel.text = "環境";
                CreateEachItem(this.reinforceItem[0], this.displayPlace[0], "資源再生", "Lv_ReproduceResource", "再生可能エネルギーの導入により、資源の消費量を減らす", "資源" + GetChangeValue("ReproduceResource"));
                CreateEachItem(this.reinforceItem[1], this.displayPlace[1], "衛生", "Lv_Health", "衛生技術を発展させ、疫病や災害の発生率を下げる", "資源" + GetChangeValue("Health"));                   
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
    public void CreateEachItem(GameObject in_item, Transform in_displayTrans, string in_systemName, string in_prefsKey, string in_detailSentence, string in_buttonLabel)
    {
        in_item = Instantiate(this.itemPrefab);
        in_item.transform.SetParent(in_displayTrans, false);
        in_item.GetComponent<ReinforceItem>().SetParameter(in_systemName, in_prefsKey, in_detailSentence, in_buttonLabel);

    }


    /// <summary>
    /// レベルアップによって変動する値を取得
    /// </summary>
    /// <param name="in_systemName">システムの名前(分岐するためです)</param>
    /// <returns></returns>
    public int GetChangeValue(string in_systemName){
        int value = 0;

        // とりあえず
        value = -10;

        return value;
    }



}
