using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Scenes
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Scenes/Scene Collection")]
    public class SceneCollection : ScriptableObject
    {
        [field: SerializeField] public List<SceneReference> SceneList = new List<SceneReference>();
    }
}