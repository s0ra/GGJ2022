using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class ActorRuntime : LevelObjectRuntime
{
    public class ActorAnimator : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        protected ActorRuntime _actorRuntime;

        public virtual void Init(ActorRuntime actorRuntime)
        {
            _actorRuntime = actorRuntime;
        }

        public virtual void UpdateAnimator()
        {

        }

        public virtual void DestroySelf()
        {

        }
    }
}
