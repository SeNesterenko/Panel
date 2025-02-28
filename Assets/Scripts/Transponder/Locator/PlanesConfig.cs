using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Transponder.Locator
{
    [CreateAssetMenu(menuName = "Configs/PlanesConfig", fileName = "PlanesConfig")]
    public class PlanesConfig : ScriptableObject
    {
        [SerializeField] private List<PlaneConfigData> _planes;

        [field: SerializeField] public Vector3 HintOffset { get; private set; }

        [Header("Cheats")]
        [SerializeField] private int _indexToUpdate;
        [SerializeField] private GameObject _transponderWindow;
        
        public List<PlaneConfigData> Planes => _planes;

        [ContextMenu("Update Path from Prefab")]
        public void UpdatePathFromPrefab()
        {
            if (_transponderWindow == null)
            {
                Debug.LogError("PlanesConfig doesn't contain TransponderWindow!");
                return;
            }

            if (_indexToUpdate < 0 || _indexToUpdate >= _planes.Count)
            {
                Debug.LogError($"Incorrect index {_indexToUpdate}! Have to from 0 to {_planes.Count - 1}");
                return;
            }
            
            var tempInstance = Instantiate(_transponderWindow);
            var tester = tempInstance.GetComponentInChildren<PlanePathTester>();
            
            if (tester == null)
            {
                Debug.LogError("PlanePathTester cant be found in the TransponderWindow!");
                DestroyImmediate(tempInstance);
                return;
            }
            
            _planes[_indexToUpdate].PathPoints.Clear();

            foreach (var point in tester.PathPoints) 
                _planes[_indexToUpdate].PathPoints.Add(point.position);

            DestroyImmediate(tempInstance.gameObject);

            Debug.Log("Path successful updated!");
        }
    }
}