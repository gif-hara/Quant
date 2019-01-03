using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// <see cref="GameObject"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 子オブジェクトも含めたレイヤーを設定する
        /// </summary>
        public static void SetLayerRecursive(this GameObject self, Layers.Id layer)
        {
            self.layer = (int)layer;
            for (var i = 0; i < self.transform.childCount; i++)
            {
                self.transform.GetChild(i).gameObject.SetLayerRecursive(layer);
            }
        }
    }
}
