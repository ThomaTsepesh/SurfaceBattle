using UnityEngine;

namespace SB
{
    public class UnitData : MonoBehaviour
    {
        public Unit Data { get; private set; }

        public void Init(Unit model) 
        {
            Data = model;
        }
    }
}