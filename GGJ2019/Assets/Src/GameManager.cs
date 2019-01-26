using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [HideInInspector] public PlanetSystemGeneration planetSystemGeneration;
    [HideInInspector] public Material lineRendererMat_Dark;
    [HideInInspector] public Material lineRendererMat_Light;
    [HideInInspector] public Material lineRendererMat_Blue;


    public Planet[] planets;

    [HideInInspector] public bool loading;
    public Text loadingText;

    bool init = false;
    
    private void Init() {
        UnityThread.initUnityThread();

        player = GameObject.FindObjectOfType<Player>();
        planets = GameObject.FindObjectsOfType<Planet>();
        planetSystemGeneration = GameObject.FindObjectOfType<PlanetSystemGeneration>();


        // Set Line Renderer Dark
        lineRendererMat_Dark = new Material(Shader.Find("Unlit/LineShader"));
        lineRendererMat_Dark.SetColor("_Color", new Color(1, 1, 1, 0.1f));

        // Set Line Renderer Light
        lineRendererMat_Light = new Material(Shader.Find("Unlit/LineShader"));
        lineRendererMat_Light.SetColor("_Color", new Color(0, 1, 0, 0.5f));

        lineRendererMat_Blue = new Material(Shader.Find("Unlit/LineShader"));
        lineRendererMat_Blue.SetColor("_Color", new Color(0, 1, 1, 1));

        loading = true;
        init = true;
        gm = this;
    }

    private void Start() {
        if (!init) Init();
    }

    private void Update() {
        if (loading) {
            loadingText.gameObject.SetActive(false);
        } else {
            loadingText.gameObject.SetActive(true);
        }
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
