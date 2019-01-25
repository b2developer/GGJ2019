using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet : MonoBehaviour {

    // Public
    public bool Selected;
    public int ZoneSize;
    public List<GameObject> PlanetsInZone;

    // Private
    Material OutlineMat;

    private void Start() {
        // Get all planets in zone
        Collider[] goPlanet = Physics.OverlapSphere(transform.position, ZoneSize);
        for (int i = 0; i < goPlanet.Length; i++) {
            if (goPlanet[i].GetComponent<Planet>() != null) PlanetsInZone.Add( goPlanet[i].gameObject);
        }

        // Get material and set up new instance
        OutlineMat = new Material(GetComponent<MeshRenderer>().sharedMaterial);
        GetComponent<MeshRenderer>().sharedMaterial = OutlineMat;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, ZoneSize);
    }

    private void LateUpdate() {
        if (Selected) {
            OutlineMat.SetFloat("_OutlineAlpha", 1);
        } else {
            OutlineMat.SetFloat("_OutlineAlpha", 0);
        }
    }
}
