using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionScr : MonoBehaviour
{
    public Renderer mainRenderer;
    public Vector2Int size = Vector2Int.one;
    private Color _materialColor;

    private void Awake()
    {
        _materialColor = mainRenderer.material.color;
    }

    public void SetTransparent(bool available)
    {
        if (available)
        {
            mainRenderer.material.color = _materialColor;
        }
        else
        {
            mainRenderer.material.color = Color.red;
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    Gizmos.color = new Color(0f,0.1f,0.8f,0.3f);
                }
                else
                {
                    Gizmos.color = new Color(0f, 0.1f, 0.6f, 0.3f); ;
                }

                Gizmos.DrawCube(transform.position + new Vector3(x, 0.025f, y), new Vector3(1f, 0.05f, 1f));
            }
        }
    }
}
