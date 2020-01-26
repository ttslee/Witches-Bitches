using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiTargetCamera : MonoBehaviour
{
    public List<Transform> players;
    public Vector3 offset;
    public float smoothTime = .1f;
    public float minSize = 3f;
    public float maxSize = 15f;
    public float zoomDelay = 7f;

    private Vector3 velocity;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }


    void LateUpdate()
    {
        if (players.Count == 0) return;
        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minSize, maxSize, GetGreatestDistance() / zoomDelay);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        return GetEncapsulatingBounds().center;
    }

    float GetGreatestDistance()
    {
        
        return Mathf.Max(GetEncapsulatingBounds().size.x, GetEncapsulatingBounds().size.y);
    }

    Bounds GetEncapsulatingBounds()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        return bounds;
    }
}
