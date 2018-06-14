using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzadorDeCervezas : MonoBehaviour {
	//a esta variable voy a agregar el game object vasoDeCerveza, que es un prefab y tiene un Rigidbody
	//por lo tanto se lo agrego
	[SerializeField] private Rigidbody _vasoDeCerveza;
	//variable que controla la velocidad con la que se lanza el vaso
	[SerializeField] private float _velocidad;
	//Instancia del audio cuando el vaso se tira y se desliza en la mesa
	[SerializeField] private AudioSource _beerSlidingAudio;
	//instancia de un vaso de cerveza, que va a ser también de tipo Rigidbody, así puede actuar la física
	//y lanzar un vaso de cerveza que va a pasar por la mesa
	private Rigidbody _instanciaVasoDeCerveza;
	private bool _canShoot=false;
	private float _totalTime=0.5f;
	private float _elapsedTime = 0f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		_elapsedTime+= Time.deltaTime;
		if(_elapsedTime>_totalTime){
			_canShoot=true;	
		}
		//detecto la entrada del botón izquierdo del mouse
		if(Input.GetButtonUp("Fire1")&& _canShoot ==true){
			//si disparé un vaso, reproduzco el audio
			_beerSlidingAudio.Play();
			createInstanciaVasoDeCerveza();
			_canShoot=false;
			_elapsedTime=0;
		}
	}

	void createInstanciaVasoDeCerveza(){
		//creo un vaso temporal para pasarle al comensal x
		//Instantiate(objeto al cual le quiero crear una copia, posición del nuevo obj, orientación del nuevo obj)
			_instanciaVasoDeCerveza=Instantiate(_vasoDeCerveza, transform.position, transform.rotation);
		//la nueva cerveza se va a mover en el eje x con velocidad=velocidad	
			_instanciaVasoDeCerveza.velocity=transform.TransformDirection(new Vector3(_velocidad, 0, 0));
		//se ignora la colisión del nuevo vaso creado con el lanzador
		//Physics.IgnoreCollision(el colider del nuevo vaso creado, el colider desde donde se lanza el nuevo vaso
		//o sea el lanzador)
			Physics.IgnoreCollision(_instanciaVasoDeCerveza.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
	}

}
