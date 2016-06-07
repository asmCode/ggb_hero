using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    private Rigidbody2D m_rigibody;
    private SpriteRenderer m_bodySprite;
    private SpriteRenderer m_headSprite;
    private SpriteRenderer m_handLeftSprite;
    private SpriteRenderer m_handRightSprite;
    private SpriteRenderer m_legLeftSprite;
    private SpriteRenderer m_legRightSprite;

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

    public int SortOrder
    {
        get
        {
            return m_bodySprite.sortingOrder;
        }

        set
        {
            m_bodySprite.sortingOrder = value;
            m_headSprite.sortingOrder = value;
            m_handLeftSprite.sortingOrder = value;
            m_handRightSprite.sortingOrder = value;
            m_legLeftSprite.sortingOrder = value;
            m_legRightSprite.sortingOrder = value;
}
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

        Object.Destroy(transform.parent.parent.gameObject);
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

        m_bodySprite = GetComponent<SpriteRenderer>();
        m_headSprite = transform.Find("Head").GetComponent<SpriteRenderer>();
        m_handLeftSprite = transform.parent.Find("HandLeftPivot/HandLeft").GetComponent<SpriteRenderer>();
        m_handRightSprite = transform.parent.Find("HandRightPivot/HandRight").GetComponent<SpriteRenderer>();
        m_legLeftSprite = transform.parent.Find("LegLeftPivot/LegLeft").GetComponent<SpriteRenderer>();
        m_legRightSprite = transform.parent.Find("LegRightPivot/LegRight").GetComponent<SpriteRenderer>();

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
