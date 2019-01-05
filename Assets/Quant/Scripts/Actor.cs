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
            this.AnimationController = this.GetComponent<ActorAnimationController>();
            this.CachedTransform = this.transform;
        }

        public void Setup(ActorStatus status)
        {
            this.StatusController = new ActorStatusController(this, status);
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
