using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Planet planet;

    private void OnDrawGizmos() {
        if (planet.PlanetsInZone != null) {
            for (int i = 0; i < planet.PlanetsInZone.Count; i++) {
                Gizmos.DrawLine(planet.transform.position, planet.PlanetsInZone[i].transform.position);
            }
        }
    }
}
