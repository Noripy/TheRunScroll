using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour {

	public GameObject player;		//プレイヤーオブジェクト

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//プレイヤーキャラをカメラが追いかけていくようにする
		if (player != null) {			//プレイヤーキャラは存在しているか？
			//存在していればカメラポジションを設定

			//カメラポジションを決定
			//現在のプレイヤーのX座標をカメラのX座標に設定
			Vector3 cameraPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

			//カメラが左端を越えて動かないように
			if(cameraPos.x < 0.0f){
				cameraPos.x = 0.0f;
			}

			//カメラが右端を越えて動かないように
			if(cameraPos.x > 25.6f){
				cameraPos.x = 25.6f;
			}

			//カメラポジションを変更
			transform.position = cameraPos;
		}
	}
}
