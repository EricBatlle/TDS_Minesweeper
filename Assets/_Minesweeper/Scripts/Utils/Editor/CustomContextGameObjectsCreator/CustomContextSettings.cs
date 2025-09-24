using UnityEngine;

namespace Utils.Editor
{
 
    [CreateAssetMenu(fileName = "CustomContextSettings", menuName = "Scriptable Objects/CustomContextSettings")]
    public class CustomContextSettings : ScriptableObject
    {
        public GameObject baseTextPrefab;
        public GameObject baseButtonPrefab;
        public GameObject baseInputFieldPrefab;
    }
}
