using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    private Rigidbody2D m_rigibody;

    public SuiController SuiController
    {
        get;
        private set;
    }

    public Water Water
    {
        get;
        private set;
    }

    public bool IsGrabable
    {
        get
        {
            return SuiController.IsGrabable;
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

    public DudeAnimator DudeAnimator
    {
        get;
        private set;
    }

    public void Initialize(Water water)
    {
        Water = water;
    }

    public void Destroy()
    {
        // This will force to call SuiController.Leaving();
        SetController(null);

        Object.Destroy(transform.parent.gameObject);
    }

    public void SetController(SuiController suiController)
    {
        if (SuiController != null)
            SuiController.Leaving();

        SuiController = suiController;
    }

    private void Awake()
    {
        m_rigibody = GetComponent<Rigidbody2D>();
        Dude = GetComponent<Dude>();
        DudeAnimator = GetComponent<DudeAnimator>();

        SetController(new SuiControllerIdleTest(this));
    }

    void FixedUpdate()
    {
        SuiController.UpdateSui();
    }

    void LateUpdate()
    {
        SuiController.LateUpdateSui();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SuiController.ProcessTriggerEnter2D(other);
    }
}
