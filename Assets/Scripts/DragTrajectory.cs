using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    public static DragTrajectory Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void OnDrag(Vector3 startPos, Vector3 endPos)
    {
        startPos.y = 0.1f;
        endPos.y = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
    public void ResetLine()
    {
        lineRenderer.positionCount = 0;
    }
}
