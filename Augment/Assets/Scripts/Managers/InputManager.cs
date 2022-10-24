using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject currSelected;

    void Update()
    {
        for (int i=0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                Vector3 tPos = Camera.main.ScreenToWorldPoint(t.position);
                Debug.Log(tPos);
                Ray r = Camera.main.ScreenPointToRay(t.position);
                RaycastHit hit;
                Physics.Raycast(r, out hit);
                if (hit.collider != null)
                {
                    Debug.Log("Hit an object");
                    currSelected = hit.collider.gameObject;
                }
            }
        }
    }
}
