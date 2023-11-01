namespace ProjectPBR.Editor.CustomEditors
{
    using UnityEngine;
    using UnityEditor;
    using ProjectPBR.Data.ScriptableObjects.Levels;

    [CustomEditor(typeof(LevelMapping))]
    public class LevelMappingEditor : Editor
    {
        private int _offsetValue; // Valore predefinito dell'offset
        private LevelMapping _levelMapping;

        private void OnEnable()
        {
            _levelMapping = target as LevelMapping;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawMappingBox();
        }

        private void DrawMappingBox()
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Label("Map Levels with Offset", EditorStyles.boldLabel);

            _offsetValue = EditorGUILayout.IntField("Offset", _offsetValue);

            if (GUILayout.Button("Map Levels"))
                MapLevels(_offsetValue);

            EditorGUILayout.EndVertical();
        }

        private void MapLevels(int offset)
        {
            for (int i = 0; i < _levelMapping.Levels.Length; i++)
            {
                _levelMapping.Levels[i].EasyBuildIndex = i + offset;
                _levelMapping.Levels[i].HardBuildIndex = i + offset;
            }
        }
    }
}
