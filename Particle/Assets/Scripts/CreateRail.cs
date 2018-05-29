using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRail : MonoBehaviour {

    public GameObject rail;
    // Use this for initialization
    void Start () {
        
        for (int i = 1; i < 500; i += 3) {
            rail = Instantiate(Resources.Load<GameObject>("Prefabs/MetalRail"));
            rail.transform.SetParent(this.gameObject.transform);
            rail.transform.position = new Vector3(i, 0, 0.05f);
            rail = Instantiate(Resources.Load<GameObject>("Prefabs/MetalRail"));
            rail.transform.SetParent(this.gameObject.transform);
            rail.transform.position = new Vector3(i, 0, 499.95f);
            rail = Instantiate(Resources.Load<GameObject>("Prefabs/MetalRail"));
            rail.transform.SetParent(this.gameObject.transform);
            rail.transform.position = new Vector3(0.05f, 0, i);
            rail.transform.rotation = new Quaternion(0, 90, 0, 90);
            rail = Instantiate(Resources.Load<GameObject>("Prefabs/MetalRail"));
            rail.transform.SetParent(this.gameObject.transform);
            rail.transform.position = new Vector3(499.95f, 0, i);
            rail.transform.rotation = new Quaternion(0, 90, 0, 90);
        }
        
    }
	
}
