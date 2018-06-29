using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private float _lerpTime = 1f;
	[SerializeField] private float _step = 0.5f;
	//_wayPoints: Arreglo de transforms que hace que el game object "Player" se pueda mover por los puntos
	//contenidos en este arreglo
	[SerializeField] private Transform [] _wayPoints;

	private float _elapsedTime = 0f;
	private Vector3 _nextPosition;
	private bool _canMove = false;
	private bool _isMoving = false;
	
	//index que sirve para calcular la siguiente mesa a moverse en el eje z
	private int _currentPositionIdx = 0;

	public void setCanMove(){
		_canMove = true;
	}

	// Update is called once per frame
	void Update () {
		if(_canMove) {
			Directions direction = Directions.kNone;
				//Checking User Input Behaviour
				if(Input.GetAxis("Horizontal") > 0) {
					direction = Directions.kRight;
				} else if (Input.GetAxis("Horizontal") < 0) {
					direction = Directions.kLeft;
				}
				CalculateStep(direction);
		}
		//After Step is calculated then we move to next position.
		if(_isMoving) {
			Move();
		}
	}

	void CalculateStep(Directions direction) {
		//si me moví, evalúo si apreté tecla de arriba o de abajo
		//Debug.Log("currenPos: "+_currentPositionIdx);
		if(direction!=Directions.kNone){
			_nextPosition = transform.position;
			//Calculamos el proximo vector de posicion segun la direccion tomada.
			//si apreté arriba y el index no es más del largo del _waypoints-1, me muevo a la proxima mesa
			//if(direction==Directions.kDown && _currentPositionIdx < _wayPoints.Length-1)
			//LO CAMBIÉ PARA ADAPTARLO A VR
			if(direction==Directions.kRight && _currentPositionIdx < _wayPoints.Length-1)
				_currentPositionIdx++;
			else
				//si no, si apreté abajo y el index es > a 0, me puedo ir a una mesa anterior
				//if(direction==Directions.kUp && _currentPositionIdx > 0)
				if(direction==Directions.kLeft && _currentPositionIdx > 0)
					_currentPositionIdx--;
			//me muevo en el eje z
			_nextPosition.z = _wayPoints[_currentPositionIdx].position.z;
			_isMoving = (direction != Directions.kNone);
		}
	}

	void Move(){
		//Lerp Movement needs a percentage of movement from point A to point B.
		_elapsedTime += Time.deltaTime;
		if(_elapsedTime > _lerpTime) {
			_elapsedTime = _lerpTime;
		}
		
		float percentage = (_elapsedTime / _lerpTime);
		transform.position = Vector3.Lerp(transform.position, _nextPosition, percentage);
		
		_canMove = false;
		
		if(percentage == 1) {
			_canMove = true;
			_isMoving = false;
			_elapsedTime = 0f;
		}
	}
}
