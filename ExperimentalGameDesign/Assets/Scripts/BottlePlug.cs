using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePlug : MonoBehaviour {
    public Vector2 MinMaxBottleContent;
    public GameObject prefab;
    
    

    public int liquid_in_glass = 0;
    public int bartender_rage = 0;

    private Transform rotateX;
    private NVIDIA.Flex.FlexSourceActor source;
    private Vector3 BottleSpawnPosition;
    private bool isFinishedPouring = false;
    private bool isPouring = false;
    // Start is called before the first frame update
    void Start() {
        source = gameObject.GetComponentInChildren<NVIDIA.Flex.FlexSourceActor>();
        isPouring = false;
        source.isActive = false;
        Debug.Log(source.container.maxParticles + " liquid stored");
        rotateX = transform;
        BottleSpawnPosition = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        liquid_in_glass = 0;
        bartender_rage = 0;
    }

    // Update is called once per frame
    void Update() {
        if (rotateX.eulerAngles.x <= 270 && rotateX.eulerAngles.x >= 40 && !isFinishedPouring) {
            source.isActive = true;
            isPouring = true;
        }
        else {
            source.isActive = false;
            isPouring = false;
        }
        
        if(source.indexCount >= source.container.maxParticles) {
            isFinishedPouring = true;
        }
        if(ParticleDetection.isInPouringZoon && isPouring && !isFinishedPouring) {
            liquid_in_glass++;
        }
        else if(!ParticleDetection.isInPouringZoon && isPouring && !isFinishedPouring) {
            bartender_rage++;
        }

    }

    public void ResetBottle() { // create a new bottle and remove the liquid 
        int bottleContent = (int)Random.Range(MinMaxBottleContent.x, MinMaxBottleContent.y);
        int remaining = isFinishedPouring ? bottleContent : source.container.maxParticles - source.indexCount; // counting how much liquid is left in the new bottle
        GameObject tmp;
        if (!isFinishedPouring) {
            tmp = Instantiate(prefab, transform.position, transform.rotation); // create a new bottle on the same position as the other one
        }
        else {
            Quaternion rot = new Quaternion(0,0,0,0);
            tmp = Instantiate(prefab, BottleSpawnPosition, rot); // create a new bottle
        }
        tmp.GetComponentInChildren<NVIDIA.Flex.FlexSourceActor>().container.maxParticles = remaining; // set the max particles in the new bottle, this is made so that the new bottle do not start with the same amount of liquid all the time
        tmp.GetComponentInChildren<BottlePlug>().isFinishedPouring = false; // stop it from inheriting the bool as the bottle with max content would still say it was finished_pouring.
        tmp.GetComponent<Rigidbody>().useGravity = true; // so it gets to the ground as the player releasing the bottle when they reset the bottle.
        tmp.name = gameObject.name;
        PlayerScript.drunkiness += liquid_in_glass;
        source.container.ResetParticle(); // removes all of the particles
        Destroy(gameObject); // destroy the current game object so that the new one can take its place
    }

}
