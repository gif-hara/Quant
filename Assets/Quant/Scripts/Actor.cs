using Quant.Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// ゲーム中に使われるキャラクターにアタッチされるコンポーネント
    /// </summary>
    public sealed class Actor : MonoBehaviour
    {
        public ActorTransformController TransformController { get; private set; }

        public ActorAnimationController AnimationController { get; private set; }

        public ActorStatusController StatusController { get; private set; }

        public IMessageBroker Broker { get; private set; }

        public Transform CachedTransform { get; private set; }

        void Awake()
        {
            this.Broker = new MessageBroker();
            this.TransformController = this.GetComponent<ActorTransformController>();
            this.CachedTransform = this.transform;
            ActorEffectController.Apply(this);
        }

        public void Setup(ActorStatus status)
        {
            var model = status.Model.Spawn(this.CachedTransform.position, this.CachedTransform.rotation);
            model.transform.SetParent(this.CachedTransform);
            this.StatusController = new ActorStatusController(this, status);
            this.AnimationController = new ActorAnimationController(model.GetComponent<Animator>(), this.CachedTransform);
        }

        public void ForceDead()
        {
            if(this.StatusController.IsDead)
            {
                return;
            }

            Destroy(this.gameObject);
            this.Broker.Publish(DiedActor.Get());
        }
    }
}
