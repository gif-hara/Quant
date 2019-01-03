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
        public Animator Animator { get; private set; }

        public ActorTransformController TransformController { get; private set; }

        void Awake()
        {
            this.Animator = this.GetComponent<Animator>();
            this.TransformController = this.GetComponent<ActorTransformController>();
        }
    }
}
