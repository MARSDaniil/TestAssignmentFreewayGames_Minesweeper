using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BoardConfig")]
public class BoardConfig : ScriptableObject {
    #region Fields

    public int sizeX = 10;
    public int sizeY = 10;
    public int minesCount = 10;

    #endregion
}
