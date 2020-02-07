using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDetection : MonoBehaviour {
    int liquidInGlass = 0;
    private static bool resetPlayer1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()  {
        if (resetPlayer1) {
            liquidInGlass = 0;
            resetPlayer1 = false;
        }

    }
    public static void resetLiquidPlayer1() {
        resetPlayer1 = true;
    }
    private void OnTrigger(Collider other) {
        if (other.gameObject.tag != "Glass") {
            liquidInGlass++;
            Debug.Log(liquidInGlass);

        }
    }
}
