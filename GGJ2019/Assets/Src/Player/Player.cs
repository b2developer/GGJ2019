using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Planet CurrentPlanet;
    public float distanceFromPlanet;

    [HideInInspector] public Planet SelectedPlanet;
    [HideInInspector] public PlayerCamera playerCamera;

    bool MovingToNewPlanet;

    private void Start() {
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
        playerCamera.player = this;
    }

    private void OnDrawGizmos() {
        if (SelectedPlanet != null && SelectedPlanet.PlanetsInZone != null) {
            for (int i = 0; i < SelectedPlanet.PlanetsInZone.Count; i++) {
                Gizmos.DrawLine(SelectedPlanet.transform.position, SelectedPlanet.PlanetsInZone[i].transform.position);
            }
        }
    }

    private void Update() {
        // Highlight Planet
        if (Input.GetMouseButtonDown(0) && !playerCamera.isDragging) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (SelectedPlanet != null) SelectedPlanet.Selected = false;
                SelectedPlanet = hit.transform.gameObject.GetComponent<Planet>();
                if (SelectedPlanet != null) SelectedPlanet.Selected = true;
            } else if (SelectedPlanet != null) {
                SelectedPlanet.Selected = false;
                SelectedPlanet = null;
            }

            GameManager.instance.UpdateAllPlanets();
        }

        // Select To Move
        if (SelectedPlanet != null && Input.GetKeyDown(KeyCode.Return)) {
            CurrentPlanet = SelectedPlanet;
            MovingToNewPlanet = true;
        }

        // Move to new planet
        if (MovingToNewPlanet) {
            Vector3 direction = CurrentPlanet.transform.position - transform.position;
            transform.position = Vector3.Lerp(transform.position, CurrentPlanet.transform.position, Time.deltaTime);

            if (Vector3.Distance(transform.position, CurrentPlanet.transform.position) < distanceFromPlanet) {
                MovingToNewPlanet = false;
                playerCamera.pivot = null;
            }
        }
    }
}
