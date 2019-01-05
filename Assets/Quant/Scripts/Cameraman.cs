using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// カメラを制御するクラス
    /// </summary>
    public sealed class Cameraman : MonoBehaviour
    {
        [SerializeField]
        private Transform root;

        [SerializeField]
        private Transform pivot;

        [SerializeField]
        private Transform distance;

        [SerializeField]
        private Transform rig;

        [SerializeField]
        private Camera controllerdCamera;

        public static Cameraman Instance { get; private set; }

        public Vector3 Position { get { return this.root.position; } set { this.root.position = value; } }

        public Quaternion Pivot { get { return this.pivot.localRotation; } set { this.pivot.localRotation = value; } }

        public float Distance { get { return Mathf.Abs(this.distance.localPosition.z); } set { this.distance.localPosition = new Vector3(0.0f, 0.0f, -Mathf.Abs(value)); } }

        public Quaternion Rig { get { return this.rig.localRotation; } set { this.rig.localRotation = value; } }

        public Camera Camera => this.controllerdCamera;

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }

        private void OnDestroy()
        {
            Assert.IsNotNull(Instance);
            Instance = null;
        }
    }
}
