using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    private SuiController m_suiController;
    private Rigidbody2D m_rigibody;

    public Water Water
    {
        get;
        private set;
    }

    public bool IsGrabable
    {
        get
        {
            return m_suiController.IsGrabable;
        }
    }

    public bool IsKinematic
    {
        get { return m_rigibody.isKinematic; }
        set { m_rigibody.isKinematic = value; }
    }

    public Dude Dude
    {
        get;
        private set;
    }

    public void Initialize(Water water)
    {
        Water = water;
    }

    public void SetController(SuiController suiController)
    {
        m_suiController = suiController;
    }

    private void Awake()
    {
        m_rigibody = GetComponent<Rigidbody2D>();
        Dude = GetComponent<Dude>();

        SetController(new SuiControllerIdleTest(this));
    }

    void Update()
    {
        m_suiController.UpdateSui();
    }

    void LateUpdate()
    {
        m_suiController.LateUpdateSui();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        m_suiController.ProcessTriggerEnter2D(other);
    }
}
