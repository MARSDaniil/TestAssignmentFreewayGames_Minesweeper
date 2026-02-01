using UnityEngine;

public class BoardCameraController {
    #region Fields

    private readonly Camera m_camera;
    private readonly BoardView m_boardView;
    private readonly IBoardService m_boardService;

    private readonly float m_padding;

    #endregion

    #region Public

    public BoardCameraController(
        Camera a_camera,
        BoardView a_boardView,
        IBoardService a_boardService,
        float a_padding = 0.5f) {
        m_camera = a_camera;
        m_boardView = a_boardView;
        m_boardService = a_boardService;
        m_padding = Mathf.Max(0f, a_padding);
    }

    public void FitToBoard() {
        if (m_camera == null) {
            return;
        }

        if (!m_camera.orthographic) {
            return;
        }

        int sizeX = Mathf.Max(1, m_boardService.SizeX);
        int sizeY = Mathf.Max(1, m_boardService.SizeY);

        float step = m_boardView.CellSize + m_boardView.Spacing;

        float boardWidth = ((sizeX - 1) * step) + m_boardView.CellSize;
        float boardHeight = ((sizeY - 1) * step) + m_boardView.CellSize;

        Vector3 camPos = m_camera.transform.position;
        camPos.x = m_boardView.Origin.x;
        camPos.y = m_boardView.Origin.y;
        m_camera.transform.position = camPos;

        float halfHeight = (boardHeight * 0.5f) + m_padding;
        float halfWidth = (boardWidth * 0.5f) + m_padding;

        float aspect = Mathf.Max(0.0001f, m_camera.aspect);

        float sizeByHeight = halfHeight;
        float sizeByWidth = halfWidth / aspect;

        m_camera.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth);
    }


    #endregion
}
