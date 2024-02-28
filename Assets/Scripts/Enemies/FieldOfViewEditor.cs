using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private float radius;
    private float angle;
    private bool canSeePlayer;
    private Transform playerTransform;

    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        radius = fov.GetRadius();
        angle = fov.GetAngle();
        canSeePlayer = fov.WasPlayerDetected();
        playerTransform = fov.GetPlayerTransform();
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * radius);

        if (canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, playerTransform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}