using UnityEngine;
using System.Collections;

public class InspectorValue<T>
{
    public delegate T CurrentValueDelegate();

    private T m_prev;
    private CurrentValueDelegate m_currentValueDelegate;

    public event System.Action Changed;

    public InspectorValue(CurrentValueDelegate currentValueDelegate)
    {
        m_currentValueDelegate = currentValueDelegate;
        m_prev = m_currentValueDelegate();
    }

    public void Check()
    {
        T curr = m_currentValueDelegate();
        if (m_prev.Equals(curr))
            return;

        m_prev = curr;

        if (Changed != null)
            Changed();
    }       
}
 