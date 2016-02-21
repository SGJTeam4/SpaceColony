using UnityEngine;
using System.Collections;

public class PlayerPrefsKeys {
	//シーン間で共通だと思われるもの
	public static readonly string Resource = "Resource";//資源
	public static readonly string Population = "Population";//人口
	public static readonly string Environment = "Environment";//環境
	public static readonly string CollectResource = "CollectResource";//資源採取
	public static readonly string RestoreEnvironment= "RestoreEnvironment";//環境修復
	public static readonly string Equipment= "Equipment";//装備
	public static readonly string Medicine= "Medicine";//医学
	public static readonly string Food= "Food";//食料
	public static readonly string ReproduceResource= "ReproduceResource";//資源再生
	public static readonly string Health= "Health";//衛生
	public static readonly string FirstFlag = "FirstFlag";//初回起動フラグ  タイトルで０に初期化設定してほしい

	//強化画面で使うような系のやつ
	public static readonly string ActivateCategory =  "ActivateCategory";
	public static readonly string LV_CollectResource = "Lv_CollectResource";//資源採取Level
	public static readonly string LV_RestoreEnvironment = "Lv_RestoreEnvironment";//環境修復Level
	public static readonly string LV_Equipment = "Lv_Equipment";//装備Level
	public static readonly string LV_Medicine = "Lv_Medicine";//医学Level
	public static readonly string LV_Food = "Lv_Food";//食料Level
	public static readonly string LV_ReproduceResource = "Lv_ReproduceResource";//資源再生Level
	public static readonly string LV_Health = "Lv_Health";//衛生Level

	//PlaySceneでしか使わないんじゃないかなってもの
	public static readonly string NowYear = "NowYear";
}
