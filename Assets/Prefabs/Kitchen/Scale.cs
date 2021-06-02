using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public void ScalePrefab(float scaleFactor)
    {
        Debug.Log("Scaling by " + scaleFactor);
        //aRSessionOrigin.transform.localScale = Vector3.one * scaleFactor;
        /* selectedObject.transform.Rotate(Vector3.up * scaleFactor);
        aRSessionOrigin.transform.localScale = Vector3.one * scaleFactor;
         Debug.Log("temple " + aRSessionOrigin.transform.localScale);*/
        transform.localScale = Vector3.one * scaleFactor;
    }
}
