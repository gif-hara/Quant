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

        void Awake()
        {
            this.TransformController = this.GetComponent<ActorTransformController>();
            this.AnimationController = this.GetComponent<ActorAnimationController>();
        }
    }
}
