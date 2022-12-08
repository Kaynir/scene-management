using System;
using UnityEngine;

namespace Kaynir.Scenes
{
    [Serializable]
    public class SceneReference
    {
        [SerializeField] private int _buildIndex = 0;

        public int BuildIndex => _buildIndex;

        public SceneReference(int buildIndex)
        {
            _buildIndex = buildIndex;
        }

        public SceneReference() : this(0) { }

        public static implicit operator int(SceneReference scene)
        {
            return scene.BuildIndex;
        }
    }
}