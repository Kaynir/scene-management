using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Scenes
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Scenes/Scene Collection")]
    public class SceneCollection : ScriptableObject
    {
        [SerializeField] private SceneReference _activeScene = new SceneReference();
        [SerializeField] private List<SceneReference> _extraScenes = new List<SceneReference>();

        public int ActiveScene => _activeScene;
        public List<int> ExtraScenes => _extraScenes.ConvertAll(s => s.BuildIndex);
    }
}