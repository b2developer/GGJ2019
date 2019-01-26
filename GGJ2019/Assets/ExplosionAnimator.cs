using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimator : MonoBehaviour
{
    public float amount = 1.0f;
    public ParticleSystem sparkParticleSystem;

    public float minShapeRadius;
    public float minStartLifetime;
    public float minEmissionRateOverTime;
	
	void Awake ()
    {
        minShapeRadius = sparkParticleSystem.shape.radius;
        minStartLifetime = sparkParticleSystem.startLifetime;
        minEmissionRateOverTime = sparkParticleSystem.emission.rateOverTime.constant;

        var shape = sparkParticleSystem.shape;
        shape.radius = 0.25f;
        transform.localScale = Vector3.one;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //sparkParticleSystem.startLifetime = amount * minStartLifetime * 0.5f;

        //var emission = sparkParticleSystem.emission;
        //emission.rateOverTime = amount * minEmissionRateOverTime;

        //var shape = sparkParticleSystem.shape;
        //shape.radius = amount * minShapeRadius;

        transform.localScale = Vector3.one * amount;

        amount += Time.deltaTime;
    }
}
