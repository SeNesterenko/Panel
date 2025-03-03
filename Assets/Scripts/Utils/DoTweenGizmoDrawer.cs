using System.Reflection;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;

namespace Utils
{
    public static class DoTweenGizmoDrawer
    {
        public static void DrawPath(PathType pathType, Vector3[] waypoints, int gizmoSegmentsCount, Color gizmoColor)
        {
            var path = new Path(pathType, waypoints, gizmoSegmentsCount, gizmoColor);

            var finalizeMethod = typeof(Path).GetMethod("FinalizePath", BindingFlags.NonPublic | BindingFlags.Instance);

            if (finalizeMethod == null)
            {
                Debug.LogError("Method FinalizePath not found");
                return;
            }

            var drawMethod = typeof(Path).GetMethod("Draw", BindingFlags.NonPublic | BindingFlags.Instance);
            if (drawMethod == null)
            {
                Debug.LogError("Method Draw not found");
                return;
            }

            finalizeMethod.Invoke(path, new object[] {false, AxisConstraint.None, Vector3.zero});
            drawMethod.Invoke(path, System.Array.Empty<object>());
        }
    }
}