using UnityEngine;
using System.Collections;

public class InputControllerProvider : MonoBehaviour
{
    public VirtualButtons m_virtualButtonsPrefab;
    public KeyboardButtons m_keyboardButtonsPrefab;
    public KeyboardStick m_keyboardStickPrefab;
    public VirtualStick m_virtualStickPrefab;

    private Buttons m_buttonsController;

    public Buttons GetButtonsController()
    {
        if (m_buttonsController == null)
        {
#if UNITY_EDITOR
            m_buttonsController = Instantiate(m_keyboardButtonsPrefab);
#else
            m_buttonsController = Instantiate(m_virtualButtonsPrefab);
#endif
        }

        return m_buttonsController;
    }
}
