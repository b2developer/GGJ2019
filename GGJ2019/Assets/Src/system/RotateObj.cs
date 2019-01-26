using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour {

    public Vector3 rot;

	void Update () {
        transform.Rotate(rot);
	}
}
