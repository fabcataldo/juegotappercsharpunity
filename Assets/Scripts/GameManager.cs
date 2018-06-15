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
	public void setScore(int sc){
		_score=sc;
	}

	//variable static cuyo contenido es compartido por todas las instancias de esta clase
	public static GameManager ActualGameManager;

	//Awake() es parecido a Start() nada mas que prepara las instancias de los GameObjects, el Start() ya los usa
	void Awake(){
		//creo una sola instancia para _actualGameManager, y hago que sobreviva en todo el juego
		//y que cuando cambie de escena, no se destruya
		if(ActualGameManager==null){
			//Debug.Log("Soy el first!");
			//Arranco guardando el objeto, en la primera jugada, y digo "che, no lo destruyas, ya que para
			//la otra escena, tengo que consultar el score del objeto"
			ActualGameManager=this;
			//Debug.Log("gameObject: "+gameObject);
			DontDestroyOnLoad(gameObject);
		}
		else{
			if(ActualGameManager!=this){
				//Debug.Log("Ya guardé la referencia anterior, sou, destruyo el gameObject que se acaba de crear!");
				Destroy(gameObject);
				//Debug.Log("ActualGameManager: "+ActualGameManager);
				//Debug.Log("gameObject: "+gameObject);
				//Le digo al gameObject que quedó de antes, que arranque el juego
				UpdateBoard();
				StartGame();
			}
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
		_boardText.text = "Score: "+_score.ToString();
		_boardText.text += "\nLifes: "+_lifes.ToString();
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
		//actualizo el score para que la escena GameOver pueda agarrar el score
		//ya que el gameover lee el ActualGameManager
		ActualGameManager.setScore(_score);
		SceneManager.LoadScene("GameOver");
	}
}
