using UnityEngine;
using System.Linq;

public static class UserDefinedExtensions
{
    public static void ClearChildren(this Transform _transform)
    {

        var children = _transform.Cast<Transform>().ToArray();

        foreach (var child in children)
        {

            Object.DestroyImmediate(child.gameObject);

        }

    }

}