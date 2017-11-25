using System;

namespace Rhythm_Ocrosoft
{
    class MyKey
    {
        private int _min;
        private int _sec;
        private int _ms;
        private int _count;
        private bool[] _key = new bool[9];

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
