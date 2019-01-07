using UnityEngine;

public class LevelIntroAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        CodeControl.Message.AddListener<LevelIntroRequestEvent>(OnLevelIntroRequested);
        animator = GetComponent<Animator>();
    }

    private void OnLevelIntroRequested(LevelIntroRequestEvent obj)
    {
        animator.SetTrigger(obj.color.ToString()+"Intro");
    }
}
