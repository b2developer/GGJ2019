﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet : MonoBehaviour {

    // Public
    public bool Selected;
    public int ZoneSize;
    [HideInInspector] public List<GameObject> PlanetsInZone;
    [HideInInspector] public List<LineRenderer> lineRenderers;
    public List<Planet> LineTo;

    // Private
    Material OutlineMat;

    private void Start() {
        // Get all planets in zone
        Collider[] goPlanet = Physics.OverlapSphere(transform.position, ZoneSize);
        for (int i = 0; i < goPlanet.Length; i++) {
            if (goPlanet[i].GetComponent<Planet>() == this) continue;
            if (goPlanet[i].GetComponent<Planet>() != null) PlanetsInZone.Add( goPlanet[i].gameObject);
        }

        // Get material and set up new instance
        OutlineMat = new Material(GetComponent<MeshRenderer>().sharedMaterial);
        GetComponent<MeshRenderer>().sharedMaterial = OutlineMat;

        // Line Renderer Creation
        for (int i = 0; i < PlanetsInZone.Count; i++) {
            Planet p = PlanetsInZone[i].GetComponent<Planet>();
            if (p.LineTo.Contains(this)) {
                continue;
            }

            GameObject go = new GameObject();
            go.transform.parent = transform;
            go.name = "Line Renderer";
            lineRenderers.Add(go.AddComponent<LineRenderer>());

            if (GameManager.instance.player.SelectedPlanet == this) {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Light;
            } else if (GameManager.instance.player.SelectedPlanet == p) {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Light;
            } else {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Dark;
            }

            lineRenderers[lineRenderers.Count - 1].SetPosition(0, transform.position);
            lineRenderers[lineRenderers.Count - 1].SetPosition(1, PlanetsInZone[i].transform.position);
            LineTo.Add(p);
        }
    }

    public void UpdateLineMaterials() {
        for (int i = 0; i < LineTo.Count; i++) {
            Planet p = LineTo[i];

            if (GameManager.instance.player.SelectedPlanet == this) {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Light;
            }
            else if (GameManager.instance.player.SelectedPlanet == p) {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Light;
            }
            else {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Dark;
            }
        }
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