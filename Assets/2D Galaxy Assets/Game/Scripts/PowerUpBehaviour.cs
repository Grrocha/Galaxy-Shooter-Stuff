using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour {
	[SerializeField]
	private float _powerupSpeed = 5.0f;
	[SerializeField]
	private int _powerupType = 0;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Boundary();
		transform.Translate(new Vector3(0,-1,0).normalized * Time.deltaTime * _powerupSpeed);
	}

	void OnTriggerEnter2D(Collider2D collidedCollider)
	{
		if(collidedCollider.tag == "Player")
		{
			PlayerBehaviour playerBehaviour = collidedCollider.GetComponent<PlayerBehaviour>();
			if(playerBehaviour != null)
			{
				CheckType(playerBehaviour);
				Destroy(this.gameObject);
			}
		}
	}

	void CheckType(PlayerBehaviour playerBehaviour)
	{
		if(_powerupType == 0)
		{
			playerBehaviour.FirePowerup(10, 1);
			
		}
		else if(_powerupType == 1)
		{
			playerBehaviour.SpeedPowerup(10, 10);
		}
		else if(_powerupType == 2)
		{
			playerBehaviour.ShieldPowerup(5);
		}

	}

	void Boundary()
	{
		if(transform.position.y <= -7)
		{
			Destroy(this.gameObject);
		}
	}
}
