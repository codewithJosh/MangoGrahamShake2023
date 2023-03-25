using System.Linq;
using UnityEngine;

public static class UserDefinedExtensions
{

    #region CLEAR_CHILDREN_METHOD

    public static void ClearChildren(this Transform _transform)
    {

        var children = _transform.Cast<Transform>().ToArray();

        foreach (var child in children)

            Object.DestroyImmediate(child.gameObject);

    }

    #endregion

}