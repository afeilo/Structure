using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TEmptyGraphic : Graphic
{
    //public override void OnRebuildRequested()
    //{
    //}
    protected override void UpdateMaterial()
    {
    }
    protected override void UpdateGeometry()
    {
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
}
