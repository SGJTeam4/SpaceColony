﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using System;

public class PlaySceneGameManager : MonoBehaviour {
	//オブジェクト参照用変数
	public GameObject shipModel;
	public Text leftUpText; 
	public Text Log1;
	public Text Log2;
	public Text Log3;
	public Text JinkoPercent;
	public Text ShigenPercent;
	public Text KankyoPercent;

	private int time = 0;//とりあえずのTime用変数
	public int nowYear = 0;
	public string leftUpStr;//左上のUIの文字用

	//基本パラメーター
	public int jinkoValue;
	public int shigenValue;
	public int kankyoValue;

	//強化項目の変数
	private int shigenSaiseiLevel = 1;
	private int kankyoSyuhukuLevel = 1;
	private int soubiLevel = 1;
	private int igakuLevel = 1;
	private int syokuryoLevel = 1;
	private int eiseiLevel = 1;
	private int shigenSaisyuLevel = 1;

	//イベントに関する変数
	private int insekiDamage = 30;
	private int infuruDamage = 30;
	private int kasaiDamage = 30;
	private int michiDamage = 40;
	private int mizuOsenDamage = 40;



	//---------------------------------------------------------
	//初期化用の関数群
	//---------------------------------------------------------
	void Awake(){
		Log1.text = "";
		Log2.text = "";
		Log3.text = "";
	}

	void Start () {
		int tmp = PlayerPrefs.GetInt (PlayerPrefsKeys.FirstFlag,0);
		tmp = 0;//とりあえず毎回初回起動フラグを立たせるためのゴリ押し。本来はタイトル画面で適切な処理をすると必要なくなります

		//Debug.Log("AAA" + (tmp));
		if (tmp == 0) {//初回起動のとき
			PlayerPrefs.SetInt (PlayerPrefsKeys.FirstFlag, 1);
			jinkoValue = 80;
			shigenValue = 100;
			kankyoValue = 100;
		} else {
			jinkoValue = PlayerPrefs.GetInt (PlayerPrefsKeys.Population);
			shigenValue = PlayerPrefs.GetInt (PlayerPrefsKeys.Resource);
			kankyoValue = PlayerPrefs.GetInt (PlayerPrefsKeys.Environment);
		}
	}


	//情報表示ログを追加する
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

		//ターン毎の状態更新と修正
		jinkoUpdate ();
		shigenUpdate ();
		kankyoUpdate ();
		paraSyusei ();

		//イベント時の状態更新と修正
		eventUpdate ();
		paraSyusei ();

		//UIに出力
		JinkoPercent.text = jinkoValue + "%";
		ShigenPercent.text = shigenValue + "%";
		KankyoPercent.text = kankyoValue + "%";
	}

	void paraSyusei(){
		if (jinkoValue > 100)
			jinkoValue = 100;
		if (jinkoValue < 0)
			jinkoValue = 0;
		if (kankyoValue > 100)
			kankyoValue = 100;
		if (kankyoValue < 0)
			kankyoValue = 0;
		if (shigenValue > 100)
			shigenValue = 100;
		if (shigenValue < 0)
			shigenValue = 0;
	}

	void Update () {
		if (time % 60 == 0) {
			yearUpdate ();
		}
		time++;


		shipModel.transform.Rotate (
			new Vector3(
				0,
				0,
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




	//---------------------------------------------------------
	//ボタンクリック時の関数
	//---------------------------------------------------------
	public void kankyoClicked(){
		saveData ();
		/*
		kankyoValue -= 10;
		shigenValue += 20 * jinkoValue * 0.01;
		refresh ();*/
	}

	public void shigenClicked(){
		saveData ();
	}

	public void jinkoClicked(){
		saveData ();
	}


	public void saveData(){
		PlayerPrefs.SetInt (PlayerPrefsKeys.Population			, jinkoValue);
		PlayerPrefs.SetInt (PlayerPrefsKeys.Resource			, shigenValue);
		PlayerPrefs.SetInt (PlayerPrefsKeys.Environment			, kankyoValue );
		PlayerPrefs.SetInt (PlayerPrefsKeys.CollectResource		, shigenSaisyuLevel);
		PlayerPrefs.SetInt (PlayerPrefsKeys.RestoreEnvironment	, kankyoSyuhukuLevel);
		PlayerPrefs.SetInt (PlayerPrefsKeys.Equipment			, soubiLevel );
		PlayerPrefs.SetInt (PlayerPrefsKeys.Medicine			, igakuLevel );
		PlayerPrefs.SetInt (PlayerPrefsKeys.Food				, syokuryoLevel);
		PlayerPrefs.SetInt (PlayerPrefsKeys.ReproduceResource	, shigenSaiseiLevel);
		PlayerPrefs.SetInt (PlayerPrefsKeys.Health				, eiseiLevel);
	}

}
