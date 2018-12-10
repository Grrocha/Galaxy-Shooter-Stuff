using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {


	private float _enemySpeed = 7.0f;
	[SerializeField]
	private GameObject _enemyExplosion;
	private UIManager _uiManager;

	void Start () {
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Boundary();
		EnemyMovement();
	}

	void Boundary()
	{
		if(transform.position.y <= -7)
		{
			transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 6, 0);
		}
	}

	void EnemyMovement()
	{
		transform.Translate(new Vector3(0,-1,-1).normalized*_enemySpeed*Time.deltaTime);
	}

	public void Damage()
	{
		Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
		if(_uiManager != null)
		{
			_uiManager.UpdateScore();
		}
		Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D collidedCollider)
	{
		if(collidedCollider.tag == "Player")
		{
			PlayerBehaviour playerBehaviour = collidedCollider.GetComponent<PlayerBehaviour>();
			if(playerBehaviour != null)
			{
				playerBehaviour.Damage();
				Damage();
			}
		}
	}


}
