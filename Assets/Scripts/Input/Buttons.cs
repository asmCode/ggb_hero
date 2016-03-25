using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour
{
    public virtual event System.Action LeftButtonPressed;
    public virtual event System.Action RightButtonPressed;

    protected void OnLeftButtonPressed()
    {
        if (LeftButtonPressed != null)
            LeftButtonPressed();
    }

    protected void OnRightButtonPressed()
    {
        if (RightButtonPressed != null)
            RightButtonPressed();
    }
}
