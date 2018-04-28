using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celula : MonoBehaviour {

	public bool viva = true,vivaEnSiguienteGeneracion = false;
	[SerializeField]
	CelulaController controlCelula;
	public GameObject infoCelulasVivas;


	void awake(){
		//controlCelula = GameObject.Find ("CelulasController").GetComponent<CelulaController>();
	}

	//Contador de vecinas
	public int NumCelVecinasVivas(){

		int numVecinasVivas = 0;
		Collider2D[] vecinasVivas;
		vecinasVivas = Physics2D.OverlapCircleAll (transform.position, 0.9f);

		for (int i = 0; i < vecinasVivas.Length; i++) {
			if(vecinasVivas[i].GetComponentInParent<Celula>().viva == true && vecinasVivas[i] != GetComponent<Collider2D>()){
				numVecinasVivas++;
			}

		}


		infoCelulasVivas.GetComponent<TextMesh> ().text = numVecinasVivas.ToString();

		return numVecinasVivas;
	}

	// Use this for initialization
	void Start () {
		controlCelula = GameObject.Find ("CelulasController").GetComponent<CelulaController>();
	}
	
	// Update is called once per frame
	//Realizar pruebas
	/*void FixedUpdate () {

		NumCelVecinasVivas ();
	}*/



	public void Morir(){

		GetComponent<SpriteRenderer> ().enabled = false;
		viva = false;
	}

	public void Nacer(){

		GetComponent<SpriteRenderer> ().enabled = true;
		viva = true;
	}


	public void VidaSiguienteGeneracion(){
		if (vivaEnSiguienteGeneracion) {
			Nacer ();
		} else {
			Morir ();
		}
	}

	//Presion de mouse activar boolean
	void OnMouseDown(){


		controlCelula.mouseDown = true;

	}

	//Nacer celula si se esta presionando
	void OnMouseOver(){
		if(controlCelula.mouseDown){
			Nacer ();
		}
		//Detecta dentro del collider se presiono
		if (Input.GetMouseButtonDown (1))
			Morir();

	}

	//Desactivar bool
	void OnMouseUp(){
		controlCelula.mouseDown = false;
	}

}

