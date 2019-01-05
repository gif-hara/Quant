using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.AIControllers
{
    /// <summary>
    /// 次のAIへ遷移可能か判断する基底クラス
    /// </summary>
    public abstract class AICondition : ScriptableObject
    {
        /// <summary>
        /// 評価する
        /// </summary>
        public abstract bool Evalute(Actor owner);
    }
}
