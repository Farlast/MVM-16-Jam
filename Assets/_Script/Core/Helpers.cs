using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    public static void DeleteChildren(this Transform transform)
    {
        foreach (Transform child in transform) Object.Destroy(child.gameObject);
    }
    public static Camera _camera;
    public static Camera Camera
    {
        get{ if (_camera == null) _camera = Camera.main; return _camera; }
    }

    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;
    public static bool IsOverUi()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }

    public static Vector2 GetWorldPositionOfCanvas(RectTransform rect)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, rect.position, Camera, out var results);
        return results;
    }

    public static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float timeMilisec)
    {
        if (WaitDictionary.TryGetValue(timeMilisec, out var wait)) return wait;

        WaitDictionary[timeMilisec] = new WaitForSeconds(timeMilisec);
        return WaitDictionary[timeMilisec];
    }
    public static int Layermask_to_layer(LayerMask layerMask)
    {
        return (int) Mathf.Log(layerMask.value, 2);
    }
    public static Vector3 GetDirection(Vector3 from ,Vector3 to)
    {
        return (to - from).normalized;
    }
    public static Vector3 DirectionFromAngle2D(float eulerY, float angleInDegree)
    {
        angleInDegree += eulerY;
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), Mathf.Cos(angleInDegree * Mathf.Deg2Rad), 0);
    }
    public static float AngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
