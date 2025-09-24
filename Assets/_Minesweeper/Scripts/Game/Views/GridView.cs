using UnityEngine;
using Utils;

namespace Game
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] 
        private Transform cellsSpawnTransform;
        [SerializeField] 
        private FlexibleGridLayout flexibleGridLayout;

        public Transform CellsSpawnTransform => cellsSpawnTransform;

        public void SetGridRows(int rowsCount) => flexibleGridLayout.ResizeByRows(rowsCount);
        public void ClearGrid() => flexibleGridLayout.ClearLayoutElements();
    }
}
