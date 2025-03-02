using UnityEngine;
using UnityEngine.UI;

namespace Transponder.Locator
{
    public class PathPointObject : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void SetColor(Color color) => 
            _image.color = color;
    }
}