using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Sprite[] livesSprites = new Sprite[4];
	public Image livesDisplay;
	public Text scoreDisplay;
	public int playerScore = 0;
	public bool isOnMenu = true;
	public GameObject spawnManager;
	public GameObject mainMenu;
	public GameObject playerPrefab;
	// Use this for initialization
	void Start () {
		spawnManager = GameObject.Find("SpawnManager");
		if(spawnManager != null)
		{
			spawnManager.SetActive(false);
		}
		scoreDisplay.text = "Score: " + playerScore;
	}
	
	// Update is called once per frame
	void Update () {
		if(isOnMenu == true)
		{
			mainMenu.SetActive(true);
			spawnManager.SetActive(false);
		}
		else
		{
			mainMenu.SetActive(false);
			spawnManager.SetActive(true);
		}

		if(isOnMenu && Input.GetKeyDown(KeyCode.Space))
		{
			scoreDisplay.text = "Score: " + playerScore;
			Instantiate(playerPrefab);
			isOnMenu = !isOnMenu;
		}


	}

	public void UpdateLives(int lives)
	{
		livesDisplay.sprite = livesSprites[lives];
	}

	public void UpdateScore()
	{
		playerScore++;
		scoreDisplay.text = "Score: " + playerScore;
	}

}
