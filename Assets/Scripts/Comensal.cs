using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comensal : MonoBehaviour {
	[SerializeField] private float _speed;
	[SerializeField] private GameManager _gm;
	private Vector3 _originalPosition;
	private Vector3 _nextPosition;
	private bool _startMoving=false;

	public void StartComensalMove(){
		_startMoving=true;	
	}

    void Start () {
		//guardo posición inicial del comensal
		_originalPosition = transform.position;
	}
	//un comensale se mueve cada tanto a lo largo de la mesa
    void Update () {
		if(_startMoving){
			_nextPosition = Vector3.left*_speed*Time.deltaTime;
			transform.Translate(_nextPosition);	
		}
    }

	//cuando colisiona con una cerveza, se debe destruir la cerveza, y reiniciar la posición inicial del comensal
	void OnCollisionEnter(Collision col){
        //Si el comensal choca con el prefab de vaso de cerveza
		if(col.gameObject.tag == "VasoDeCervezaPrefab"){
            //que se destruya ese prefab
			Destroy(col.gameObject);
			//Cada vez que un comensal toma un vaso, son 10 pts obtenidos
			_gm.UpdateScore(); //el gamemanager debería ver esta variable y decir "aumentar score del jugador"
		}		
		else{
		//si choco con un gameobject vacío que puse al final de las mesas, descuento una vida y reinicio posición
			if(col.gameObject.tag == "ComensalColliderInvisible"){
				_gm.PlayerDiscountLife();
			}
		}
		ResetPosition();
	}

	void ResetPosition(){
		transform.position = _originalPosition;
	}
}