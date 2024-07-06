using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimator : MonoBehaviour
{
    private Animator        animator;
    private readonly int    deactivateAnimations = -1;
    private readonly int    isIddle = 0;
    private readonly int    isIddleHappy = 1;
    private readonly int    isWalkingNormal = 2;
    private readonly int    isWalkingHappy = 3;
    private readonly int    isRunning = 4;
    private readonly int    isEating = 5;
    private readonly int    isAngry = 6;
    private readonly int    isSitting = 7;
    private readonly string animationId = "AnimationID";
    private readonly string sittingParameter = "IsSitting";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Activa o desactiva la animacion de estar inactivo.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    public void Idle(bool activate)
    {
        ActivateAnimationInteger(activate, animationId, isIddle);
    }

    /// <summary>
    /// Activa o desactiva la animacion de estar inactivo y feliz.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    public void IdleHappy(bool activate)
    {
        ActivateAnimationInteger(activate, animationId, isIddleHappy);
    }

    /// <summary>
    /// Activa o desactiva la animacion de caminar normalmente.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    public void WalkingNormal(bool activate)
    {
        ActivateAnimationInteger(activate, animationId, isWalkingNormal);
    }

    /// <summary>
    /// Activa o desactiva la animacion de caminar feliz.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    public void WalkingHappy(bool activate)
    {
        ActivateAnimationInteger(activate, animationId, isWalkingHappy);
    }

    /// <summary>
    /// Activa o desactiva la animacion de correr.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    public void Running(bool activate)
    {
        ActivateAnimationInteger(activate, animationId, isRunning);
    }

    /// <summary>
    /// Activa o desactiva la animacion de comer.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    public void Eating(bool activate)
    {
        ActivateAnimationInteger(activate, animationId, isEating);
    }

    /// <summary>
    /// Activa o desactiva la animacion de estar enojado.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    public void Angry(bool activate)
    {
        ActivateAnimationInteger(activate, animationId, isAngry);
    }

    /// <summary>
    /// Activa o desactiva la animacion de estar sentado.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    /// <param name="needsToMove">Indica si el personaje necesita moverse antes de sentarse.</param>
    public void Sitting(bool activate, bool needsToMove)
    {
        ActivateAnimationInteger(activate, animationId, isSitting);
        animator.SetBool(sittingParameter, !needsToMove);
    }

    /// <summary>
    /// Activa o desactiva una animacion especifica estableciendo un entero en el Animator.
    /// </summary>
    /// <param name="activate">Indica si se debe activar o desactivar la animacion.</param>
    /// <param name="animationId">El identificador de la animacion en el Animator.</param>
    /// <param name="animationState">El estado entero que corresponde a la animacion a activar.</param>
    private void ActivateAnimationInteger(bool activate, string animationId, int animationState)
    {
        if (activate)
        {
            animator.SetInteger(animationId, animationState);
        }
        else
        {
            animator.SetInteger(animationId, deactivateAnimations);
        }
    }
}
