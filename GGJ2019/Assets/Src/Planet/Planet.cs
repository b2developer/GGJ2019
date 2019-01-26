using System.Collections.Generic;
using UnityEngine;
using System.Threading;

[System.Serializable]
public class Planet : MonoBehaviour {

    // Public
    public bool Selected;
    public float ZoneSize;
    [HideInInspector] public List<GameObject> PlanetsInZone = new List<GameObject>();
    [HideInInspector] public List<LineRenderer> lineRenderers = new List<LineRenderer>();
    public List<Planet> LineTo = new List<Planet>();

    // Private
    Material OutlineMat;

    private void Start() {
        UpdateLines();

        // Create Gen Planet
        GenPlanet gen = gameObject.AddComponent<GenPlanet>();
        gen.m_Material = new Material(Shader.Find("Custom/VertexColoredShader"));
        gen.GeneratePlanet();

        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).GetComponent<LineRenderer>() == null) {
                transform.GetChild(i).localPosition = Vector3.zero;
            }
        }
    }
    

    public void UpdateLines() {

        // Get all planets in zone
        Collider[] goPlanet = Physics.OverlapSphere(transform.position, ZoneSize);
        for (int i = 0; i < goPlanet.Length; i++) {
            if (goPlanet[i].GetComponent<Planet>() == this) continue;
            if (goPlanet[i].GetComponent<Planet>() != null) PlanetsInZone.Add(goPlanet[i].gameObject);
        }

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

            if (GameManager.instance.player.CurrentPlanet == this) {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Light;
            }
            else if (GameManager.instance.player.CurrentPlanet == p && GameManager.instance.player.CurrentPlanet.PlanetsInZone.Contains(gameObject)) {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Light;
            }
            else {
                lineRenderers[lineRenderers.Count - 1].sharedMaterial = GameManager.instance.lineRendererMat_Dark;
            }

            lineRenderers[lineRenderers.Count - 1].SetPosition(0, transform.position);
            lineRenderers[lineRenderers.Count - 1].SetPosition(1, PlanetsInZone[i].transform.position);
            lineRenderers[lineRenderers.Count - 1].startWidth = 0.1f;
            lineRenderers[lineRenderers.Count - 1].endWidth = 0.1f;
            LineTo.Add(p);
        }
    }

    public void UpdateLineMaterials() {
        for (int i = 0; i < LineTo.Count; i++) {
            Planet p = LineTo[i];

            if (GameManager.instance.player.CurrentPlanet == this) {
                lineRenderers[i].sharedMaterial = GameManager.instance.lineRendererMat_Light;
            }
            else if (GameManager.instance.player.CurrentPlanet == p && GameManager.instance.player.CurrentPlanet.PlanetsInZone.Contains(gameObject)) {
                lineRenderers[i].sharedMaterial = GameManager.instance.lineRendererMat_Light;
            }
            else {
                lineRenderers[i].sharedMaterial = GameManager.instance.lineRendererMat_Dark;
            }
        }
    }

    // private void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.cyan;
    //     Gizmos.DrawWireSphere(transform.position, ZoneSize);
    // }

    private void LateUpdate() {
        if (Selected) {
            //OutlineMat.SetFloat("_OutlineAlpha", 1);
        } else {
            //OutlineMat.SetFloat("_OutlineAlpha", 0);
        }
    }
}
