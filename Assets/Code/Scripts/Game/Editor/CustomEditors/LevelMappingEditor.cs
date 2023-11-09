namespace ProjectPBR.Editor.CustomEditors
{
    using UnityEngine;
    using UnityEditor;
    using ProjectPBR.Data.ScriptableObjects.Levels;
    using log4net.Core;
    using ProjectPBR.Data.ScriptableObjects.Levels.Structs;
    using NUnit.Framework;
    using System.Collections.Generic;

    [CustomEditor(typeof(ScenesMappingData))]
    public class LevelMappingEditor : Editor
    {
        private int _offsetValue; // Valore predefinito dell'offset
        private ScenesMappingData _levelMapping;

        private void OnEnable()
        {
            _levelMapping = target as ScenesMappingData;
        }

        public override void OnInspectorGUI()
        {
            SetLevelsSize();
            DrawDefaultInspector();
            DrawMappingBox();
        }

        /// <summary>
        /// Draws the GUI Box for mapping levels.
        /// </summary>
        private void DrawMappingBox()
        {
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Label("Map Levels with Offset", EditorStyles.boldLabel);

            _offsetValue = EditorGUILayout.IntField("Offset", _offsetValue);

            if (GUILayout.Button("Map Levels"))
                MapLevels(_offsetValue);

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Maps the levels with the given offset.
        /// </summary>
        /// <param name="offset">level index offset.</param>
        private void MapLevels(int offset)
        {
            for (int k = 0; k < _levelMapping.Stages.Count; k++)
            {
                for (int i = 0; i < _levelMapping.Stages[k].Levels.Count; i++)
                    _levelMapping.Stages[k].Levels[i] = new LevelMapData(i + offset);
            }
        }

        /// <summary>
        /// Sets the size of the levels for each stage.
        /// </summary>
        private void SetLevelsSize()
        {
            int size = _levelMapping.LevelsPerStage;

            for (int k = 0; k < _levelMapping.Stages.Count; k++)
            {
                List<LevelMapData> currentLevels = _levelMapping.Stages[k].Levels;

                while (currentLevels.Count > size)
                    currentLevels.RemoveAt(currentLevels.Count - 1);

                while (currentLevels.Count < size)
                    currentLevels.Add(new LevelMapData());
            }
        }

    }
}
