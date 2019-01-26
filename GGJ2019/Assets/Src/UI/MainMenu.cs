using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject resources;

	public void Exit() {
        Application.Quit();
    }

    public void StartGame() {
        GameManager.instance.loading = false;
        GameManager.instance.planetSystemGeneration.StartGeneratePlanetSystems();
        resources.SetActive(true);
        
        this.gameObject.SetActive(false);
    }

}
