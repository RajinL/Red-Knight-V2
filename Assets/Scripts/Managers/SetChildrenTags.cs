using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to set every child of this object to have the same tag as this object.
/// Should be used for Object Roots such as an enemy with multiple child objects.
/// Useful if every child object should be the same as the parent so that all 
/// damage behaves consistently across each object.
/// </summary>
public class SetChildrenTags : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        SetAllChildrenToParentTag();
    }
    private void SetAllChildrenToParentTag()
    {
        foreach (Transform t in gameObject.transform)
        {
            t.tag = gameObject.tag;
        }
    }
}
