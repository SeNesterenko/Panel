using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Locator
{
    public class PlaneView : MonoBehaviour
    {
        [SerializeField] private Image _planeImage;
        [SerializeField] private Image _arrowImage;
        
        [SerializeField] private Color _identColor;
        [SerializeField] private Color _defaultColor;

        [SerializeField] private float _pathPointAppearTime = 1;

        private List<PathPointObject> _pathPoints;
        private float _lastTime;
        private int _lastIndex;

        [field: SerializeField] public GameObject PlaneContainer { get; private set; }

        public void Initialize(List<PathPointObject> pathPointsObjects) => 
            _pathPoints = pathPointsObjects;

        public void SetIDENTState(bool isActive) => 
            _planeImage.color = isActive ? _identColor : _defaultColor;

        private void Update()
        {
            if (_pathPoints == null || _pathPoints.Count == 0) 
                return;
            
            if (!(Time.time - _lastTime >= _pathPointAppearTime)) 
                return;
            
            _lastTime = Time.time;
            _pathPoints[_lastIndex].transform.position = transform.position;
            _pathPoints[_lastIndex].gameObject.SetActive(true);
            _lastIndex = (_lastIndex + 1) % _pathPoints.Count;
        }
    }
}