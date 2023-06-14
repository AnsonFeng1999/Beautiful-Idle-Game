using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material Highlight, oldMaterial;

    private void OnMouseEnter()
    {
        gameObject.GetComponent<MeshRenderer>().material = Highlight;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
    }
}
