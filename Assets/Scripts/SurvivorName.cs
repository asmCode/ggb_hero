using UnityEngine;
using System.Collections;

public class SurvivorName : MonoBehaviour
{
    public UILabel m_label;
    public TweenAlpha m_tween;

    public void Awake()
    {
        m_tween = GetComponent<TweenAlpha>();
    }

    public void SetName(string name, Color color)
    {
        m_label.text = name;
        m_label.color = color;

        RefreshParentGrid();

        m_tween.ResetToBeginning();
        m_tween.PlayForward();
    }

    public void Disapeared()
    {
        gameObject.SetActive(false);
        RefreshParentGrid();
    }

    private void RefreshParentGrid()
    {
        var grid = gameObject.GetComponentInParent<UIGrid>();
        if (grid != null)
            grid.enabled = true;
    }
}
