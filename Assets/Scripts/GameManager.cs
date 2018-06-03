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

	public int getScore(){
		return _score;
	}

	//variable static cuyo contenido es compartido por todas las instancias de esta clase
	public static GameManager ActualGameManager;

	//Awake() es parecido a Start() nada mas que prepara las instancias de los GameObjects, el Start() ya los usa
	void Awake(){
		//creo una sola instancia para _actualGameManager, y hago que sobreviva en todo el juego
		//y que cuando cambie de escena, no se destruya
		if(ActualGameManager==null){
			ActualGameManager=this;
			DontDestroyOnLoad(gameObject);
		}
	}

	//devuelvo la instancia actual del GameManager
	public GameManager GetActualInstanceGameManager(){
		return ActualGameManager;
	}
	
	// Use this for initialization
	void Start () {
		UpdateBoard();
		StartGame();
	}
	
	public void StartGame(){
		_score=0;
		_lifes=3;
		_player.setCanMove();
		for(int i=0;i<_comensales.Length;i++){
			_comensales[i].StartComensalMove();
		}
	}

	void UpdateBoard(){
		_boardText.text = "Puntaje actual: "+_score.ToString();
		_boardText.text += "\nVidas restantes: "+_lifes.ToString();
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


	void GameOver(){
		SceneManager.LoadScene("GameOver");
	}
}
