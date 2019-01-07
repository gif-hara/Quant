using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.AIControllers
{
    /// <summary>
    /// AIの基底クラス
    /// </summary>
    public abstract class AIElement : ScriptableObject
    {
        public abstract AIElement Clone { get; }

        /// <summary>
        /// AIが起動した際の処理
        /// </summary>
        public abstract void Enter(Actor owner, CompositeDisposable disposables);

        /// <summary>
        /// AIが終了した際の処理
        /// </summary>
        public virtual void Exit()
        {
        }

        public virtual bool CanExit => true;
    }
}
