using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Planet planet;
    public PlayerCamera playerCamera;

    private void Start() {
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
    }

    private void OnDrawGizmos() {
        if (planet != null && planet.PlanetsInZone != null) {
            for (int i = 0; i < planet.PlanetsInZone.Count; i++) {
                Gizmos.DrawLine(planet.transform.position, planet.PlanetsInZone[i].transform.position);
            }
        }
    }

    private void Update() {
        // Highlight Planet
        if (Input.GetMouseButtonDown(0) && !playerCamera.isDragging) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (planet != null) planet.Selected = false;
                planet = hit.transform.gameObject.GetComponent<Planet>();
                if (planet != null) planet.Selected = true;
            } else if (planet != null) {
                planet.Selected = false;
                planet = null;
            }
        }

        // Select To Move
        
    }
}
