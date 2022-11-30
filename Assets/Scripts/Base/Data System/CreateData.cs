#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Base.DataSystem
{
    public class CreateData<T> where T : ScriptableObject
    {
        public static T CreateMyAsset(string name)
        {
            T asset = ScriptableObject.CreateInstance<T>();

            AssetDatabase.CreateAsset(asset, "Assets/" + name + ".asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;

            return asset;
        }
    }
}
#endif