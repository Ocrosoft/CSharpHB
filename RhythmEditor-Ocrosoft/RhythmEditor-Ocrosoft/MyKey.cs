namespace RhythmEditor_Ocrosoft
{
    class MyKey
    {
        private int _min;//分
        private int _sec;//秒
        private int _ms;//毫秒
        private int _count;//总数
        private bool[] _key = new bool[9];//记录key
        #region 公有字段
        public int Min
        {
            get
            {
                return _min;
            }

            set
            {
                _min = value;
            }
        }

        public int Sec
        {
            get
            {
                return _sec;
            }

            set
            {
                _sec = value;
            }
        }

        public int Ms
        {
            get
            {
                return _ms;
            }

            set
            {
                _ms = value;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }

            set
            {
                _count = value;
            }
        }

        public bool[] Key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
            }
        }
        #endregion
    }
}
