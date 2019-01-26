using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [Header("Popup")]
    public GameObject Popup;
    public TextMeshProUGUI text_title;
    public TextMeshProUGUI text_message;
    public TextMeshProUGUI text_option1;
    public TextMeshProUGUI text_option2;
    public TextMeshProUGUI text_option3;
    bool showPopup;
    bool GoingToNextPlace;
    DataBase db;
    int db_currentID;

    GameObject highlightPlanet;

    PlanetSystemGeneration.Zone ZoneToWarp;
    bool MovingToNewPlanet;
    bool Warp;

    private void Start() {
        db = new DataBase();
        db.Init();
        playerCamera = Camera.main.GetComponent<PlayerCamera>();
        playerCamera.player = this;

        // Create highlightPlanet
        highlightPlanet = GameObject.CreatePrimitive(PrimitiveType.Quad);
        highlightPlanet.transform.localScale = new Vector3(5, 5, 5);
        highlightPlanet.name = "WarpRedical";
        highlightPlanet.AddComponent<Billboard>();
        MeshRenderer wpmr = highlightPlanet.GetComponent<MeshRenderer>();
        wpmr.sharedMaterial = new Material(Shader.Find("Unlit/CutoutTransparentColor"));
        wpmr.sharedMaterial.SetTexture("_MainTex", GameManager.instance.planetSystemGeneration.WarpRedical);
        wpmr.sharedMaterial.SetColor("_Color", new Color(0, 1, 0, 1));
        highlightPlanet.SetActive(false);

    }

    private void OnDrawGizmos() {
        if (SelectedPlanet != null && SelectedPlanet.PlanetsInZone != null) {
            for (int i = 0; i < SelectedPlanet.PlanetsInZone.Count; i++) {
                Gizmos.DrawLine(SelectedPlanet.transform.position, SelectedPlanet.PlanetsInZone[i].transform.position);
            }
        }
    }

    private void Update() {
        if (CurrentPlanet == null) {
            Debug.LogWarning("No Current Planet");
            return;
        }

        // Highlight Planet
        if (Input.GetMouseButtonDown(0) && !playerCamera.isDragging) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (SelectedPlanet != null) SelectedPlanet.Selected = false;
                SelectedPlanet = hit.transform.gameObject.GetComponent<Planet>();
                if (SelectedPlanet != null && CurrentPlanet.PlanetsInZone.Contains(SelectedPlanet.gameObject)) {
                    SelectedPlanet.Selected = true;
                    highlightPlanet.SetActive(true);
                    highlightPlanet.transform.position = SelectedPlanet.transform.position;
                } else if (SelectedPlanet != null) {
                    highlightPlanet.SetActive(false);
                    SelectedPlanet.Selected = false;
                    SelectedPlanet = null;
                }
            } else if (SelectedPlanet != null) {
                highlightPlanet.SetActive(false);
                SelectedPlanet.Selected = false;
                SelectedPlanet = null;
            }

        }

        // Select To Move
        if (SelectedPlanet != null && Input.GetKeyDown(KeyCode.Return) && !showPopup) {
            if (CurrentPlanet.PlanetsInZone.Contains(SelectedPlanet.gameObject)) {
                int energyAmount = (int)Vector3.Distance(transform.position, SelectedPlanet.transform.position);
                if (energy >= energyAmount) {
                    CurrentPlanet = SelectedPlanet;
                    MovingToNewPlanet = true;
                    energy -= energyAmount;
                }
            }
        }

        // Warp Check
        if (Input.GetKey(KeyCode.Space) && !showPopup) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000);

            // Show warp points
            for (int i = 0; i < GameManager.instance.planetSystemGeneration.zones.Count; i++)
                if (GameManager.instance.planetSystemGeneration.zones[i].go != CurrentZone.go)
                    GameManager.instance.planetSystemGeneration.zones[i].planets[0].transform.GetChild(0).gameObject.SetActive(this);

            RaycastHit hit;
            for (int i = 0; i < GameManager.instance.planetSystemGeneration.zones.Count; i++) {
                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.gameObject.layer == 9) {
                        for (int j = 0; j < hit.transform.childCount; j++) {
                            hit.transform.GetChild(j).GetComponent<Planet>().UpdateLintMatToBlue();
                            ZoneToWarp = hit.transform.gameObject.GetComponent<ZoneSystem>().zone;

                            int energyAmount = (int)Vector3.Distance(transform.position, ZoneToWarp.go.transform.position);
                            if (energy >= energyAmount) {
                                Warp = true;
                            } else {
                                // TODO: display gui error
                            }
                        }
                    }
                } else {
                    Warp = false;
                    for (int j = 0; j < GameManager.instance.planetSystemGeneration.zones[i].planets.Count; j++)
                        GameManager.instance.planetSystemGeneration.zones[i].planets[j].GetComponent<Planet>().UpdateLineMaterials();
                }
            }

            if (Input.GetMouseButtonDown(0) && !playerCamera.isDragging) {
                WarpShip();
            }
        }
        // Warp
        else if (Input.GetKeyUp(KeyCode.Space) && !showPopup) {
            WarpShip();
        }

        // Move to new planet
        if (MovingToNewPlanet) {
            GoingToNextPlace = true;

            Vector3 direction = CurrentPlanet.transform.position - transform.position;
            transform.position = Vector3.Lerp(transform.position, CurrentPlanet.transform.position, Time.deltaTime);

            transform.LookAt(CurrentPlanet.transform);

            if (Vector3.Distance(transform.position, CurrentPlanet.transform.position) < distanceFromPlanet) {
                MovingToNewPlanet = false;
                playerCamera.pivot = null;
            }

            GameManager.instance.UpdateAllPlanetMaterials();
        } else {
            orbit();
            if (GoingToNextPlace == true) {
                GoingToNextPlace = false;

                db_currentID = Random.Range(0, db.DATA.Count);

                // Set popup text
                text_title.text = db.DATA[db_currentID].title;
                text_message.text = db.DATA[db_currentID].message;

                text_option1.text = db.DATA[db_currentID].Option1;
                text_option2.text = db.DATA[db_currentID].Option2;
                text_option3.text = db.DATA[db_currentID].Option3;

                showPopup = true;
            }
   
        }

        if (showPopup) {
            Popup.SetActive(true);
        }
        else {
            Popup.SetActive(false);
        }
    }

    public void Option1() {
        energy += db.DATA[db_currentID].Option1_Energy;
        scrap  += db.DATA[db_currentID].Option1_Scrap;
        people += db.DATA[db_currentID].Option1_Peaple;
        showPopup = false;
    }

    public void Option2() {
        energy += db.DATA[db_currentID].Option2_Energy;
        scrap  += db.DATA[db_currentID].Option2_Scrap;
        people += db.DATA[db_currentID].Option2_Peaple;
        showPopup = false;
    }

    public void Option3() {
        energy += db.DATA[db_currentID].Option3_Energy;
        scrap  += db.DATA[db_currentID].Option3_Scrap;
        people += db.DATA[db_currentID].Option3_Peaple;
        showPopup = false;
    }

    float x;
    void orbit() {
        x += 50 * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0, x, 0);
        
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -3);
        Vector3 position = rotation * negDistance + CurrentPlanet.transform.position;

        transform.rotation = rotation;
        transform.position = position;
        transform.Rotate(new Vector3(0, -90, 0));
    }

    void WarpShip() {
        GameManager.instance.UpdateAllPlanetMaterials();
        if (Warp) {
            SphereCollider sc = CurrentZone.go.AddComponent<SphereCollider>();
            sc.radius = 10;

            int energyAmount = (int)Vector3.Distance(transform.position, ZoneToWarp.go.transform.position);
            Debug.Log(energyAmount);
            energy -= energyAmount;

            CurrentZone = ZoneToWarp;
            Destroy(CurrentZone.go.GetComponent<SphereCollider>());

            // Move to
            CurrentPlanet = ZoneToWarp.planets[0].GetComponent<Planet>();
            MovingToNewPlanet = true;
        }
        for (int i = 0; i < GameManager.instance.planetSystemGeneration.zones.Count; i++)
            GameManager.instance.planetSystemGeneration.zones[i].planets[0].transform.GetChild(0).gameObject.SetActive(false);

    }
}
