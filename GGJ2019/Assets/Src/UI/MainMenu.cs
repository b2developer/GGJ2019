using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    //public GameObject menu;
    //public GameManager GM;

	public void Exit() {
        Application.Quit();
    }

    public void StartGame() {
        GameManager.instance.loading = false;
        GameManager.instance.planetSystemGeneration.StartGeneratePlanetSystems();
        this.gameObject.SetActive(false);
    }

}
