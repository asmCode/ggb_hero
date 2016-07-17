using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    public Transform m_healthBarPosition;

    private HealthBar m_healthBar;
    private Rigidbody2D m_rigibody;
    private SpriteRenderer m_bodySprite;
    private SpriteRenderer m_headSprite;
    private SpriteRenderer m_handLeftSprite;
    private SpriteRenderer m_handRightSprite;
    private SpriteRenderer m_fistRightSprite;
    private SpriteRenderer m_legLeftSprite;
    private SpriteRenderer m_legRightSprite;
    private SpriteRenderer m_fistLeftSprite;

    public Color TintColor
    {
        set
        {
            m_bodySprite.color = value;
            m_handLeftSprite.color = value;
            m_handRightSprite.color = value;
        }
    }

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
            m_headSprite.sortingOrder = value + 2;
            m_handLeftSprite.sortingOrder = value + 1;
            m_handRightSprite.sortingOrder = value + 1;
            m_fistRightSprite.sortingOrder = value + 1;
            m_legLeftSprite.sortingOrder = value + 1;
            m_legRightSprite.sortingOrder = value + 1;
            m_fistLeftSprite.sortingOrder = value + 1;
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

    public void SetHealthBarVisible(bool visible)
    {
        if (visible)
        {
            if (m_healthBar != null)
                return;

            m_healthBar = HealthBarPool.Instance.Get();
            UptadeHealthBarPosition();
        }
        else
        {
            if (m_healthBar == null)
                return;

            m_healthBar.gameObject.SetActive(false);
            m_healthBar = null;
        }
    }

    public void SetHealthValue(float health)
    {
        if (m_healthBar == null)
            return;

        m_healthBar.SetHealth(health);
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
        m_fistRightSprite = transform.parent.Find("HandRightPivot/HandRight/SuiFist").GetComponent<SpriteRenderer>();
        m_fistLeftSprite = transform.parent.Find("HandLeftPivot/HandLeft/SuiFist").GetComponent<SpriteRenderer>();
        m_legLeftSprite = transform.parent.Find("LegLeftPivot/LegLeft").GetComponent<SpriteRenderer>();
        m_legRightSprite = transform.parent.Find("LegRightPivot/LegRight").GetComponent<SpriteRenderer>();

        SetController(new SuiControllerIdleTest(this));
    }

    void Update()
    {
        UptadeHealthBarPosition();
    }

    private void UptadeHealthBarPosition()
    {
        if (m_healthBar != null)
        {
            m_healthBar.transform.OverlayPosition(m_healthBarPosition);
        }
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
        if (SuiController != null)
            SuiController.ProcessTriggerEnter2D(other);
    }
}
