using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class SmokeController : MonoBehaviour
{
    public float engineRevs;
    public float exhaustRate;
    public float damage;
    public GameObject car;
    public CarController carController;
    public CarCollide carCollide;

    ParticleSystem exhaust;

    void Start() {
        exhaust = GetComponent<ParticleSystem>();
        car = this.transform.parent.parent.gameObject;
        carController = car.GetComponent<CarController>();
        carCollide = car.GetComponent<CarCollide>();
        exhaustRate = 5000;
    }

    void Update() {
        engineRevs = carController.Revs;
        damage = carCollide.damage;
        exhaust.emissionRate = Mathf.Pow(engineRevs, 5) * exhaustRate + 30;
        var col = exhaust.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(new Color(214, 189, 151), 0.079f), new GradientColorKey(Color.white, 1.0f)},
            new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(damage / 255f + 10f / 255f, 0.061f), new GradientAlphaKey(0.0f, 1.0f) });
        col.color = grad;
    }
}
