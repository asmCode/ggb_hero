using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Superhero : MonoBehaviour
{
    public RectBounds m_superheroArea;
    public RectBounds m_shoreLeftBounds;
    public RectBounds m_shoreRightBounds;
    public RectBounds m_rescueAreaLeft;
    public RectBounds m_rescueAreaRight;
    public WaterLevel m_waterLevel;
    public Transform m_rescuePointLeft;
    public Transform m_rescuePointRight;
    public GrabEffectPool m_grabEffectPool;
    public Gameplay m_gameplay;

    public Transform m_survivalNamesLeft;
    public Transform m_survivalNamesRight;
    private RandomNames m_randomNames;
    public SurvivorNameManager m_survivorNameManager;

    private Stack<Suicider> m_suiciders = new Stack<Suicider>();
    private bool m_isPLayingSwimmAnim;
    private bool m_isPLayingJumpAnim;

    public bool IsInAir
    {
        get;
        private set;
    }

    public bool IsOnWater
    {
        get;
        private set;
    }

    public bool IsSwimming
    {
        get { return m_isPLayingSwimmAnim && Velocity.x != 0.0f; }
    }

    public Vector2 Velocity
    {
        get;
        set;
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

    public int GetHoldingSuis()
    {
        return m_suiciders.Count;
    }

    private void Awake()
    {
        Dude = GetComponent<Dude>();
        DudeAnimator = GetComponent<DudeAnimator>();

        m_randomNames = new RandomNames();
        m_randomNames.Initialize();
    }

    void Update()
    {
        Vector2 velocity = Velocity;
        if (IsOnWater && velocity.y > 0.0f)
        {
            Dude.SetBobyPartsKinematic(false);
            DudeAnimator.ClearClip();
            m_isPLayingSwimmAnim = false;
            IsOnWater = false;
            IsInAir = true;
        }

        if (velocity.y > 0)
            IsInAir = true;

        Vector2 position = transform.position;
        float waterHeight;
        int waterStripIndex;
        Vector2 prevPosition = position;

        if (IsOnWater)
        {
            waterHeight = m_waterLevel.GetWaterHeight(transform.position.x);

            position = transform.position;
            position.y = waterHeight;
            position.x += velocity.x * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            position += velocity * Time.deltaTime;
        }

        Bounds bounds = m_superheroArea.GetBounds();

        float bounce_power = 0.3f;

        if (position.x < bounds.min.x)
        {
            position.x = bounds.min.x;
            velocity.x = -velocity.x * bounce_power;
        }
        if (position.x > bounds.max.x)
        {
            position.x = bounds.max.x;
            velocity.x = -velocity.x * bounce_power;
        }
        if (position.y > bounds.max.y)
        {
            position.y = bounds.max.y;
            velocity.y = 0.0f;
        }

        if (m_rescueAreaLeft.IsPointInside(position) ||
            m_rescueAreaRight.IsPointInside(position))
        {
            NotifyCollisionWithRescueArea();
        }
        
        Bounds shoreLeftBounds = m_shoreLeftBounds.GetBounds();
        if (position.x < shoreLeftBounds.max.x && position.y < shoreLeftBounds.max.y && velocity.y < 0 && prevPosition.y >= shoreLeftBounds.max.y)
        {
            position.y = shoreLeftBounds.max.y;
            velocity = Vector2.zero;
            if (IsInAir)
            {
                AudioManager.GetInstance().SoundLand.Play();
                IsInAir = false;
            }
        }
        else if (position.x < shoreLeftBounds.max.x && position.y < shoreLeftBounds.max.y && velocity.x < 0 && prevPosition.y <= shoreLeftBounds.max.y)
        {
            position.x = shoreLeftBounds.max.x;
            velocity.x = -velocity.x * bounce_power;
        }

        Bounds shoreRightBounds = m_shoreRightBounds.GetBounds();
        if (position.x > shoreRightBounds.min.x && position.y < shoreRightBounds.max.y && velocity.y < 0 && prevPosition.y >= shoreRightBounds.max.y)
        {
            position.y = shoreRightBounds.max.y;
            velocity = Vector2.zero;
            if (IsInAir)
            {
                AudioManager.GetInstance().SoundLand.Play();
                IsInAir = false;
            }
        }
        else if (position.x > shoreRightBounds.min.x && position.y < shoreRightBounds.max.y && velocity.x > 0 && prevPosition.y <= shoreRightBounds.max.y)
        {
            position.x = shoreRightBounds.min.x;
            velocity.x = -velocity.x * bounce_power;
        }

        waterHeight = m_waterLevel.GetWaterHeight(transform.position.x);

        if (position.y <= waterHeight && !IsOnWater)
        {
            IsOnWater = true;
            IsInAir = false;
            // TODO, water circles
            // m_water.Impulse(waterStripIndex, Mathf.Min(3.0f, Velocity.magnitude), position.x);
            velocity.y = 0.0f;
            velocity.x = 0.0f;
        }

        if (IsOnWater && velocity.x != 0.0f)
        {
            if (!AudioManager.GetInstance().SoundSwim.IsPlaying())
                AudioManager.GetInstance().SoundSwim.Play();
        }
        else
        {
            if (AudioManager.GetInstance().SoundSwim.IsPlaying())
                AudioManager.GetInstance().SoundSwim.Stop();
        }

        if (IsOnWater && !m_isPLayingSwimmAnim)
        {
            Dude.SetBobyPartsKinematic(true);
            if (Dude.IsConnected(BodyPartType.HandLeft))
                Dude.SetBobyPartKinematic(BodyPartType.HandLeft, false);
            if (Dude.IsConnected(BodyPartType.HandRight))
                Dude.SetBobyPartKinematic(BodyPartType.HandRight, false);
            DudeAnimator.Swim();
            m_isPLayingSwimmAnim = true;
            m_isPLayingJumpAnim = false;
        }

        if (!IsOnWater && !m_isPLayingJumpAnim)
        {
            Dude.SetBobyPartsKinematic(true);
            if (Dude.IsConnected(BodyPartType.HandLeft))
                Dude.SetBobyPartKinematic(BodyPartType.HandLeft, false);
            if (Dude.IsConnected(BodyPartType.HandRight))
                Dude.SetBobyPartKinematic(BodyPartType.HandRight, false);
            DudeAnimator.Jump();
            m_isPLayingJumpAnim = true;
        }

        Velocity = velocity;
        transform.position = new Vector3(position.x, position.y, -0.3f);
    }

    public void NotifyCollisionWithSui(Suicider sui)
    {
        if (sui != null && sui.IsGrabable)
        {
            if (CanTakeSui())
            {
                AddSui(sui);
                Dude.SetBobyPartsKinematic(true);
                DudeAnimator.SetupPivots();
                if (Dude.IsConnected(BodyPartType.HandLeft))
                    Dude.SetBobyPartKinematic(BodyPartType.HandLeft, false);
                if (Dude.IsConnected(BodyPartType.HandRight))
                    Dude.SetBobyPartKinematic(BodyPartType.HandRight, false);

                AudioManager.GetInstance().SoundCatch.Play();
            }
        }
    }

    public void NotifyCollisionWithRescueArea()
    {
        if (!IsFree())
            ReleaseSuiciders();
    }

    private bool IsFree()
    {
        return m_suiciders.Count == 0;
    }

    private bool CanTakeSui()
    {
        return GetHoldingSuis() < GameSettings.HandCapacity;
    }

    private void AddSui(Suicider sui)
    {
        GrabEffect grabEffect = m_grabEffectPool.Get();
        if (grabEffect != null)
        {
            grabEffect.transform.position = sui.transform.position;
            grabEffect.Play();
        }

        sui.IsKinematic = false;
        sui.Dude.SetBobyPartsKinematic(false);
        sui.DudeAnimator.ClearClip();
        sui.Dude.PlugIn(Dude);
        sui.SetController(new SuiControllerWithSuperhero(sui));

        if (m_suiciders.Count > 0)
        {
            Dude dude = m_suiciders.Peek().Dude;
            dude.PlugOut();
            dude.PlugIn(sui.Dude);
        }
        
        m_suiciders.Push(sui);
    }

    private void ReleaseSuiciders()
    {
        Dude.SetBobyPartsKinematic(true);
        DudeAnimator.SetupPivots();

        int total = GGHeroGame.GetTotal();
        int totalBefore = total + GameSettings.SuiRescuedCount;
        GameSettings.SuiRescuedCount += GetHoldingSuis();
        int totalAfter = total + GameSettings.SuiRescuedCount;
        m_gameplay.ReportAchievementIfNeeded(totalBefore, totalAfter);

        Vector2 rescuePosition = transform.position.x < 0.0f ?
            m_rescuePointLeft.position :
            m_rescuePointRight.position;

        while (m_suiciders.Count > 0)
        {
            Suicider sui = m_suiciders.Pop();
            ShowSurvivalName(sui);
            sui.Dude.PlugOut();
            sui.SetController(new SuiControllerRescuing(sui, rescuePosition));
        }
    }

    private void ShowSurvivalName(Suicider sui)
    {
        if (sui == null)
            return;

        Transform container = sui.transform.position.x < 0.0f ? m_survivalNamesLeft : m_survivalNamesRight;

        m_survivorNameManager.SetName(m_randomNames.GetName(sui.IsFemale), sui.TintColor, sui.transform.position, container);
    }
}
