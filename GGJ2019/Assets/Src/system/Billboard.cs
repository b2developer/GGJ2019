using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	void Update () {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, Time.time * 100));
	}
}
