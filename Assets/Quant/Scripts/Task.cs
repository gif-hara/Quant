using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 様々な処理を実行するための基底クラス
    /// </summary>
    public interface Task
    {
        /// <summary>
        /// タスクを実行する
        /// </summary>
        void Invoke();
    }
}
