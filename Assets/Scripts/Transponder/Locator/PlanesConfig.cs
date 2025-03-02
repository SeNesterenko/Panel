using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEditor;
using Utils;

namespace Transponder.Locator
{
    [CreateAssetMenu(menuName = "Configs/PlanesConfig", fileName = "PlanesConfig")]
    public class PlanesConfig : ScriptableObject
    {
        [SerializeField] private List<PlaneConfigData> _planes;

        [field: SerializeField] public Vector3 HintOffset { get; private set; }
        [field: SerializeField] public int CountPathPointsObjects { get; private set; }

        [Header("Cheats")]
        [SerializeField] private int _indexToUpdate;
        [SerializeField] private GameObject _transponderWindow;

        public List<PlaneConfigData> Planes => _planes;

        [UsedImplicitly]
        private void UpdatePathFromPrefab()
        {
            if (_transponderWindow == null)
            {
                Debug.LogError("PlanesConfig doesn't contain TransponderWindow!");
                return;
            }

            if (_indexToUpdate < 0 || _indexToUpdate >= _planes.Count)
            {
                Debug.LogError($"Incorrect index {_indexToUpdate}! Must be from 0 to {_planes.Count - 1}");
                return;
            }

            var tempInstance = Instantiate(_transponderWindow);
            var tester = tempInstance.GetComponentInChildren<PlanePathTester>();

            if (tester == null)
            {
                Debug.LogError("PlanePathTester can't be found in the TransponderWindow!");
                DestroyImmediate(tempInstance);
                return;
            }

            _planes[_indexToUpdate].PathPoints.Clear();

            foreach (var point in tester.PathPoints) 
                _planes[_indexToUpdate].PathPoints.Add(point.position);

            DestroyImmediate(tempInstance.gameObject);

            Debug.Log("Path successfully updated!");
        }
        
#if UNITY_EDITOR
    [CustomEditor(typeof(PlanesConfig))]
    public class PlanesConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var config = (PlanesConfig)target;

            if (GUILayout.Button("Update Path from Prefab")) 
                config.UpdatePathFromPrefab();
        }
    }
#endif
    }
}
