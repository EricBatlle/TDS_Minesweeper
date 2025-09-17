using System;
using TriInspector;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Cell
    {
        [ShowInInspector]
        public CellState State { get; set; }
        [ShowInInspector]
        public bool HasBomb { get; private set; }
        [ShowInInspector]
        public Vector2Int Position { get; private set; }

        public Cell(CellState state, bool hasBomb, Vector2Int position)
        {
            State = state;
            HasBomb = hasBomb;
            Position = position;
        }
    }
}
