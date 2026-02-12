using UnityEngine;

public static class ColliderUtils
{
    public static Vector3[] GetCapsuleColliderCornerPoints(CapsuleCollider collider)
    {
        Vector3[] corners = new Vector3[7];

        Vector3 center = collider.transform.TransformPoint(collider.center);

        corners[0] = center;
        corners[1] = center + Vector3.up * collider.height / 2f;
        corners[2] = center - Vector3.up * collider.height / 2f;
        corners[3] = center + Vector3.right * collider.radius;
        corners[4] = center - Vector3.right * collider.radius;
        corners[5] = center + Vector3.forward * collider.radius;
        corners[6] = center - Vector3.forward * collider.radius;

        return corners;
    }
}
