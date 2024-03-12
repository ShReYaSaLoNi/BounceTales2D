using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0,0,-10);
    public float smoothing;
    public float cameraDistance = 10f;
    public Rect bounds;

    Vector2 minPosition, maxPosition;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();    
        CalculateBoundary();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 o;
        if(Input.GetAxisRaw("Horizontal") >= 0){
            o = offset;
        }
        else{
            o = new Vector3(-offset.x, offset.y, offset.z);
        }
        Vector3 targetPosition = target.position + o;
        if(bounds.width > 0 && bounds.height > 0){
            targetPosition = LimitedPositionToBounds(targetPosition);
        }
        transform.position = Vector3.Lerp(transform.position , targetPosition, smoothing * Time.deltaTime);
    }

    void CalculateBoundary(){
        Vector3 screenMin = cam.ViewportToWorldPoint(new Vector3(0,0, cameraDistance));
        Vector3 screenMax = cam.ViewportToWorldPoint(new Vector3(1,1, cameraDistance));

        float halfWidth = (screenMax.x - screenMin.x)/2f;
        float halfHeight = (screenMax.y - screenMin.y)/2f;

        minPosition = new Vector2(bounds.xMin + halfWidth, bounds.yMin + halfHeight);
        maxPosition = new Vector2(bounds.xMax - halfWidth, bounds.yMax + halfHeight);
    }

    Vector3 LimitedPositionToBounds(Vector3 position){
        
        if(position.x < minPosition.x){
            position.Set(minPosition.x,position.y,position.z);
        }
        else if(position.x > maxPosition.x){
            position.Set(maxPosition.x,position.y,position.z);
        }

        if(position.y < minPosition.y){
            position.Set(position.x,minPosition.y,position.z);
        }
        else if(position.y > maxPosition.y){
            position.Set(position.x,maxPosition.y,position.z);
        }

        return position;
    }

    public void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.green;

    // Draw the wireframe rectangle using Gizmos.DrawLine
     Gizmos.DrawLine(new Vector2(bounds.xMin, bounds.yMin), new Vector2(bounds.xMax, bounds.yMin));
     Gizmos.DrawLine(new Vector2(bounds.xMax, bounds.yMin), new Vector2(bounds.xMax, bounds.yMax));
     Gizmos.DrawLine(new Vector2(bounds.xMax, bounds.yMax), new Vector2(bounds.xMin, bounds.yMax));
     Gizmos.DrawLine(new Vector2(bounds.xMin, bounds.yMax), new Vector2(bounds.xMin, bounds.yMin));
    }


}


