using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    [HideInInspector] public Player player;

    public GameObject pivot;

    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    float x = 0.0f;
    float y = 0.0f;

    public bool isDragging;

    void LateUpdate() {
        if (player == null) return;
        if (player.CurrentPlanet == null) {
            pivot = player.gameObject;
        } else {
            if (pivot == null) pivot = player.CurrentPlanet.gameObject;
            else if (pivot != player.CurrentPlanet.gameObject) {
                pivot = player.gameObject;
            }
        }

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        if (Input.GetAxis("Mouse X") > 0.001f || Input.GetAxis("Mouse X") < -0.001f || Input.GetAxis("Mouse Y") > 0.001f || Input.GetAxis("Mouse Y") < -0.001f) {
            isDragging = true;
        } else {
            isDragging = false;
        }

        if (Input.GetMouseButton(0)) {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + pivot.transform.position;

        transform.rotation = rotation;
        transform.position = position;
    }

}
