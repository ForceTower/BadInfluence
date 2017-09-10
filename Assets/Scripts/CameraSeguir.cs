using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSeguir : MonoBehaviour {
    public Transform target;
    public Vector3 offset;

    void Start() {
		
    }
	    
    void LateUpdate() {
        Vector3 position = target.position - offset;
        transform.position = position;
    }
}
