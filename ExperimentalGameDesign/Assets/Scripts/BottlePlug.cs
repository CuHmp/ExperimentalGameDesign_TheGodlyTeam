using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePlug : MonoBehaviour {

    public NVIDIA.Flex.FlexSourceActor source;
    public Transform rotateX;
    public bool finished_pouring = false;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start() {
        source = gameObject.GetComponentInChildren<NVIDIA.Flex.FlexSourceActor>();
        
        rotateX = transform;
    }

    // Update is called once per frame
    void Update() {
        if (rotateX.eulerAngles.x <= 270 && rotateX.eulerAngles.x >= 40 && !finished_pouring) {
            source.isActive = true;
        }
        else {
            source.isActive = false;
        }
        
        if(source.indexCount >= source.container.maxParticles) {
            finished_pouring = true;
        }
    }

    public void ResetBottle() {
        GameObject tmp = Instantiate(prefab, transform.position, transform.rotation);
        int remaining = finished_pouring ? 1000 : source.container.maxParticles - source.indexCount;
        tmp.GetComponentInChildren<NVIDIA.Flex.FlexSourceActor>().container.maxParticles = source.container.maxParticles + remaining;
        tmp.GetComponent<Rigidbody>().useGravity = true;
        source.container.ResetParticle(gameObject);
        return;
    }

}
