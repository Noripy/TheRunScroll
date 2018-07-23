using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// 定数定義
	private const int MAX_SCORE = 999999;// スコア最大数

	public int stageNo;					// ステージナンバー

	public GameObject textGameOver;		// 「ゲームオーバー」テキスト
	public GameObject textClear;		// 「クリア」テキスト
	public GameObject buttons;			// 操作ボタン
	public GameObject textScoreNumber;	// スコアテキスト

	public enum GAME_MODE{				// ゲーム状態定義
		PLAY,								// プレイ中
		CLEAR,								// クリア
		GAMEOVER,							// ゲームオーバー
	};
	public GAME_MODE gameMode = GAME_MODE.PLAY;	// ゲーム状態

	private int score = 0;				// スコア
	private int displayScore = 0;		// 表示用スコア

	public AudioClip clearSE;			// 効果音：クリア
	public AudioClip gameoverSE;		// 効果音：ゲームオーバー

	private AudioSource audioSource;	// オーディオソース

	// Use this for initialization
	void Start () {
		audioSource = this.gameObject.GetComponent<AudioSource> ();

		RefreshScore ();
	}
	
	// Update is called once per frame
	void Update () {
		if (score > displayScore) {
			displayScore += 10;

			if (displayScore > score) {
				displayScore = score;
			}

			RefreshScore ();
		}
	}

	// ゲームオーバー処理
	public void GameOver () {
		audioSource.PlayOneShot (gameoverSE);
		textGameOver.SetActive (true);
		buttons.SetActive (false);

		Invoke ("GoBackStageSelect", 2.0f);
	}

	// ゲームクリア処理
	public void GameClear () {
		audioSource.PlayOneShot (clearSE);
		gameMode = GAME_MODE.CLEAR;
		textClear.SetActive (true);
		buttons.SetActive (false);

		// セーブデータ更新
		if (PlayerPrefs.GetInt ("CLEAR", 0) < stageNo) {
			PlayerPrefs.SetInt ("CLEAR", stageNo);
		}
		Invoke ("GoBackStageSelect", 2.0f);
	}

	// スコア加算
	public void AddScore (int val) {
		score += val;
		if (score > MAX_SCORE) {
			score = MAX_SCORE;
		}
	}

	// スコア表示を更新
	void RefreshScore () {
		textScoreNumber.GetComponent<Text> ().text = displayScore.ToString ();
	}

	// ステージセレクトシーンに戻る
	void GoBackStageSelect () {
		SceneManager.LoadScene ("StageSelectScene");
	}
}
