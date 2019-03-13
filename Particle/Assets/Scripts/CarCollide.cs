using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollide : MonoBehaviour {

    public float damage = 1;
    private void OnCollisionEnter(Collision collision) {
        damage += this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
    }
}
