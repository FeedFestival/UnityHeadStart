using GameScrypt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameScrypt.GSInteractSystem
{
    public class GSInteractableDefinition : MonoBehaviour, IInteractableDefinition
    {
        [SerializeField]
        public Dictionary<object, IInteractable> Interactables { get; set; }
        public List<GSInteractable> TrivialInteractable;

        public virtual void Init(IInteractSystemController interactSystemController)
        {
            Debug.LogError("You should extend this class and not use it directly.");
            throw new NotImplementedException();
        }
    }
}
