using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Task
{
    /// <summary>
    /// 対象のゲームオブジェクトを死亡させるタスク
    /// </summary>
    public sealed class DestroyGameObject : TaskMonoBehavior
    {
        [SerializeField]
        private GameObject[] targets = null;

        public override void Invoke()
        {
            foreach(var g in this.targets)
            {
                Destroy(g);
            }
        }
    }
}
