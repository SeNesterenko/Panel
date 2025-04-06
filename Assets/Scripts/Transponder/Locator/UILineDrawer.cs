using System;
using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Locator
{
    public class UILineDrawer : MonoBehaviour
    {
        [SerializeField] private Image _lineImage;
        [SerializeField] private RectTransform rt;

        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _identColor;
        [SerializeField] private Color _default;

        private RectTransform _startPoint;
        private RectTransform _endPoint;

        public void Initialize(RectTransform startPoint, RectTransform endPoint)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            
            _lineImage.transform.SetAsFirstSibling();
            
            var scale = rt.localScale;
            scale.x = 1920f / Screen.width;
            rt.localScale = scale;
        }
        
        public void SetState(bool isIDENT, bool isSelected) =>
            _lineImage.color = isIDENT ? _identColor : isSelected ? _selectColor : _default;

        private void Update()
        {
            if (_startPoint && _endPoint)
                DrawLine();
        }

        private void DrawLine()
        {
            var startPos = _startPoint.position;
            var endPos = _endPoint.position;
            var direction = endPos - startPos;
            var distance = direction.magnitude;
            
            var rectTransform = _lineImage.rectTransform;
            rectTransform.position = startPos;
            rectTransform.sizeDelta = new Vector2(distance, rectTransform.sizeDelta.y);
            rectTransform.pivot = new Vector2(0, 0.5f);
            rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
    }
}