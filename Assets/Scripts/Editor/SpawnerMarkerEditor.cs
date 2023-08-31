using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnMarker))]
public class SpawnerMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Active | GizmoType.Pickable)]
    public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawner.transform.position, 0.5f);
    }
}