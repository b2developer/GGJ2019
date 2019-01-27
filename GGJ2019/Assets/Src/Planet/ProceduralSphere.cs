using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSphere : MonoBehaviour
{
    public class Wave
    {
        public float amplitude = 1.0f;
        public float frequency = 1.0f;
        public float offset = 0.0f;

        public Wave(float a, float f, float o)
        {
            amplitude = a; frequency = f; offset = o;
        }

        public float Compute(float time)
        {
            return amplitude * Mathf.Sin(time * frequency + offset * frequency);
        }
    }

    public class NoiseMachine
    {
        public List<Wave> waves;

        public float bias = 1.0f;
        public NoiseMachine(int waveCount, float bias_)
        {
            waves = new List<Wave>();

            bias = bias_;
                
            float ampSum = 0.0f;

            for (int i = 0; i < waveCount; i++)
            {
                Wave w = new Wave((Random.value - 0.5f) * 2.0f, (Random.value - 0.5f) * 0.5f, (Random.value - 0.5f) * 2.0f);

                ampSum += Mathf.Abs(w.amplitude);

                waves.Add(w);
            }

            //normalise the wave amplitudes down to 1 at the maximum value of each wave
            foreach (Wave w in waves)
            {
                w.amplitude /= ampSum;
            }
        }

        public float Compute(float time)
        {
            float sum = 0.0f;

            foreach (Wave w in waves)
            {
                sum += w.Compute(time);
            }

            return sum;
        }
    }

    public class DimensionalNoise
    {
        public const float THIRD = 1 / 3.0f;

        public List<NoiseMachine> machines;

        public DimensionalNoise(int dimensions, float[] biases)
        {
            machines = new List<NoiseMachine>();

            for (int i = 0; i < dimensions; i++)
            {
                machines.Add(new NoiseMachine((int)(Random.value * 3.0f + 3), biases[i]));
            }
        }

        public float Compute(float[] variables)
        {
            float sum = 0.0f;

            int dim = machines.Count;

            float biasSum = 0.0f;

            for (int i = 0; i < dim; i++)
            {
                float value = machines[i].Compute(variables[i]) * machines[i].bias;

                sum += value;
                biasSum += machines[i].bias;
            }


            sum /= biasSum;

            return sum;
        }
    }

    public MeshFilter meshFilter = null;

    public GameObject spherePrefab = null;

    public Material atmosphereMaterial = null;

    public Color underground = Color.red;
    public Color ground = Color.red;
    public Color overground = Color.green;

    public Vector3 spin;

    [Range(1.0f, 10.0f)]
    public float colourBias = 5.0f;

    [Range(0.0f, 1.0f)]
    public float colourBlend = 0.5f;

    [Range(0.0f, 0.1f)]
    public float colourNoise = 0.0f;

    float[,,] matrix = new float[20, 20, 20];

    // Use this for initialization
    public void Generate()
    {

        Transform[] children = GetComponentsInChildren<Transform>();

        for (int i =0; i < children.GetLength(0); i++)
        {
            if (children[i] != transform)
            {
                Destroy(children[i].gameObject);
            }
        }

        float[] randomValues = new float[3] { Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f) };

        float sum = randomValues[0] + randomValues[1] + randomValues[2];

        randomValues[0] /= sum;
        randomValues[1] /= sum;
        randomValues[2] /= sum;

        DimensionalNoise dn = new DimensionalNoise(3, randomValues);

        Vector3 half = new Vector3(matrix.GetLength(0), matrix.GetLength(1), matrix.GetLength(2)) / 2.0f;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int k = 0; k < matrix.GetLength(2); k++)
                {
                    matrix[i, j, k] = dn.Compute(new float[3] { i, j, k });
                }
            }
        }

        colourBias = Random.value * 5.0f + 5.0f;

        underground = Random.ColorHSV(0.0f, 1.0f, 0.6f, 0.9f);
        ground = Random.ColorHSV(0.0f, 1.0f, 0.6f, 0.9f);
        overground = Random.ColorHSV(0.0f, 1.0f, 0.6f, 0.9f);

        //blend colours
        Color average = underground + ground + overground / 3.0f;

        underground = Color.Lerp(underground, average, colourBlend);
        ground = Color.Lerp(ground, average, colourBlend);
        overground = Color.Lerp(overground, average, colourBlend);

        transform.localScale = Vector3.one * (Random.value * 2.0f + 1.0f);

        AlterVertices(matrix, matrix.GetLength(0), matrix.GetLength(1), matrix.GetLength(2), Random.value * 0.1f);

        //generate atmosphere
        GameObject atmosphere = Instantiate<GameObject>(spherePrefab);
        
        atmosphere.transform.SetParent(transform);
        
        atmosphere.transform.localPosition = Vector3.zero;
        atmosphere.transform.localScale = Vector3.one + Vector3.one * Random.value * 0.1f;
        
        atmosphere.GetComponent<MeshRenderer>().material = atmosphereMaterial;
        
        Color atc = Random.ColorHSV();
        atc.a = Random.value * 0.7f;
        
        atmosphere.GetComponent<MeshRenderer>().material.color = atc;
        
        spin = new Vector3(Random.value, Random.value, Random.value) - Vector3.one * 0.5f;
        spin *= 20.0f;
    }   

    public void AlterVertices(float[,,] matrix, int il, int jl, int kl, float ratio)
    {
        Vector3[] verts = meshFilter.mesh.vertices;

        meshFilter.mesh.colors = new Color[verts.GetLength(0)];

        Color[] colours = meshFilter.sharedMesh.colors;

        for (int i = 0; i < verts.GetLength(0); i++)
        {
            Vector3 iterator = verts[i].normalized;

            iterator += Vector3.one;
            iterator *= 0.5f;
            iterator = new Vector3(iterator.x * il, iterator.y * jl, iterator.z * kl);

            iterator -= Vector3.one * 0.00001f;

            iterator.x = iterator.x < 0.0f ? 0.0f : iterator.x;
            iterator.y = iterator.y < 0.0f ? 0.0f : iterator.y;
            iterator.z = iterator.z < 0.0f ? 0.0f : iterator.z;

            float val = matrix[Mathf.FloorToInt(iterator.x), Mathf.FloorToInt(iterator.y), Mathf.FloorToInt(iterator.z)];
            val *= 0.5f;

            float absVal = val + 1.0f;
            absVal *= 0.5f;

            verts[i].Normalize();

            Vector3 n = verts[i];

            verts[i] *= 1 + val * ratio;

            float u = Mathf.Atan2(n.x, n.z) / (2 * Mathf.PI) + 0.5f;
            float v = n.y * 0.5f + 0.5f;

            float scaledVal = absVal - 0.5f;
            scaledVal *= colourBias;

            scaledVal = Mathf.Clamp(scaledVal, -0.5f, 0.5f);
            scaledVal += 0.5f;

            Color sc = Color.Lerp(underground, ground, scaledVal * 2.0f);

            if (scaledVal > 0.5f)
            {
                sc = Color.Lerp(ground, overground, scaledVal * 2.0f - 1.0f);
            }

            float noise = (Random.value - 0.5f) * colourNoise;

            sc.r += noise;
            sc.g += noise;
            sc.b += noise;

            colours[i] = sc;
        }


        meshFilter.mesh.vertices = verts;
        meshFilter.mesh.colors = colours;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.eulerAngles += spin * Time.deltaTime;
    }
}
