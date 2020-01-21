using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;
    private Vector3 targetObject;
    public float cameraSpeed;

    private float minSize;
    private float maxSize;
    private float sensitivity;
    private float size;

	// Use this for initialization
	void Start () {

        minSize = 5f;
        maxSize = 22f;
        sensitivity = 5f;
        size = 15f;
        		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        targetObject = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetObject, cameraSpeed * Time.deltaTime);

        Camera.main.orthographicSize = size;

        if (Input.GetAxis("Mouse ScrollWheel") > 0) {

            size -= sensitivity;
            if (size < minSize) {
                size = minSize;

            }

            Camera.main.orthographicSize = size;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {

            size += sensitivity;
            if (size > maxSize) {
                size = maxSize;
            }

            Camera.main.orthographicSize = size;
        }
	}
}
