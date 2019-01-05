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
        /// <summary>
        /// AIが起動した際の処理
        /// </summary>
        public abstract void Enter(Actor owner, CompositeDisposable disposables);

        /// <summary>
        /// AIが終了した際の処理
        /// </summary>
        public abstract void Exit();
    }
}
