using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BoardConfig")]
public class BoardConfig : ScriptableObject {
    #region Fields

    public int sizeX = 10;
    public int sizeY = 10;
    public int minesCount = 10;

    [SerializeField] private List<NumColorsAdjacent> m_numColorsAdjacents;

    public Color GetdjacentsColor(byte a_num) {

        foreach (NumColorsAdjacent numColorsAdjacent in m_numColorsAdjacents) {
            if (a_num == numColorsAdjacent.num) {
                return numColorsAdjacent.color;
            }
        }

        return Color.black;
    }


    #endregion
}

[Serializable]
public struct NumColorsAdjacent {
    public byte num;
    public Color color;
}