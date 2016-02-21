using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System;

public class PlaySceneGameManager : MonoBehaviour {
	public static int GAME_CLEAR = 0;
	public static int GAME_OVER = 1;
	public static int GAME_PLAYING = 2;




	//オブジェクト参照用変数
	public GameObject shipModel;
	public GameObject canvas;
	public GameObject clearCanvas;
	public GameObject gameoverCanvas;
	public GameObject clearMessage;
	public GameObject gameoverMessage;
	public Text leftUpText; 
	public Text Log1;
	public Text Log2;
	public Text Log3;
	public Text JinkoPercent;
	public Text ShigenPercent;
	public Text KankyoPercent;

	private int time = 0;//とりあえずのTime用変数
	private int gameState = GAME_PLAYING;
	public int nowYear = 0;
	public string leftUpStr;//左上のUIの文字用


	public string insekiMessage = "";

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
	private int insekiDamage = 18;
	private int infuruDamage = 18;
	private int kasaiDamage = 18;
	private int michiDamage = 25;
	private int mizuOsenDamage = 25;
	private int kachikuDamage = 20;



	//---------------------------------------------------------
	//初期化用の関数群
	//---------------------------------------------------------
	void Awake(){
		Log1.text = "";
		Log2.text = "";
		Log3.text = "";
	}

	void Start () {
		//audioSource = gameObject.GetComponent<AudioSource>();



		int tmp = PlayerPrefs.GetInt (PlayerPrefsKeys.FirstFlag,0);
		//tmp = 0;//とりあえず毎回初回起動フラグを立たせるためのゴリ押し。本来はタイトル画面で適切な処理をすると必要なくなります

		//Debug.Log("AAA" + (tmp));
		if (tmp == 0) {//初回起動のとき
			PlayerPrefs.SetInt (PlayerPrefsKeys.FirstFlag, 1);
			jinkoValue = 80;
			shigenValue = 100;
			kankyoValue = 100;

			leftUpStr = "超スーパー銀河暦" + (nowYear+1) + "年";
			leftUpText.text = leftUpStr;
		} else {
			loadData ();
			leftUpStr = "超スーパー銀河暦" + (nowYear) + "年";
			leftUpText.text = leftUpStr;
		}

		//UIに出力
		JinkoPercent.text = jinkoValue + "%";
		ShigenPercent.text = shigenValue + "%";
		KankyoPercent.text = kankyoValue + "%";
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



	//------------------------------------------------------
	//発展形イベント関数
	//------------------------------------------------------
	void devEventUpdate(){
		int intResult = (int)(Random.value * 2);
		switch (intResult) {
		case 0:
			newDevSuccessEventUpdate ();
			break;
		case 1:
			findPlanetEventUpdate ();
			break;
		}
	}

	void newDevSuccessEventUpdate(){
		jinkoValue += 10;
		addLog (nowYear+"年目：　農産局が収穫量の多い稲の開発に成功、人口が10％増加しました");
	}

	void findPlanetEventUpdate(){
		shigenValue += 20;
		addLog (nowYear+"年目：　隕石群の中に鉱脈が発見され、資源を20％増やすことができました");
	}

	//---------------------------------------------------
	//イベント系関数
	//---------------------------------------------------
	void eventUpdate(){

		int intResult = 0;

		float tmp;
		if (nowYear >= 0) {
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
					kachikuEvent ();
					break;
				}
				return;
			} 

			tmp = Random.value * 100;
			if (tmp < nowYear * 0.2) {
				InsekiEvent ();
				return;
			}
		}

		if(Random.value * 100 < 20){
			devEventUpdate ();
		}

	}
	void InsekiEvent(){
		jinkoValue -= insekiDamage - soubiLevel * 3;
		kankyoValue -= insekiDamage - soubiLevel * 3;
		addLog (nowYear+"年目：　空調区画に隕石が衝突、人口が"+ (insekiDamage - soubiLevel * 3) +"％、環境が"+ (insekiDamage - soubiLevel * 5) +"％減少しました");
	}

	void infuruEvent(){
		jinkoValue -= infuruDamage - igakuLevel * 3;
		addLog (nowYear+"年目：　インフルエンザが流行し、人口が"+(infuruDamage - igakuLevel * 3)+"％減少\n");
	}

	void kasaiEvent(){
		jinkoValue -= kasaiDamage - syokuryoLevel * 3;
		addLog (nowYear+"年目：　第三居住区画で火災事故が発生。人口の" + (kasaiDamage - syokuryoLevel * 3) + "％が失われました");
	}

	void michiEvent(){
		jinkoValue -= michiDamage - igakuLevel * 3;
		addLog (nowYear+"年目：　新型流行病が発生。人口の"+ (michiDamage - igakuLevel * 3)+"％が失われました\n");
	}

	void mizuOsenEvent(){
		jinkoValue -= mizuOsenDamage - syokuryoLevel * 3;
		addLog (nowYear+"年目：　排水による水質汚染が深刻化し、人口が"+ (mizuOsenDamage - syokuryoLevel * 3 )+"％減りました");
	}

	void kachikuEvent(){
		jinkoValue -= kachikuDamage - igakuLevel * 3;
		addLog (nowYear+"年目：　排水による水質汚染が深刻化し、人口が"+ (kachikuDamage - igakuLevel * 3 )+"％減りました");
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

	bool judgeGameover(){
		if (jinkoValue <= 0 || kankyoValue <= 0 || shigenValue <= 0) {
			return true;
		}
		return false;

	}

	void gameClearUpdate(){
		time++;
		if (time >= 300) {
			SceneManager.LoadScene("Title");
		}
	}

	void gamePlayingUpdate(){

		if (nowYear >= 300) {
			gameState = GAME_CLEAR;
			time = 0;
			canvas.SetActive (false);
			clearCanvas.SetActive (true);

			//audioSource.Play();
		}

		if (judgeGameover () == true) {
			gameState = GAME_OVER;
			time = 0;
		}

		if (time % 60 == 59) {
			yearUpdate ();
		}
		time++;


	}

	void gameoverUpdate(){

		time++;
		if (time >= 500) {
			SceneManager.LoadScene("Title");
		}

		if (time == 100) {
			canvas.SetActive (false);
		}
		if (time == 200) {
			gameoverCanvas.SetActive (true);
		}
		shipModel.transform.Rotate (
			new Vector3(
				0,
				0,
				0.2f * 60.0f * Time.deltaTime
			),
			Space.Self
		);
	}

	void Update () {

		switch(gameState){
		case 2:
			gamePlayingUpdate ();
			break;
		case 0:
			gameClearUpdate ();
			break;
		case 1:
			gameoverUpdate ();
			break;
		}


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

		PlayerPrefs.SetString (PlayerPrefsKeys.ActivateCategory			, PlayerPrefsKeys.Environment	);
		SceneManager.LoadScene("Reinforce");

		//kankyoValue -= 10;
		//shigenValue += (int)(20 * jinkoValue * 0.01);

		//refresh ();
	}

	public void shigenClicked(){
		saveData ();
		PlayerPrefs.SetString (PlayerPrefsKeys.ActivateCategory			, PlayerPrefsKeys.Resource	);
		SceneManager.LoadScene("Reinforce");
	}

	public void jinkoClicked(){
		saveData ();
		PlayerPrefs.SetString (PlayerPrefsKeys.ActivateCategory			, PlayerPrefsKeys.Population	);
		SceneManager.LoadScene("Reinforce");
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
		PlayerPrefs.SetInt (PlayerPrefsKeys.NowYear				, nowYear);
	}

	void loadData(){
		shigenSaiseiLevel = PlayerPrefs.GetInt (PlayerPrefsKeys.LV_ReproduceResource);
		kankyoSyuhukuLevel = PlayerPrefs.GetInt (PlayerPrefsKeys.LV_RestoreEnvironment);
		soubiLevel = PlayerPrefs.GetInt (PlayerPrefsKeys.LV_Equipment);
		igakuLevel = PlayerPrefs.GetInt (PlayerPrefsKeys.LV_Medicine);
		syokuryoLevel = PlayerPrefs.GetInt (PlayerPrefsKeys.LV_Food);
		eiseiLevel = PlayerPrefs.GetInt (PlayerPrefsKeys.LV_Health);
		shigenSaisyuLevel = PlayerPrefs.GetInt (PlayerPrefsKeys.LV_CollectResource);
		jinkoValue = PlayerPrefs.GetInt (PlayerPrefsKeys.Population);
		shigenValue = PlayerPrefs.GetInt (PlayerPrefsKeys.Resource);
		kankyoValue = PlayerPrefs.GetInt (PlayerPrefsKeys.Environment);
		nowYear = PlayerPrefs.GetInt (PlayerPrefsKeys.NowYear);

	}



}
