using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class EnemyManager : MonoBehaviour {
	private const int ENEMY_POINT = 50;	// 敵の得点

	private GameObject gameManager;		// ゲームマネージャー

	public LayerMask blockLayer;		// ブロックのレイヤー

	private Rigidbody2D rbody;		// 敵制御用rigidbody2D

	private float moveSpeed = 1;		// 移動速度

	public enum MOVE_DIR				// 移動方向定義
	{
		LEFT,
		RIGHT,
	};

	private MOVE_DIR moveDirection = MOVE_DIR.LEFT;	// 移動方向

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();

		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		bool isBlock;		// 進行方向にブロックがあるか否か

		switch (moveDirection) {
		case MOVE_DIR.LEFT:		// 左に移動
			rbody.velocity = new Vector2 (moveSpeed * -1, rbody.velocity.y);
			transform.localScale = new Vector2 (1, 1);

			isBlock = Physics2D.Linecast (
				new Vector2 (transform.position.x, transform.position.y + 0.5f),
				new Vector2 (transform.position.x - 0.3f, transform.position.y + 0.5f), 
				blockLayer);

			if (isBlock) {
				moveDirection = MOVE_DIR.RIGHT;
			}

			break;
		case MOVE_DIR.RIGHT:	// 右に移動
			rbody.velocity = new Vector2 (moveSpeed, rbody.velocity.y);
			transform.localScale = new Vector2 (-1, 1);

			isBlock = Physics2D.Linecast (
				new Vector2 (transform.position.x, transform.position.y + 0.5f),
				new Vector2 (transform.position.x + 0.3f, transform.position.y + 0.5f), 
				blockLayer);

			if (isBlock) {
				moveDirection = MOVE_DIR.LEFT;
			}

			break;
		}
	}

	// 敵オブジェクト削除処理
	public void DestroyEnemy () {
		gameManager.GetComponent<GameManager> ().AddScore (ENEMY_POINT);

		rbody.velocity = new Vector2 (0, 0);
		//  コライダーを削除
		CircleCollider2D circleCollider = GetComponent<CircleCollider2D> ();
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		Destroy (circleCollider);
		Destroy (boxCollider);
		// 死亡アニメーション
		Sequence animSet = DOTween.Sequence();
		animSet.Append(transform.DOLocalMoveY(0.5f, 0.2f).SetRelative());
		animSet.Append (transform.DOLocalMoveY (-10.0f, 1.0f).SetRelative ());

		Destroy (this.gameObject, 1.2f);
	}
}
