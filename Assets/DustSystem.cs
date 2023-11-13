using UnityEngine;

public class DustSystem : MonoBehaviour
{
    private ParticleSystem _dust;
    private ParticleSystem.VelocityOverLifetimeModule _velocityOvTime;
    private ParticleSystem.SizeOverLifetimeModule _sizeOverLifetime;
    private ParticleSystemRenderer _dustRenderer;
    public bool isGrounding;

    private void Awake()
    {
        _dust = GetComponent<ParticleSystem>();
        _sizeOverLifetime = _dust.sizeOverLifetime;
        _velocityOvTime = _dust.velocityOverLifetime;
        _dustRenderer = _dust.GetComponent<ParticleSystemRenderer>();
    }

    private void Update()
    {
        GroundDust();
        FlipDust();
        WallJumpDust();
    }

    private void GroundDust()
    {
        //Send to AnimationController
        if (!isGrounding) return;
        isGrounding = false;

        //Dust settings
        _velocityOvTime.x = -3.28f;
        _velocityOvTime.y = 3.93f;
        _sizeOverLifetime.sizeMultiplier = 1.5f;
        _dustRenderer.sortingLayerName = "UI_1"; 

        //Play
        _dust.Play();
    }

    private void FlipDust()
    {
        //When flipping
        if (!BasicPlayerMovement.isFlippinOnGround) return;
        BasicPlayerMovement.isFlippinOnGround = false;

        //Dust settings
        _velocityOvTime.x = -0.28f;
        _velocityOvTime.y = 3.93f;
        _sizeOverLifetime.sizeMultiplier = 1;
        _dustRenderer.sortingLayerName = "Default";

        //Play
        _dust.Play();
    }
    
    private void WallJumpDust()
    {
        //When wall jumping
        if (!BasicPlayerMovement.isWallJumpin) return;
        BasicPlayerMovement.isWallJumpin = false;
        
        //Dust settings
        _velocityOvTime.x = -0.28f;
        _velocityOvTime.y = 3.93f;
        _sizeOverLifetime.sizeMultiplier = 1;
        _dustRenderer.sortingLayerName = "Default";

        //Play
        _dust.Play();
    }
}