namespace VUDK.Extensions.Gizmos
{
    using UnityEngine;

    public static class GizmosExtension
    {
        public static void DrawArrow(Vector3 from, Vector3 to, Vector3 amplitude, float size = 0.2f)
        {
            Vector3 direction = to - from;
            Vector3 arrowPoint = to - direction.normalized * 0.2f;

            Gizmos.DrawLine(from, arrowPoint);
            Gizmos.DrawLine(arrowPoint, arrowPoint + Quaternion.Euler(amplitude) * direction.normalized * size);
            Gizmos.DrawLine(arrowPoint, arrowPoint + Quaternion.Euler(-amplitude) * direction.normalized * size);
        }

        public static void DrawWireCubeWithRotation(Vector3 position, Quaternion rotation, Vector3 size)
        {
            Matrix4x4 originalMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.matrix = originalMatrix;
        }
    }
}