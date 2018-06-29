using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[SerializeField] private TextMesh _boardText;
	[SerializeField] private Player _player;
	[SerializeField] Comensal[] _comensales;
	
	private int _lifes=3;

/* 
	private static GameManager _instance = null;
	public static GameManager Instance {
		
		get {
			if(_instance == null) {
				_instance = FindObjectOfType<GameManager>();
				if(_instance == null) {
					GameObject gameManagerObject = new GameObject("GameManager");
					_instance = gameManagerObject.AddComponent<GameManager>();
					DontDestroyOnLoad(gameManagerObject);
				} 
			}

			return _instance;
		}
	}
*/
	
	// Use this for initialization
	void Start () {
		Constants.SCORE = 0;
		_lifes=3;
		_comensales = FindObjectsOfType<Comensal>();
		_boardText = GameObject.FindGameObjectWithTag("BoardText").GetComponent<TextMesh>();
		_player = FindObjectOfType<Player>();

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
		_boardText.text = $"Score: {Constants.SCORE} \nLifes: {_lifes}";
	}


	//Método que actualiza el puntaje, y trabaja en conjunto con AddScore()
	public void UpdateScore(){
		AddScore();
	}
	void AddScore(){
		Constants.SCORE += 10;
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


	void GameOver(){
		if(SceneManager.sceneCountInBuildSettings==1){
			SceneManager.LoadScene("Scene1");
		}
		else{
			SceneManager.LoadScene("GameOver");
		}
	}
}
