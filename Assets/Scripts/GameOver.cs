using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {
	
	[SerializeField] private Transform[] buttons;
	[SerializeField] private Text _totalScore;
	

	// Use this for initialization
	void Start () {
		//obtengo la referencia del GameManager
		
		//le pongo texto al Total Score Board de la escena del Game Over
		_totalScore.text = $"Puntaje total: {Constants.SCORE} puntos";	
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
