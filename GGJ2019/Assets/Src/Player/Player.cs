using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [HideInInspector] public float distanceFromPlanet;
    [HideInInspector] public Planet SelectedPlanet;
    [HideInInspector] public Planet CurrentPlanet;
    [HideInInspector] public PlayerCamera playerCamera;

    public PlanetSystemGeneration.Zone CurrentZone;

    [Header("Resources")]
    public int scrap;
    public int energy;
    public int people;

    bool MovingToNewPlanet;

    PlanetSystemGeneration.Zone ZoneToWarp;
    bool Warp;

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

        }

        // Select To Move
        if (SelectedPlanet != null && Input.GetKeyDown(KeyCode.Return)) {
            if (CurrentPlanet.PlanetsInZone.Contains(SelectedPlanet.gameObject)) {
                CurrentPlanet = SelectedPlanet;
                MovingToNewPlanet = true;
            }
        }

        // Warp Check
        if (Input.GetKey(KeyCode.Space)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000);

            // Show warp points
            for (int i = 0; i < GameManager.instance.planetSystemGeneration.zones.Count; i++)
                GameManager.instance.planetSystemGeneration.zones[i].planets[0].transform.GetChild(0).gameObject.SetActive(this);

            RaycastHit hit;
            for (int i = 0; i < GameManager.instance.planetSystemGeneration.zones.Count; i++) {
                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.gameObject.layer == 9) {
                        for (int j = 0; j < hit.transform.childCount; j++) {
                            hit.transform.GetChild(j).GetComponent<Planet>().UpdateLintMatToBlue();
                            ZoneToWarp = hit.transform.gameObject.GetComponent<ZoneSystem>().zone;
                            Warp = true;
                        }
                    }
                } else {
                    Warp = false;
                    for (int j = 0; j < GameManager.instance.planetSystemGeneration.zones[i].planets.Count; j++)
                        GameManager.instance.planetSystemGeneration.zones[i].planets[j].GetComponent<Planet>().UpdateLineMaterials();
                }
            }


        }
        // Warp
        else if (Input.GetKeyUp(KeyCode.Space)) {
            GameManager.instance.UpdateAllPlanetMaterials();
            if (Warp) {
                SphereCollider sc = CurrentZone.go.AddComponent<SphereCollider>();
                sc.radius = 10;

                CurrentZone = ZoneToWarp;
                Destroy(CurrentZone.go.GetComponent<SphereCollider>());

                // Move to
                CurrentPlanet = ZoneToWarp.planets[0].GetComponent<Planet>();
                MovingToNewPlanet = true;
            }
            for (int i = 0; i < GameManager.instance.planetSystemGeneration.zones.Count; i++)
                GameManager.instance.planetSystemGeneration.zones[i].planets[0].transform.GetChild(0).gameObject.SetActive(false);
        }

        // Move to new planet
        if (MovingToNewPlanet) {
            Vector3 direction = CurrentPlanet.transform.position - transform.position;
            transform.position = Vector3.Lerp(transform.position, CurrentPlanet.transform.position, Time.deltaTime);

            transform.LookAt(CurrentPlanet.transform);

            if (Vector3.Distance(transform.position, CurrentPlanet.transform.position) < distanceFromPlanet) {
                MovingToNewPlanet = false;
                playerCamera.pivot = null;
            }

            GameManager.instance.UpdateAllPlanetMaterials();
        }
    }
}
