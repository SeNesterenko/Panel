using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Locator
{
    public class PlaneView : MonoBehaviour
    {
        [SerializeField] private Image _planeImage;
        [SerializeField] private Image _arrowImage;
        [SerializeField] private Image _joiningLineImage;
        [SerializeField] private GameObject _selectedContour;
        
        [SerializeField] private Color _identColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _selectColor;

        [SerializeField] private float _pathPointAppearTime = 1;
        
        [field: SerializeField] public RectTransform LineRendererTarget { get; private set; }

        private List<PathPointObject> _pathPoints;
        private float _lastTime;
        private int _lastIndex;

        [field: SerializeField] public GameObject PlaneContainer { get; private set; }

        public void Initialize(List<PathPointObject> pathPointsObjects) => 
            _pathPoints = pathPointsObjects;

        public void SetState(bool isIDENT, bool isSelected)
        {
            if (isIDENT)
            {
                _planeImage.color = _defaultColor;
                _arrowImage.color = _identColor;
                _joiningLineImage.color = _identColor;
                _pathPoints.ForEach(p => p.SetColor(_identColor));
                _selectedContour.SetActive(false);
            }
            else if (isSelected)
            {
                _planeImage.color = _selectColor;
                _arrowImage.color = _selectColor;
                _joiningLineImage.color = _selectColor;
                _pathPoints.ForEach(p => p.SetColor(_selectColor));
                _selectedContour.SetActive(true);
            }
            else
            {
                _planeImage.color = _defaultColor;
                _arrowImage.color = _identColor;
                _joiningLineImage.color = _identColor;
                _pathPoints.ForEach(p => p.SetColor(_identColor));
                _selectedContour.SetActive(false);
            }
        }

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

        public void Dispose() => 
            _pathPoints?.ForEach(p => Destroy(p.gameObject));
    }
}