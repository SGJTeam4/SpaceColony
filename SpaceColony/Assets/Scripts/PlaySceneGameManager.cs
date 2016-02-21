using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using System;

public class PlaySceneGameManager : MonoBehaviour {
	public GameObject shipModel;
	public Text leftUpText; 
	public Text Log1;
	public Text Log2;
	public Text Log3;
	public Text JinkoPercent;
	public Text ShigenPercent;
	public Text KankyoPercent;


	private int time = 0;
	public int nowYear = 0;

	public string log1 = "test1";
	public string log2 = "text2";
	public string log3 = "テスト３";
	public string leftUpStr;
	public string eventLog= "イベント１";

	//基本パラメーター
	public double jinkoValue;
	public double shigenValue;
	public double kankyoValue;

	//強化項目の変数
	private int shigenSaiseiLevel = 1;
	private int kankyoSyuhukuLevel = 1;
	private int soubiLevel = 1;
	private int igakuLevel = 1;
	private int syokuryoLevel = 1;
	private int eiseiLevel = 1;


	//イベントに関する変数
	private int insekiDamage = 30;
	private int infuruDamage = 30;
	private int kasaiDamage = 30;
	private int michiDamage = 40;
	private int mizuOsenDamage = 40;


	void Awake(){

		Log1.text = "";
		Log2.text = "";
		Log3.text = "";
	}

	// Use this for initialization
	void Start () {
		jinkoValue = 80;
		shigenValue = 100;
		kankyoValue = 100;
	}
	void addLog(string str){
		Log3.text = Log2.text;
		Log2.text = Log1.text;
		Log1.text = str;
	}



	//-------------------------------------------------------
	//基本パラメーターのアップデート関数
	//-------------------------------------------------------

	void jinkoUpdate(){

		jinkoValue += 2;
		if (jinkoValue > 100) {
			jinkoValue = 100;
		}
	}

	void shigenUpdate(){
		shigenValue -= (int)(jinkoValue * 0.1 - shigenSaiseiLevel);
	}

	void kankyoUpdate(){
		kankyoValue += kankyoSyuhukuLevel;
		if (kankyoValue > 100) {
			kankyoValue = 100;
		}
	}




	//---------------------------------------------------
	//イベント系関数
	//---------------------------------------------------
	void eventUpdate(){

		int intResult = 0;
		float tmp;
		if (Random.value * 100 <= nowYear * (1.3 - kankyoValue * 0.01) - eiseiLevel * 5) {
			intResult = (int)(Random.value * 5);
			switch (intResult) {
			case 0:
				infuruEvent ();
				break;
			case 1:
				kasaiEvent ();
				break;
			case 2:
				michiEvent ();
				break;
			case 3:
				mizuOsenEvent ();
				break;
			case 4:

				break;
			}
		} else{
			tmp = Random.value * 100;
			Debug.Log("AAA" + (tmp) + " < " +(nowYear * 0.2));
			if(tmp < nowYear * 0.2){

				InsekiEvent ();
			}
			else if(Random.value * 100 < 20){

			}
		}
	}
	void InsekiEvent(){
		jinkoValue -= insekiDamage - soubiLevel * 5;
		kankyoValue -= insekiDamage - soubiLevel * 5;
		addLog (nowYear+"年目：　いんせきなう");
	}

	void infuruEvent(){
		jinkoValue -= infuruDamage - igakuLevel * 5;
		addLog (nowYear+"年目：　いんふるなう");
	}

	void kasaiEvent(){
		jinkoValue -= kasaiDamage - syokuryoLevel * 5;
		addLog (nowYear+"年目：　かさいなう");
	}

	void michiEvent(){
		jinkoValue -= michiDamage - igakuLevel * 5;
		addLog (nowYear+"年目：　みちのやまうなう");
	}

	void mizuOsenEvent(){
		jinkoValue -= mizuOsenDamage - syokuryoLevel * 5;
		addLog (nowYear+"年目：　水質汚染なう");
	}


	//---------------------------------------------------------
	//基本アップデート関数
	//---------------------------------------------------------
	void yearUpdate(){
		nowYear++;
		leftUpStr = "超スーパー銀河暦" + nowYear + "年";
		leftUpText.text = leftUpStr;

		jinkoUpdate ();
		shigenUpdate ();
		kankyoUpdate ();
		eventUpdate ();


		JinkoPercent.text = jinkoValue + "%";
		ShigenPercent.text = shigenValue + "%";
		KankyoPercent.text = kankyoValue + "%";

	}

	// Update is called once per frame
	void Update () {
		if (time % 60 == 0) {
			yearUpdate ();
		}
		time++;

		shipModel.transform.Rotate (
			new Vector3(
				0,0,
				//0,//Input.GetAxis("Vertical") * 60.0f * Time.deltaTime,
				-0.2f * 60.0f * Time.deltaTime

			
			),
			Space.Self
		);
		/*
		shipModel.transform.Rotate (
			new Vector3(
				0,//Input.GetAxis("Vertical") * 60.0f * Time.deltaTime,
				-0.2f * 60.0f * Time.deltaTime,
				0
			),
			Space.World
		);*/
	}


	void refresh(){

		JinkoPercent.text = jinkoValue + "%";
		ShigenPercent.text = shigenValue + "%";
		KankyoPercent.text = kankyoValue + "%";
	}

	/// <summary>
	/// ボタン系関数
	/// </summary>
	public void buttonClicked(){
		kankyoValue -= 10;
		shigenValue += 20 * jinkoValue * 0.01;
		refresh ();
	}
}
