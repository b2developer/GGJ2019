using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSystemGeneration : MonoBehaviour {

    // Public
    [Header("System Info")]
    public int MinSystems = 5;
    public int MaxSystems = 20;
    public float MinSystemDistance = 10;
    public float MaxSystemDistance = 200;
    public float MinSystemSize = 50;
    public float MaxSystemSize = 150;
    public int PlanetsLoaded = 0;

    [Header("Zone Info")]
    public int MinPlanets = 10;
    public int MaxPlanets = 10;
    public float MinPlanetsDistance = 10;
    public float MaxPlanetsDistance = 50;

    // Private
    [System.Serializable]
    public struct Zone {
        public GameObject go;
        public float size;
        public List<GameObject> planets;
    }
    [Header("Zones")]
    public List<Zone> zones = new List<Zone>();

    [Header("Debug Settings")]
    public bool ShowZoneGizmos;
    public bool ShowPlanetGizmos;

    private void Awake() {
        StartCoroutine( GeneratePlanetSystems() );
    }

    IEnumerator GeneratePlanetSystems() {
        int zoneAmount = Random.Range(MinSystems, MaxSystems);
        
        for (int j = 0; j < zoneAmount; j++) {
            // Generate Zone
            Zone z = new Zone();
            z.go = new GameObject("Zone");
            z.size = Random.Range(MinSystemSize, MaxSystemSize);
            if (j != 0) z.go.transform.position = Random.insideUnitSphere.normalized * (zones[j - 1].size + z.size);
            z.planets = new List<GameObject>();

            int planetAmount = Random.Range(MinPlanets, MaxPlanets);

            // Generate Planets
            for (int i = 0; i < planetAmount; i++) {

                // Create Object
                GameObject planet = new GameObject();
                planet.name = "Planet";
                planet.transform.parent = z.go.transform;

                // Ceate colider
                planet.AddComponent<SphereCollider>();

                // Set Planet Position
                Vector3 PlanetPosition;
                if (i == 0) PlanetPosition = z.go.transform.position;
                else PlanetPosition = z.planets[i - 1].transform.position + (Random.insideUnitSphere.normalized * Random.Range(MinPlanetsDistance, MaxPlanetsDistance));
                while (Vector3.Distance(PlanetPosition, z.go.transform.position) > z.size) {
                    if (i == 0) PlanetPosition = z.go.transform.position;
                    else PlanetPosition = z.planets[i - 1].transform.position + (Random.insideUnitSphere.normalized * Random.Range(MinPlanetsDistance, MaxPlanetsDistance));
                }

                
                planet.transform.position = PlanetPosition;

                // Create Planet
                Planet p = planet.AddComponent<Planet>();
                if (i == 0) {
                    p.ZoneSize = Random.Range(MinPlanetsDistance, MaxPlanetsDistance);
                    if (j == 0) GameManager.instance.player.CurrentPlanet = p;
                }
                else {
                    p.ZoneSize = Vector3.Distance(planet.transform.position, z.planets[i - 1].transform.position);
                    z.planets[i - 1].GetComponent<Planet>().ZoneSize = p.ZoneSize;
                }
                
                z.planets.Add(planet);
                PlanetsLoaded++;
                yield return null;
            }
            zones.Add(z);
        }

        GameManager.instance.UpdateAllPlanetLines();
        GameManager.instance.UpdateAllPlanetMaterials();

        GameManager.instance.loading = true;
    }

    private void OnDrawGizmos() {
        if (zones == null) return;
        if (!ShowPlanetGizmos && !ShowZoneGizmos) return;

        for (int i = 0; i < zones.Count; i++) {
            Gizmos.color = Color.red;
            if (ShowZoneGizmos) Gizmos.DrawWireSphere(zones[i].go.transform.position, zones[i].size);
            Gizmos.color = Color.cyan;

            for (int j = 0; j < zones[i].planets.Count; j++) {
                if (ShowPlanetGizmos) Gizmos.DrawWireSphere(zones[i].planets[j].transform.position, zones[i].planets[j].GetComponent<Planet>().ZoneSize);
            }
        }
    }
}
