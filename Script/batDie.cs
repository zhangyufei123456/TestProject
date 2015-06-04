using UnityEngine;
using System.Collections;

public class batDie : MonoBehaviour {

	private float rotateSpeed = 5;
	private float moveSpeed = 0.8f;

	void Start () {
	
	}
	

	void Update () {
		this.transform.Translate(0,-moveSpeed,0);
		this.transform.Rotate(0,rotateSpeed,0);
		this.transform.Rotate(Vector3.up,Space.World);
		this.transform.Rotate(Vector3.right,Space.World);
	}
}
