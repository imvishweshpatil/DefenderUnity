using UnityEngine;

public static class ExtensionMethods
{
    public static float FlatDistance(this Vector3 source, Vector3 destination)
    {
        return Vector2.Distance(source, destination);
    }

    public static void MoveTowardsTarget(this Transform transform, Transform target, float speed)
    {
        var position = transform.position;
        var direction = target.position - position;
        direction.Normalize();
        position += direction * (speed * Time.deltaTime);
        transform.position = position;
    }
}