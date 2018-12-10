using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	[SerializeField]
	private List<GameObject> _spawnableObjects = new List<GameObject>();
	[SerializeField]
	private float _spawnFrequency = 3.0f;
	private float _nextSpawn;
	[SerializeField]
	private float _powerUpChance = 7;
	void Start () {
		_nextSpawn = Time.time + _spawnFrequency;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= _nextSpawn)
		{
			Instantiate(_spawnableObjects[0], new Vector3(Random.Range(-8.0f, 8.0f), 6, 0), Quaternion.identity);
			CheckPowerUps();
			_nextSpawn = Time.time + _spawnFrequency;
		}

	}

	void CheckPowerUps()
	{
		if(Random.Range(0,10)>=_powerUpChance)
		{
			Instantiate(_spawnableObjects[Random.Range(1,_spawnableObjects.Count)], new Vector3(Random.Range(-8.0f, 8.0f), 6, 0), Quaternion.identity);	
		}
	}

}
