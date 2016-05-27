using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour
{
    public virtual event System.Action LeftButtonPressed;
    public virtual event System.Action LeftButtonReleased;
    public virtual event System.Action RightButtonPressed;
    public virtual event System.Action RightButtonReleased;

    protected void OnLeftButtonPressed()
    {
        if (LeftButtonPressed != null)
            LeftButtonPressed();
    }

    protected void OnLeftButtonReleased()
    {
        if (LeftButtonReleased != null)
            LeftButtonReleased ();
    }

    protected void OnRightButtonPressed()
    {
        if (RightButtonPressed != null)
            RightButtonPressed();
    }

    protected void OnRightButtonReleased()
    {
        if (RightButtonReleased != null)
            RightButtonReleased();
    }
}
