using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    //List of events, randomize input keys list
    // timer 
    private static int[] input_keys;
    private float MaxTime = 1000;
    private float timer;
    // Start is called before the first frame update
    void Start() {
        timer = MaxTime;
        input_keys = new int[4];
        for(int i = 0; i < 4; i++) {
            input_keys[i] = i;
        }
    }

    public static int[] getInput() { 
        return input_keys; 
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime * PlayerScript.drunkiness / 2;
        
        if(timer <= 0) {
            timer = MaxTime;
            Debug.Log("Did a timer cycle");
            int eventID = 0;
            switch (eventID) {
                case 0: 
                {
                    reshuffle(input_keys);
                    break;
                }
            }

        }

    }


    private void reshuffle(int[] nums) {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < nums.Length; t++) {
            int tmp = nums[t];
            int r = Random.Range(t, nums.Length);
            nums[t] = nums[r];
            nums[r] = tmp;
        }
    }

}
