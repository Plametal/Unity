// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {

//     public Transform followTransform;
//     public BoxCollider2D mapBounds;

//     private float xMin, xMax, yMin, yMax;
//     private float camY,camX;
//     private float camOrthsize;
//     private float cameraRatio;
//     private Camera mainCam;
//     private Vector3 smoothPos;
//     public float smoothSpeed = 0.5f;

//     private void Start()
//     {
//         xMin = mapBounds.bounds.min.x;
//         xMax = mapBounds.bounds.max.x;
//         yMin = mapBounds.bounds.min.y;
//         yMax = mapBounds.bounds.max.y;
//         mainCam = GetComponent<Camera>();
//         camOrthsize = mainCam.orthographicSize;
//         cameraRatio = (xMax + camOrthsize) / 2.0f;
//     }
//     // Update is called once per frame
//     void FixedUpdate()
//     {
//         camY = Mathf.Clamp(followTransform.position.y, yMin + camOrthsize, yMax - camOrthsize);
//         camX = Mathf.Clamp(followTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);
//         smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);
//         this.transform.position = smoothPos;
        
        
//     }
// }

// using System.Collections; 
// using System.Collections.Generic; 
// using UnityEngine; 

// public class cameraCtrl : MonoBehaviour 
// { 
// 	public GameObject model; 
//     public float CameraZ = -10; 
    
//     void FixedUpdate() 
//     { 
//     	Vector3 targetPos = new Vector3(model.transform.position.x, model.transform.position.y, CameraZ); 
//         transform.position = Vector3.Lerp(transform.position, targetPos, 2f*Time.deltaTime); 
//     } 
// }