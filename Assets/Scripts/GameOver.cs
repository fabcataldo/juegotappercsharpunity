using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {
	private GameManager _gm;
	[SerializeField] private Transform[] buttons;
	[SerializeField] private Text _totalScore;
	

	// Use this for initialization
	void Start () {
		//obtengo la referencia del GameManager
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		//le pongo texto al Total Score Board de la escena del Game Over
		_totalScore.text = _totalScore.text+_gm.GetActualInstanceGameManager().getScore().ToString()+" puntos";	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void RestartGame(){
			SceneManager.LoadScene("Scene1");

	}

	public void ExitGame(){
			Application.Quit();
	}
}
