using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeMaterial : MonoBehaviour
{
    public Material material;

    private MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public void SetMaterial()
    {
        renderer.material = material;
    }

    public void SetMaterial(Material mat)
    {
        renderer.material = mat;
    }
}
