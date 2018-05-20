using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {
	private GameManager _gm;
	[SerializeField] private Text _totalScore;

	// Use this for initialization
	void Start () {
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		_totalScore.text = _totalScore.text+_gm.GetActualInstanceGameManager().getScore().ToString()+" puntos";	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame(){
		//Debug.Log("PEPEE");
		SceneManager.LoadScene("Scene1");
	}

	public void ExitGame(){
		Application.Quit();
	}
}
