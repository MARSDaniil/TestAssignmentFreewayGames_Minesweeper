public class WrapIntSelector {
    #region Fields

    private readonly int m_minValue;
    private readonly int m_maxValue;

    private int m_value;

    #endregion

    #region Public

    public int Value {
        get {
            return m_value;
        }
    }

    public WrapIntSelector(int a_startValue, int a_minValue, int a_maxValue) {
        m_minValue = a_minValue;
        m_maxValue = a_maxValue;

        m_value = Clamp(a_startValue);
    }

    public int Increase() {
        int next = m_value + 1;

        if (next > m_maxValue) {
            next = m_minValue;
        }

        m_value = next;
        return m_value;
    }

    public int Decrease() {
        int next = m_value - 1;

        if (next < m_minValue) {
            next = m_maxValue;
        }

        m_value = next;
        return m_value;
    }

    public void SetValue(int a_value) {
        m_value = Clamp(a_value);
    }

    #endregion

    #region Private

    private int Clamp(int a_value) {
        if (a_value < m_minValue) {
            return m_minValue;
        }

        if (a_value > m_maxValue) {
            return m_maxValue;
        }

        return a_value;
    }

    #endregion
}
