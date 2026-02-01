using UnityEngine;

public class BoardView : MonoBehaviour {
    #region Fields

    [SerializeField] private Transform m_cellsRoot;
    [SerializeField] private int m_sizeX = 10;
    [SerializeField] private int m_sizeY = 10;
    [SerializeField] private CellView m_cellPrefab;

    #endregion

    #region Public

    public int SizeX => m_sizeX;
    public int SizeY => m_sizeY;
    public Transform CellsRoot => m_cellsRoot;
    public CellView CellPrefab => m_cellPrefab;

    #endregion
}
