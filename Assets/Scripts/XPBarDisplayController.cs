using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBarDisplayController : MonoBehaviour
{
    public RectTransform bar;
    void Update()
    {
        var rectParent = (RectTransform)bar.parent;
        float parentWidth = rectParent.rect.xMax - rectParent.rect.xMin;
        bar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 5, parentWidth * Mathf.Clamp01(XPLevelController.instance.xp / XPLevelController.instance.requiredXP));
    }
}
