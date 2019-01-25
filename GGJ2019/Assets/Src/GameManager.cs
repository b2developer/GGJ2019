using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager gm;
    public static GameManager instance { get {
            if (gm == null) {
                return GameObject.FindObjectOfType<GameManager>();
            } else {
                return gm;
            }
        } }

    public Player player;
    public Material lineRendererMat_Dark;
    public Material lineRendererMat_Light;
    public Planet[] planets;

    private void Awake() {
        gm = this;
        player = GameObject.FindObjectOfType<Player>();
        planets = GameObject.FindObjectsOfType<Planet>();
    }

    private void Start() {
        // Set Line Renderer Dark
        lineRendererMat_Dark = new Material(Shader.Find("Unlit/LineShader"));
        lineRendererMat_Dark.SetColor("_Color", new Color(1, 1, 1, 0.1f));

        // Set Line Renderer Light
        lineRendererMat_Light = new Material(Shader.Find("Unlit/LineShader"));
        lineRendererMat_Light.SetColor("_Color", new Color(1, 1, 1, 0.5f));
    }

    public void UpdateAllPlanets() {
        for (int i = 0; i < planets.Length; i++) {
            planets[i].UpdateLineMaterials();
        }
    }
}
