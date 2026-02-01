using UnityEngine;

public class BoardView : MonoBehaviour {
    #region Fields

    [SerializeField] private Transform m_cellsRoot;
    [SerializeField] private CellView m_cellPrefab;

    [SerializeField] private int m_sizeX = 10;
    [SerializeField] private int m_sizeY = 10;

    [SerializeField] private float m_cellSize = 1f;
    [SerializeField] private float m_spacing = 0.05f;

    [SerializeField] private Vector2 m_origin = Vector2.zero;

    #endregion

    #region Public

    public int SizeX {
        get {
            return m_sizeX;
        }
    }
    public int SizeY {
        get {
            return m_sizeY;
        }
    }

    public float CellSize {
        get {
            return m_cellSize;
        }
    }
    public float Spacing {
        get {
            return m_spacing;
        }
    }

    public Vector2 Origin {
        get {
            return m_origin;
        }
    }

    public Transform CellsRoot {
        get {
            return m_cellsRoot;
        }
    }
    public CellView CellPrefab {
        get {
            return m_cellPrefab;
        }
    }

    #endregion
}
