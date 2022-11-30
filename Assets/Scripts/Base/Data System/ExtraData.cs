namespace Base.DataSystem
{
    [System.Serializable]
    public struct ExtraData
    {
        public string DataName;
        public int Value;

        public ExtraData(string _dataName, int _value)
        {
            DataName = _dataName;
            Value = _value;
        }
    
        public void SetValue(int value)
        {
            Value = value;
        }
    }
}