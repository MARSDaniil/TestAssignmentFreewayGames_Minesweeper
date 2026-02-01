using System;
using UnityEngine;

public interface IBoardService {
    #region Public

    int SizeX {
        get;
    }
    int SizeY {
        get;
    }

    event Action<Vector2Int> e_onCellChangedEvent;
    event Action e_onGameLostEvent;
    event Action e_onGameWinEvent;

    void CreateNewBoard();
    void OpenCell(Vector2Int a_position);
    void ToggleFlag(Vector2Int a_position);

    BoardCell GetCell(Vector2Int a_position);

    #endregion
}
