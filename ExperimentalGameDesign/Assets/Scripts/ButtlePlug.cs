using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtlePlug : MonoBehaviour {

    public NVIDIA.Flex.FlexSourceActor source;
    public Transform rotateX;
    public bool finished_pouring;

    // Start is called before the first frame update
    void Start() {
        source = gameObject.GetComponentInChildren<NVIDIA.Flex.FlexSourceActor>();
        rotateX = transform;
    }

    // Update is called once per frame
    void Update() {
        if (rotateX.eulerAngles.x <= 270 && rotateX.eulerAngles.x >= 40) {
            source.isActive = true;
        }
        else {
            source.isActive = false;
        }
        
        //int[] test = new int[source.container.particleBuffer.count];
        //source.container.FreeParticles(test);
        //if (false) {
        //    finished_pouring = true;
        //}
    }
}
