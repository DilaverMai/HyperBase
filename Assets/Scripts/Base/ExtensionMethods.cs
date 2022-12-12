using System.Text;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static bool CheckLayer(this GameObject go, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << go.layer));
    }

    public static Color SetAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static Transform[] FindWithLayer(GameObject[] list, LayerMask mask)
    {
        Transform[] enemys = new Transform[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            if (CheckLayer(list[i], mask))
            {
                enemys[i] = list[i].transform;
            }
        }

        return enemys;
    }

    public static string StripPunctuation(this string s)
    {
        var sb = new StringBuilder();
        foreach (char c in s)
        {
            if (!char.IsPunctuation(c))
                sb.Append(c);
        }

        return sb.ToString();
    }
    
    private static T Raycast<T>(this T gen,Vector3 pos,LayerMask masks) where T:MonoBehaviour
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, masks))
        {
            return hit.transform.GetComponent<T>();
        }

        return default(T);
    }
    
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }

#if UNITY_EDITOR
    public static void DrawDisc(Vector3 center, float radius, Color color)
    {
        Handles.color = color;
        Handles.DrawWireDisc(center, Vector3.down, radius);
    }
#endif
}
