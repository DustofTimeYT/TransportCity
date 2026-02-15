using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public void ToggleMenu(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}