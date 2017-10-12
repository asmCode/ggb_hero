using UnityEngine;
using System.Collections;
using System;

public class Suicider : MonoBehaviour
{
    public SuiciderRoot m_root;
    public Transform m_healthBarPosition;
    public Sprite m_maleHead;
    public Sprite m_femaleHead;

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
    public WaterWaver m_waterWaiver = new WaterWaver();

    public bool IsFemale
    {
        get;
        private set;
    }

    public Color TintColor
    {
        get
        {
            return m_bodySprite.color;
        }

        set
        {
            m_bodySprite.color = value;
            m_handLeftSprite.color = value;
            m_handRightSprite.color = value;
        }
    }

    internal void SetOpacity(float v)
    {
        SetOpacity(m_bodySprite, v);
        SetOpacity(m_headSprite, v);
        SetOpacity(m_handLeftSprite, v);
        SetOpacity(m_handRightSprite, v);
        SetOpacity(m_fistRightSprite, v);
        SetOpacity(m_legLeftSprite, v);
        SetOpacity(m_legRightSprite, v);
        SetOpacity(m_fistLeftSprite, v);
    }

    private void SetOpacity(SpriteRenderer sprite, float opacity)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, opacity);
    }

    public Color SkinTintColor
    {
        set
        {
            m_headSprite.color = value;
            m_fistRightSprite.color = value;
            m_fistLeftSprite.color = value;
        }
    }

    public SuiController SuiController
    {
        get;
        private set;
    }

    public WaterLevel WaterLevel
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
        set
        {
            m_rigibody.isKinematic = value;
            m_rigibody.simulated = !value;
        }
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

    public void Initialize(WaterLevel waterLevel)
    {
        WaterLevel = waterLevel;
    }

    public void Reset()
    {
        DudeAnimator.Reset();
    }

    public void SetIsFemale(bool isFemale)
    {
        IsFemale = isFemale;
        m_headSprite.sprite = isFemale ? m_femaleHead : m_maleHead;
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

        IsKinematic = false;
        Dude.SetBobyPartsKinematic(false);
        DudeAnimator.ClearClip();

        m_root.gameObject.SetActive(false);
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

    public WaterCircles WaterCircles
    {
        get; set;
    }

    void Update()
    {
        UptadeHealthBarPosition();

        SuiController.UpdateSui();
    }

    private void UptadeHealthBarPosition()
    {
        if (m_healthBar != null)
        {
            m_healthBar.transform.OverlayPosition(m_healthBarPosition);
        }
    }

    void LateUpdate()
    {
        SuiController.LateUpdateSui();

        Vector3 position = transform.position;
        position.z = -0.3f;
        transform.position = position;
    }

    public void NotifyCollisionWithDestroyArea()
    {
        if (SuiController != null)
            SuiController.NotifyCollisionWithDestroyArea();
    }
}
