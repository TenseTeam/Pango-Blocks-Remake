namespace VUDK.Extensions.Gizmos
{
    using UnityEngine;

    public static class GizmosExtension
    {
        public static void DrawArrow(Vector3 from, Vector3 to, float size = 0.2f)
        {
            Vector3 direction = to - from;
            Vector3 arrowPoint = to - direction.normalized * 0.2f;

            Gizmos.DrawLine(from, arrowPoint);
            Gizmos.DrawLine(arrowPoint, arrowPoint + Quaternion.Euler(0, 160, 0) * direction.normalized * size);
            Gizmos.DrawLine(arrowPoint, arrowPoint + Quaternion.Euler(0, -160, 0) * direction.normalized * size);
        }

        //public static void DrawArrowRay(Vector3 from, Vector3 to, float size = 0.2f)
        //{
        //    Gizmos.DrawRay(from, to - from);
        //    Gizmos.DrawRay(to, Quaternion.Euler(0f, 135f, 0f) * (to - from).normalized * size);
        //    Gizmos.DrawRay(to, Quaternion.Euler(0f, -135f, 0f) * (to - from).normalized * size);
        //}

        public static void DrawWireCubeWithRotation(Vector3 position, Quaternion rotation, Vector3 size)
        {
            Matrix4x4 originalMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.matrix = originalMatrix;
        }
    }
}