using UnityEngine;

public class AnimatorComtroller : MonoBehaviour
{
private Animator animator;
private Unit moviment;
private const string IS_WALKING = "IsWalking";

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        moviment = gameObject.GetComponentInParent<Unit>();
    }
    void Start()
    {
        moviment.GetActionMove().OnWalkingValue += AnimationWalkingController;
    }

    private void AnimationWalkingController(Vector3 moveDir)
    {
        if(moveDir != Vector3.zero)animator.SetBool(IS_WALKING, true);
        else animator.SetBool(IS_WALKING, false);
    }
}
