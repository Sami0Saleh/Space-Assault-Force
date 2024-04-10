using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[AddComponentMenu("Dizzy Media/Invector World Components/Helpers/Misc/Transform Indicator")]
public class TransInd : MonoBehaviour {
    
    public enum GizmoType {
     
        RayLine = 0,
        Box = 1,
        
    }//GizmoType

    public enum RayDirection {
    
        Forward = 0,
        Left = 1,
        Right = 2,
        Back = 3,
        Down = 4,
    
    }//ForwardDirection
    
    [Space]
    
    public bool useGizmos;
    
    [Space]
    
    public GizmoType gizmoType;
    public RayDirection rayDirection;

    [Space]
    
    public float rayDist;
    
    [Space]
    
    public Color gizmoColor;
    public Vector3 gizmoBox_Size;
    
    void Start(){}

     void OnDrawGizmos() {
         
         if(useGizmos){
                          
             if((int)gizmoType == 0){
                 
                 Gizmos.color = gizmoColor;
                 Vector3 direction = new Vector3(0, 0, 0);
                 
                 if((int)rayDirection == 0){
                 
                    direction = transform.TransformDirection(Vector3.forward) * rayDist;
                 
                 }//rayDirection = forward
                 
                 if((int)rayDirection == 1){
                 
                    direction = transform.TransformDirection(Vector3.left) * rayDist;
                 
                 }//rayDirection = left
                 
                 if((int)rayDirection == 2){
                 
                    direction = transform.TransformDirection(Vector3.right) * rayDist;
                 
                 }//rayDirection = right
                 
                 if((int)rayDirection == 3){
                 
                    direction = transform.TransformDirection(Vector3.back) * rayDist;
                 
                 }//rayDirection = back
                 
                 if((int)rayDirection == 4){
                 
                    direction = transform.TransformDirection(Vector3.down) * rayDist;
                 
                 }//rayDirection = down
                 
                 Gizmos.DrawRay(transform.position, direction);
                 
             }//showRay
             
             if((int)gizmoType == 1){
                 
                 Gizmos.matrix = this.transform.localToWorldMatrix;
                 Gizmos.color = gizmoColor;
                 Gizmos.DrawCube(Vector3.zero, gizmoBox_Size);
             
             }//showBox
             
         }//useGizmos
         
     }//OnDrawGizmos
    
    
}
