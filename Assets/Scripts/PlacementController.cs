using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    GameObject selectedObject;
    private bool isPlaced;
    GameObject Temple;
    private ARSessionOrigin aRSessionOrigin;
    public ARPlaneManager ArPlane;

    [SerializeField]
    private ARPlaneManager arPlaneManager;

    public GameObject PlacedPrefab
    {

        get
        {
            if (placedPrefab == null)
            {
                placedPrefab = Instantiate(placedPrefab, Vector3.zero, new Quaternion(-90, 180, 0, 0));

            }
            return placedPrefab;
        }
    }

    public void ScalePrefab(float scaleFactor)
    {
        Debug.Log("Scaling by "+scaleFactor);
        //aRSessionOrigin.transform.localScale = Vector3.one * scaleFactor;
        /* selectedObject.transform.Rotate(Vector3.up * scaleFactor);
        aRSessionOrigin.transform.localScale = Vector3.one * scaleFactor;
         Debug.Log("temple " + aRSessionOrigin.transform.localScale);*/
        selectedObject.transform.localScale = Vector3.one * scaleFactor;
    }

    private ARRaycastManager arRaycastManager;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
             aRSessionOrigin = GetComponent< ARSessionOrigin>();

}

bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;

        return false;
    }


    public void DeactivatePlaneDetection()
    {
       

        arPlaneManager.enabled = false;
    }


    public void DestroyPlacement()
    {
        isPlaced = false;
        Debug.Log("destroy placement");
        DestroyImmediate(placedPrefab);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            selectedObject = Instantiate(placedPrefab, Vector3.zero + new Vector3(0, 0, 5), new Quaternion(0, 180, 0, 0));
        }


        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (!isPlaced) { 
            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                Debug.Log("touching################ "+ hitPose.position);
                foreach (var plan in ArPlane.trackables)
                {
                    plan.gameObject.SetActive(false);
                }
                selectedObject = Instantiate(placedPrefab, hitPose.position + new Vector3(0,0,5), new Quaternion(0, 180, 0, 0));
                DeactivatePlaneDetection();

                isPlaced = true;
            }
        }


    }


    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}