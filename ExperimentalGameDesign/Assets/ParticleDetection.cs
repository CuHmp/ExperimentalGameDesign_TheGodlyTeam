using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDetection : MonoBehaviour {
    public static bool isInPouringZoon;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bottle") {
            isInPouringZoon = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Bottle") {
            isInPouringZoon = false;
        }
    }

}
