using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
//Moudle 3
namespace Rhythm_Ocrosoft
{
    class DrKey
    {
        #region 私有字段
        private Point _position = new Point(100, 120);
        private string keyName;
        private int _moveSpeed = 10;
        Bitmap _keyPress = new Bitmap("Resources/Key.png");//按键图片
        #endregion

        #region 公有字段
        public string KeyName
        {
            get
            {
                return keyName;
            }

            set
            {
                keyName = value;
            }
        }

        public Point Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        public int MoveSpeed
        {
            get
            {
                return _moveSpeed;
            }

            set
            {
                _moveSpeed = value;
            }
        }
        #endregion
        //显示错误
        private void showError(string error)
        {
            try
            {
                MessageBox.Show(null, error + ",请联系Ocrosoft@Ocrosoft.com", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show(null, "错误显示失败,请联系Ocrosoft@Ocrosoft.com", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //0X3001
        public void Draw(Graphics g,Size size)
        {
            try
            {
                g.DrawImage(_keyPress, _position.X,_position.Y,size.Width,size.Height);
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X3001，ErrorInfo：" + exc);
            }
        }
        //移动，0X3002
        public void Move()
        {
            try
            {
                _position.Y += MoveSpeed;
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X3002，ErrorInfo：" + exc);
            }
        }
        //销毁，0X3999
        public void Dispose()
        {
            try
            {
                _keyPress.Dispose();
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X3999，ErrorInfo：" + exc);
            }
        }
    }
}
