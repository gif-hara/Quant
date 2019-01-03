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
        private Animator animator;

        void Awake()
        {
            this.animator = this.GetComponent<Animator>();
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        this.animator.SetTrigger("Attack");
                        Debug.Log("?");
                    }
                });
        }
    }
}
