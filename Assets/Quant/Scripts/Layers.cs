using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    public static class Layers
    {
        public enum Id
        {
            Player = 9,
            Player_Bullet = 10,
            Enemy = 11,
            Enemy_Bullet = 12,
        }

        public enum Mask
        {
            Player = 1 << 9,
            Player_Bullet = 1 << 10,
            Enemy = 1 << 11,
            Enemy_Bullet = 1 << 12,
        }

        public static Id GetBulletLayerId(Id layer)
        {
            if(layer == Id.Player)
            {
                return Id.Player_Bullet;
            }
            if(layer == Id.Enemy)
            {
                return Id.Enemy_Bullet;
            }

            Assert.IsTrue(false, $"layerId = {layer} は未対応の値です");
            return Id.Player_Bullet;
        }
    }
}
