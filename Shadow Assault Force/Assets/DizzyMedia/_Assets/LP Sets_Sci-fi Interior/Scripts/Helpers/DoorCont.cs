using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCont : MonoBehaviour {
    
    public enum DoorState {
        
        unlocked = 0,
        locked = 1,
        
    }//DoorState
    
    
    public bool useDebug;
    
    [Space]
    
    [Header("References")]
    
    [Space]
    
    public BoxCollider doorTrigger;
    public MeshRenderer doorLights;
    public MeshRenderer doorPanel;
    public Animator doorAnim;
    public string doorAnimString;
    
    [Space]
    
    [Header("User Options")]
    
    [Space]
    
    public DoorState doorState;
    public string tagToCheck;
    public Material[] lightMats;
    
    [Space]
    
    public bool useSounds;
    
    public AudioSource audSource;
    public AudioClip[] audClips;
    public float audVol;
    
    [Space]
    
    [Header("Auto")]
    
    [Space]
    
    public bool doorOpen;
    public bool playDet;
    public bool locked;

    void Start() {
        
        playDet = false;
        locked = false;
        
        StartCoroutine("DoorInit");
        
    }//start

    
    private IEnumerator DoorInit(){
        
        yield return new WaitForSeconds(0.1f);
        
        if((int)doorState == 1){
            
            LockState(true);
            
        }//doorState == locked
        
    }//DoorInit
    
    private void OnTriggerEnter(Collider other) {
        
        if(!locked){
        
            if(other.gameObject.tag == tagToCheck){

                playDet = true;
                
                if(useDebug){
                    
                    Debug.Log("Player Entered");
                    
                }//useDebug

            }//tag == tagToCheck
    
        }//!locked
        
    }//OnTriggerEnter    

    private void OnTriggerStay(Collider other) {
        
        if(!locked){
        
            if(playDet){

                if(!doorOpen){

                    Door(true);

                }//!doorOpen

            }//playDet
            
        }//!locked
    
    }//OnTriggerStay 
    
    private void OnTriggerExit(Collider other) {
        
        if(!locked){
        
            if(other.gameObject.tag == tagToCheck){

                playDet = false;
                Door(false);
                
                if(useDebug){
                    
                    Debug.Log("Player Exit");
                    
                }//useDebug

            }//tag == tagToCheck
    
        }//!locked
            
    }//OnTriggerExit 
    
    
    public void Door(bool state){
        
        if(!locked){
        
            doorOpen = state;
            doorAnim.SetBool(doorAnimString, state);
            
            if(useSounds){
                
                if(state){
                    
                    audSource.PlayOneShot(audClips[0], audVol);
                
                //state
                } else {
                    
                    audSource.PlayOneShot(audClips[1], audVol);
                    
                }//state
                
            }//useSounds
            
            if(useDebug){
                    
                Debug.Log("Door Opened = " + state);
                    
            }//useDebug
        
        }//!locked
            
    }//Door
    
    public void LockState(bool state){
        
        locked = state;
        
        if(state){
            
            doorLights.material = lightMats[1];
            
            if(doorPanel != null){
                
                doorPanel.material = lightMats[1];
            
            }//doorPanel != null
            
            doorTrigger.enabled = false;
            
        //state
        } else {
            
            doorLights.material = lightMats[0];
            
            if(doorPanel != null){
                
                doorPanel.material = lightMats[0];
                
                doorTrigger.enabled = true;
            
            }//doorPanel != null
            
        }//state
        
    }//LockState
    
}
