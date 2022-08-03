using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    MeshRenderer _renderer;
    PaintWallManager _manager;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _manager = GetComponentInParent<PaintWallManager>();
    }

    void OnMouseOver()
    {
        if (_renderer != null && _renderer.material.color != Color.red)
        {
            _renderer.material.color = Color.red;
            _manager.IncreaseCount();
        }
    }
}
