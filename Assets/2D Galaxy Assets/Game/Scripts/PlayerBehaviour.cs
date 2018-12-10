using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public int fireMode = 0;
	public int playerLives = 3;

	[SerializeField]
	private float _playerSpeed = 5.0f;
	[SerializeField]
	private float _maxboundaryY = 0.0f;
	[SerializeField]
	private float _minboundaryY = -4.2f;
	[SerializeField]
	private float _maxboundaryX = 9.5f;
	[SerializeField]
	private float _minboundaryX = -9.5f;
	[SerializeField]
	private List<GameObject> _laserObject = new List<GameObject>();
	private float _elapsedCooldown;
	private bool _firePowerup;
	private bool _speedPowerup;
	[SerializeField]
	private bool _shieldPowerup;
	private float _fireTimeout;
	private float _speedTimeout;
	private float _shieldTimeout;
	[SerializeField]
	private GameObject _Explosion;
	[SerializeField]
	private GameObject _shieldObject;

	private UIManager _uiManager;

	void Start ()
	{
		transform.position = new Vector3(0,0,0);
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		if(_uiManager != null)
		{
			_uiManager.UpdateLives(playerLives);
		}
	}
	
	// Update is called once per frame
	void Update() {
		
		PlayerMovement();
		Bound();
		if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
		{
			CheckFire();
		}
		_elapsedCooldown += 1*Time.deltaTime;

		if(_firePowerup == true)
		{
			if(Time.time >= _fireTimeout)
			{
				CancelFirePowerup();
			}
		}
		if(_speedPowerup == true)
		{
			if(Time.time >= _speedTimeout)
			{
				CancelSpeedPowerup();
			}
		}
		if(_shieldPowerup == true)
		{
			if(Time.time >= _shieldTimeout)
			{
				CancelShieldPowerup();
			}
		}
		if(playerLives <= 0)
		{
			Die();
		}

	}

	private void PlayerMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		transform.Translate(new Vector3(1,0,0).normalized*_playerSpeed*horizontalInput*Time.deltaTime);
		transform.Translate(new Vector3(0,1,0).normalized*_playerSpeed*verticalInput*Time.deltaTime);
	}

	private void Bound()
	{
		if(transform.position.y >= _maxboundaryY)
		{
			transform.position = new Vector3(transform.position.x, _maxboundaryY, 0);
		}
		else if(transform.position.y <= _minboundaryY)
		{
			transform.position = new Vector3(transform.position.x, _minboundaryY, 0);
		}

		if (transform.position.x > _maxboundaryX)
		{
			transform.position = new Vector3(_minboundaryX, transform.position.y, 0);
		}
		else if (transform.position.x < _minboundaryX)
		{
			transform.position = new Vector3(_maxboundaryX, transform.position.y, 0);
		}

	}

	private void CheckFire()
	{
			CheckCooldown(0.25f, _laserObject[fireMode]);
			//Instantiate(_laserObject, transform.position + new Vector3(0,0.88f,0), Quaternion.identity);
	}

	private void CheckCooldown(float cooldownRate, GameObject weapon)
	{
		if(_elapsedCooldown >= cooldownRate)
		{
			Fire(weapon);
			_elapsedCooldown = 0;
		}
	}

	private void Fire(GameObject weapon)
	{
		Instantiate(weapon, transform.position + new Vector3(0,0.88f,0), Quaternion.identity);
	}

	public void FirePowerup(float timeout, int newFireMode)
	{
		 fireMode = newFireMode;
		 _firePowerup = true;
		 _fireTimeout = Time.time + timeout;
	}

	public void SpeedPowerup(float timeout, int newSpeed)
	{
		 _playerSpeed = newSpeed;
		 _speedPowerup = true;
		 _speedTimeout = Time.time + timeout;
	}

	public void ShieldPowerup(float timeout)
	{
		 _shieldPowerup = true;
		 _shieldObject.SetActive(_shieldPowerup);
		 _shieldTimeout = Time.time + timeout;
	}
	public void CancelShieldPowerup()
	{
		_shieldPowerup = false;
		_shieldObject.SetActive(_shieldPowerup);
	}

	public void CancelFirePowerup()
	{
		fireMode = 0;
		_firePowerup = false;
	}

	public void CancelSpeedPowerup()
	{
		_playerSpeed = 5.0f;
		_speedPowerup = false;
	}

	public void Damage()
	{
		if(!_shieldPowerup)
		{
			playerLives--;
			if(_uiManager != null)
			{
				_uiManager.UpdateLives(playerLives);
			}
		}

	}

	void Die()
	{
		Instantiate(_Explosion, transform.position, Quaternion.identity);
		_uiManager.playerScore = 0;
		_uiManager.isOnMenu = true;
		Destroy(this.gameObject);
	}

}
