using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// 対象のゲームオブジェクトを死亡させるマップイベント
    /// </summary>
    public sealed class DestroyGameObject : MapEvent
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
