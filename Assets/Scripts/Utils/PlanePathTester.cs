using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class PlanePathTester : MonoBehaviour
    {
        public Transform[] PathPoints;
        
        [SerializeField] private int _gizmoSegmentCount = 10;
        [SerializeField] private Color _gizmoColor = Color.cyan;
        [SerializeField] private PathType _pathType;

        private void OnDrawGizmos()
        {
            if (!IsSelectedSelfOrChildren())
                return;

            DrawPathGizmo();
        }

        private bool IsSelectedSelfOrChildren()
        {
            var target = gameObject.transform;
    
            while (target != null)
            {
                if (target == transform)
                    return true;
    
                target = target.parent;
            }

            return false;
        }
        
        private Vector3[] CreatePathPoints()
        {
            var path = new Vector3[PathPoints.Length];
            for (var i = 0; i < PathPoints.Length; i++)
            {
                path[i] = PathPoints[i].position;
            }

            return path;
        }

        
        private void DrawPathGizmo() => 
            DoTweenGizmoDrawer.DrawPath(_pathType, CreatePathPoints(), _gizmoSegmentCount, _gizmoColor);
    }
}