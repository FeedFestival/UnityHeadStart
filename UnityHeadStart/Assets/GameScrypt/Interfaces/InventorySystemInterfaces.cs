using UnityEngine.Events;

namespace GameScrypt.Interfaces
{
    public interface IInventorySystem
    {
        public void Init();
    }

    public interface IItem
    {
        public string GetName();
    }
}