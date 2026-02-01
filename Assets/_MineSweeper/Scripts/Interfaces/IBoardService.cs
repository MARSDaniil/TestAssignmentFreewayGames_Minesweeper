using System;
using UnityEngine;

public interface IBoardService {
    #region Public
    int MinesCount {
        get;
    }
    int FlagsPlaced {
        get;
    }

    int SizeX {
        get;
    }
    int SizeY {
        get;
    }

    event Action<Vector2Int> e_onCellChangedEvent;
    event Action e_onGameLostEvent;
    event Action e_onGameWinEvent;
    event Action<int> e_onFlagsChangedEvent;
    event Action e_onFirstCellOpenedEvent;
    void CreateNewBoard();
    void OpenCell(Vector2Int a_position);
    void ToggleFlag(Vector2Int a_position);

    BoardCell GetCell(Vector2Int a_position);

    #endregion
}
