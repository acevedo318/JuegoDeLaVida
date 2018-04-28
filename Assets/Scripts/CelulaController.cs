using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CelulaController : MonoBehaviour {

	private bool empezar = false;
	[SerializeField]
	GameObject celulaPrefab,ContenedorCelula;
	//guardar cuadricula
	private List<Celula> celulas = new List<Celula> ();
	public float tiempoGeneracion = 1f;

	public bool mouseDown;//Variable global de presion de mouse 
	public Text txtGeneracion;
	private int generacion = 0;

	void Awake(){
		GameObject.Find ("CelulasContent");
	}


	// Use this for initialization
	void Start () {
		generacion = 0;
		CrearCelula ();


	}

	// Update is called once per frame
	void Update () {
		
	}

	public void ActivarPanel(GameObject panel){

		if (panel.activeSelf) {
			panel.SetActive (false);
		} else {
			panel.SetActive (true);
		}
	


	}

	public void PlayPause(){

		empezar = !empezar;
		if (empezar) {
			SiguienteGeneracion ();
		} else {
			CancelInvoke ("SiguienteGeneracion");
		}

	}


	//Funcion para invocar siguiente generacion se invoca cada vez que termine y el tiempo timeGeneracion
	//Tiene en cuenta las reglas del juego
	void SiguienteGeneracion(){

		foreach (var item in celulas) {

			if (item.viva) {

				if (item.NumCelVecinasVivas() == 2 || item.NumCelVecinasVivas() == 3) {

					item.vivaEnSiguienteGeneracion = true;

				} else {
					item.vivaEnSiguienteGeneracion = false;
				}

			} else {

				if (item.NumCelVecinasVivas() == 3) {
					item.vivaEnSiguienteGeneracion = true;
				} else {
					
				}

			}

		}

		foreach (var item in celulas) {
			item.VidaSiguienteGeneracion ();
		}
		generacion++;
		txtGeneracion.text = "\n"+generacion;
		Invoke ("SiguienteGeneracion",tiempoGeneracion);

	}


	//Crea las 10000 celulas
	void CrearCelula(){
		
		for (int i = 0; i < 50; i++) {
				
			for (int j = 0; j < 50; j++) {

				GameObject celulaTemp = Instantiate (celulaPrefab, new Vector2 (j, i), Quaternion.identity);
				celulas.Add (celulaTemp.GetComponent<Celula>());
				celulaTemp.GetComponent<Celula> ().Morir ();
				celulaTemp.transform.parent = ContenedorCelula.transform;
			}

		}

		ContenedorCelula.SetActive (true);

	}

}
