using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

	[SerializeField]
	private float _laserSpeed = 10.0f;
	
	void Start ()
	{
		_laserSpeed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		LaserMove();
		CheckDestroy();
	}

	void CheckDestroy()
	{
		if(transform.position.y >= 6)
		{
			Destroy(this.gameObject);
		}
	}

	void LaserMove()
	{
		transform.Translate(new Vector3(0,1,0).normalized*_laserSpeed*Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collidedCollider)
	{
		if(collidedCollider.tag == "Enemy")
		{
			EnemyBehaviour enemyBehaviour = collidedCollider.GetComponent<EnemyBehaviour>();
			if(enemyBehaviour != null)
			{
				enemyBehaviour.Damage();
				Destroy(this.gameObject);
			}
		}
	}

}
