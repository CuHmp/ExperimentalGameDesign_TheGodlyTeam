using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePlug : MonoBehaviour {
    public Vector2 MinMaxBottleContent;
    private NVIDIA.Flex.FlexSourceActor source;
    private Transform rotateX;
    public bool finished_pouring = false;
    public GameObject prefab;
    private Vector3 BottleSpawnPosition;

    // Start is called before the first frame update
    void Start() {
        source = gameObject.GetComponentInChildren<NVIDIA.Flex.FlexSourceActor>();
        source.isActive = false;
        Debug.Log(source.container.maxParticles + " liquid stored");
        rotateX = transform;
        BottleSpawnPosition = GameObject.FindGameObjectWithTag("Respawn").transform.position;
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

    public void ResetBottle() { // create a new bottle and remove the liquid 
        int bottleContent = (int)Random.Range(MinMaxBottleContent.x, MinMaxBottleContent.y);
        int remaining = finished_pouring ? bottleContent : source.container.maxParticles - source.indexCount; // counting how much liquid is left in the new bottle
        GameObject tmp;
        if (!finished_pouring) {
            tmp = Instantiate(prefab, transform.position, transform.rotation); // create a new bottle on the same position as the other one
            Debug.Log("foo");
        }
        else {
            Quaternion rot = new Quaternion(0,0,0,0);
            tmp = Instantiate(prefab, BottleSpawnPosition, rot); // create a new bottle
            Debug.Log("bar");
        }
        tmp.GetComponentInChildren<NVIDIA.Flex.FlexSourceActor>().container.maxParticles = remaining; // set the max particles in the new bottle, this is made so that the new bottle do not start with the same amount of liquid all the time
        tmp.GetComponentInChildren<BottlePlug>().finished_pouring = false; // stop it from inheriting the bool as the bottle with max content would still say it was finished_pouring.
        tmp.GetComponent<Rigidbody>().useGravity = true; // so it gets to the ground as the player releasing the bottle when they reset the bottle.
        source.container.ResetParticle(); // removes all of the particles
        Destroy(gameObject); // destroy the current game object so that the new one can take its place
    }

}
