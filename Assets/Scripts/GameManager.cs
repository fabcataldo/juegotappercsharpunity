using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[SerializeField] private TextMesh _boardText;
	[SerializeField] private Player _player;
	[SerializeField] Comensal[] _comensales;
	private int _score=0;
	private int _lifes=3;

	// Use this for initialization
	void Start () {
		UpdateBoard();
		StartGame();
	}
	
	public void StartGame(){
		_player.setCanMove();
		for(int i=0;i<_comensales.Length;i++){
			_comensales[i].StartComensalMove();
		}
	}

	void UpdateBoard(){
		_boardText.text = "Puntaje: "+_score.ToString();
		_boardText.text += "\nVidas del jugador: "+_lifes.ToString();
	}


	//Método que actualiza el puntaje, y trabaja en conjunto con AddScore()
	public void UpdateScore(){
		AddScore();
	}
	void AddScore(){
		_score+=10;
		UpdateBoard();
	}


	//Método que maneja las vidas, trabaja en conjunto con el que descuenta vidas
	public void PlayerDiscountLife(){
		DownLife();
	}
	void DownLife(){
		_lifes-=1;
		UpdateBoard();
		if(_lifes==0){
			GameOver();
		}
	}

	//Método que debería reiniciar el juego
	void GameOver(){
		SceneManager.LoadScene("Scene1");
	}
}
