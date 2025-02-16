using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public Renderer mainRenderer;

    public TextMeshProUGUI textMovementDifficulty;

    public int movementDifficulty;

    private CellData cellData;

    public void SetTransparent(bool available)
    {
        if (available)
        {
            mainRenderer.material.color = Color.green;
        }
        else
        {
            mainRenderer.material.color = Color.red;
        }
    }

    public void Init(CellData data)
    {
        cellData = data;
        this.movementDifficulty = data.movementDifficulty;
        textMovementDifficulty.text = movementDifficulty.ToString();
    }

    private void Update()
    {
        UpdateMovementDifficulty();


    }

    private void UpdateMovementDifficulty()
    {
        if (movementDifficulty != cellData.movementDifficulty)
        {
            if (movementDifficulty < 0)
            {
                movementDifficulty = cellData.movementDifficulty;
            }
            else
            {
                cellData.movementDifficulty = movementDifficulty;
            }

            textMovementDifficulty.text = movementDifficulty.ToString();
        }
    }
}
