using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Superhero : MonoBehaviour
{
    public RectBounds m_superheroArea;
    public RectBounds m_shoreLeftBounds;
    public RectBounds m_shoreRightBounds;
    public Water m_water;

    private Stack<Suicider> m_suiciders = new Stack<Suicider>();
    private bool m_isPLayingSwimmAnim;
    private bool m_isPLayingJumpAnim;

    public bool IsOnWater
    {
        get;
        private set;
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
    }

    void FixedUpdate()
    {
        Vector2 velocity = Velocity;    
        if (IsOnWater && velocity.y > 0.0f)
        {
            Dude.SetBobyPartsKinematic(false);
            DudeAnimator.ClearClip();
            m_isPLayingSwimmAnim = false;
            IsOnWater = false;
        }

        Vector2 position;
        float waterHeight;
        int waterStripIndex;

        position = transform.position;
        Vector2 prevPosition = position;

        if (IsOnWater)
        {
            waterStripIndex = m_water.GetWaterStripIndex(transform.position.x);
            waterHeight = m_water.GetWaterHeight(waterStripIndex);

            position = transform.position;
            position.y = waterHeight;
            position.x += velocity.x * Time.fixedDeltaTime;
            transform.position = position;
        }
        else
        {
            position += velocity * Time.fixedDeltaTime;
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

        Bounds shoreLeftBounds = m_shoreLeftBounds.GetBounds();
        if (position.x < shoreLeftBounds.max.x && position.y < shoreLeftBounds.max.y && velocity.y < 0 && prevPosition.y >= shoreLeftBounds.max.y)
        {
            position.y = shoreLeftBounds.max.y;
            velocity = Vector2.zero;
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
        }
        else if (position.x > shoreRightBounds.min.x && position.y < shoreRightBounds.max.y && velocity.x > 0 && prevPosition.y <= shoreRightBounds.max.y)
        {
            position.x = shoreRightBounds.min.x;
            velocity.x = -velocity.x * bounce_power;
        }

        waterStripIndex = m_water.GetWaterStripIndex(transform.position.x);
        waterHeight = m_water.GetWaterHeight(waterStripIndex);
        
        if (position.y <= waterHeight && !IsOnWater)
        {
            IsOnWater = true;
            m_water.Impulse(waterStripIndex, Velocity.magnitude * 0.4f);
            velocity.y = 0.0f;
            velocity.x = 0.0f;
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
        transform.position = position;
    }

    private void Update()
    {
    }

    void _LateUpdate()
    {
        if (!IsOnWater)
            return;

        int waterStripIndex = m_water.GetWaterStripIndex(transform.position.x);
        float waterHeight = m_water.GetWaterHeight(waterStripIndex);

        Vector3 position = transform.position;
        position.y = waterHeight;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Suicider suicider = other.gameObject.GetComponent<Suicider>();
        if (suicider != null && suicider.IsGrabable)
        {
            if (CanTakeSui())
            {
                AddSui(suicider);
                Dude.SetBobyPartsKinematic(true);
                DudeAnimator.SetupPivots();
                if (Dude.IsConnected(BodyPartType.HandLeft))
                    Dude.SetBobyPartKinematic(BodyPartType.HandLeft, false);
                if (Dude.IsConnected(BodyPartType.HandRight))
                    Dude.SetBobyPartKinematic(BodyPartType.HandRight, false);
            }
        }

        RescueArea rescueArea = other.gameObject.GetComponent<RescueArea>();
        if (rescueArea != null)
        {
            if (!IsFree())
                ReleaseSuiciders();
        }
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
        /*
        Vector3 suiDirection = (sui.transform.position - transform.position).normalized * 0.11f;

        sui.gameObject.transform.parent = m_suiContainer;
        sui.transform.position = transform.position + suiDirection;
        sui.SetController(new SuiControllerWithSuperhero(sui));
        */

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

        GameSettings.SuiRescuedCount += GetHoldingSuis();

        Vector2 rescuePosition = transform.position.x < 0.0f ?
            GameObject.Find("RescuePointLeft").transform.position :
            GameObject.Find("RescuePointRight").transform.position;

        while (m_suiciders.Count > 0)
        {
            Suicider sui = m_suiciders.Pop();
            sui.Dude.PlugOut();
            sui.SetController(new SuiControllerRescuing(sui, rescuePosition));
        }
    }
}
