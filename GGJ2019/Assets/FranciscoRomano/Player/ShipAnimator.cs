using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipAnimator : MonoBehaviour
{
    private Animator[] animators;
    
	// Use this for initialization
	void Awake() { animators = GetComponentsInChildren<Animator>(); Debug.Log(animators.Length); }
	
	public void EnableModule(int index) { animators[index].SetBool("isVisible", true); }

    public void DisableModule(int index) { animators[index].SetBool("isVisible", false); }
}
