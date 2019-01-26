using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager gm;
    public static GameManager instance { get {
            if (gm == null) {
                GameManager _gm = GameObject.FindObjectOfType<GameManager>();
                _gm.Init();
                return _gm;
            } else {
                return gm;
            }
        } }

    [HideInInspector] public Player player;
    [HideInInspector] public Material lineRendererMat_Dark;
    [HideInInspector] public Material lineRendererMat_Light;
    public Planet[] planets;

    bool init = false;
    
    private void Init() {
        UnityThread.initUnityThread();

        player = GameObject.FindObjectOfType<Player>();
        planets = GameObject.FindObjectsOfType<Planet>();

        // Set Line Renderer Dark
        lineRendererMat_Dark = new Material(Shader.Find("Unlit/LineShader"));
        lineRendererMat_Dark.SetColor("_Color", new Color(1, 1, 1, 0.2f));

        // Set Line Renderer Light
        lineRendererMat_Light = new Material(Shader.Find("Unlit/LineShader"));
        lineRendererMat_Light.SetColor("_Color", new Color(0, 1, 0, 1));

        init = true;
        gm = this;
    }

    private void Start() {
        if (!init) Init();
    }

    public void UpdateAllPlanetMaterials() {
        for (int i = 0; i < planets.Length; i++) {
            planets[i].UpdateLineMaterials();
        }
    }

    public void UpdateAllPlanetLines() {
        planets = GameObject.FindObjectsOfType<Planet>();

        for (int i = 0; i < planets.Length; i++) {
            planets[i].UpdateLines();
        }
    }
}
