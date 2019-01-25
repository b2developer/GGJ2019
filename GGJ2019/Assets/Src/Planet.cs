using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet : MonoBehaviour {
    public int ZoneSize;
    public List<GameObject> PlanetsInZone;

    private void Start() {
        Collider[] goPlanet = Physics.OverlapSphere(transform.position, ZoneSize);
        for (int i = 0; i < goPlanet.Length; i++) {
            if (goPlanet[i].GetComponent<Planet>() != null) PlanetsInZone.Add( goPlanet[i].gameObject);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, ZoneSize);
    }
}
