using UnityEngine;
using System.Collections;

public class GrabPanel : MonoBehaviour
{
    #region Inspector
    public UILabel m_grabCapacity;
    public UILabel m_incGrabCounter;
    #endregion

    public void SetGrabCapacity(int value)
    {
        m_grabCapacity.text = string.Format("x {0}", value.ToString());
    }

    public void SetGrabCounter(int suiSaved, int suiSavedGoal)
    {
        m_incGrabCounter.text = string.Format("{0} / {1}", suiSaved.ToString(), suiSavedGoal.ToString());
    }

    public void ShowGrabCounter(bool show)
    {
        NGUITools.SetActive(m_incGrabCounter.gameObject, show);
    }
}
