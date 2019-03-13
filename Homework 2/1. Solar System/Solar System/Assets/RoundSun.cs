using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSun : MonoBehaviour {

	public Transform Sun;
	public Transform Mercury;
	public Transform Venus;
	public Transform Earth;
	public Transform Mars;
	public Transform Jupiter;
	public Transform Saturn;
	public Transform Uranus;
	public Transform Neptune;
	public Transform EarthShadow;

	// Use this for initialization
	void Start () {
		Sun.position = Vector3.zero;
		Mercury.position = new Vector3 (7, 0, 0);
		Venus.position = new Vector3 (9, 0, 0);
		Earth.position = new Vector3 (12, 0, 0);
		Mars.position = new Vector3 (15, 0, 0);
		Jupiter.position = new Vector3 (20, 0, 0);
		Saturn.position = new Vector3 (28, 0, 0);
		Uranus.position = new Vector3 (35, 0, 0);
		Neptune.position = new Vector3 (40, 0, 0);
		EarthShadow.position = new Vector3 (12, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		Mercury.RotateAround (Sun.position, new Vector3(0, 10, 1), 47 * Time.deltaTime);
		Mercury.Rotate (Vector3.down * 6 * Time.deltaTime);
		Venus.RotateAround (Sun.position, new Vector3(0, 15, -1), 35 * Time.deltaTime);
		Venus.Rotate (Vector3.down * 1 * Time.deltaTime);
		Earth.RotateAround (Sun.position, Vector3.up, 30 * Time.deltaTime);
		Earth.Rotate (Vector3.down * 300 * Time.deltaTime);
		Mars.RotateAround (Sun.position, new Vector3(0, 18, 2), 24 * Time.deltaTime);
		Mars.Rotate (Vector3.up * 300 * Time.deltaTime);
		Jupiter.RotateAround (Sun.position, new Vector3(0, 16, 1), 13 * Time.deltaTime);
		Jupiter.Rotate (Vector3.up * 600 * Time.deltaTime);
		Saturn.RotateAround (Sun.position, new Vector3(0, 20, -1), 9 * Time.deltaTime);
		Saturn.Rotate (Vector3.up * 400 * Time.deltaTime);
		Uranus.RotateAround (Sun.position, new Vector3(0, 25, -1), 6 * Time.deltaTime);
		Uranus.Rotate (Vector3.up * 500 * Time.deltaTime);
		Neptune.RotateAround (Sun.position, new Vector3(0, 30, 1), 5 * Time.deltaTime);
		Neptune.Rotate (Vector3.up * 500 * Time.deltaTime);
		EarthShadow.RotateAround (Sun.position, Vector3.up, 30 * Time.deltaTime);
		EarthShadow.Rotate (Vector3.down * 100 * Time.deltaTime);
	}
}
