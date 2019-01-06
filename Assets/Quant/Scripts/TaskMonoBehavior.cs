using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// <see cref="MonoBehaviour"/>を継承した<see cref="Task"/>
    /// </summary>
    public abstract class TaskMonoBehavior : MonoBehaviour, Task
    {
        public abstract void Invoke();
    }
}
