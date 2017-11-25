using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Tank
{
    public partial class FormMain : Form
    {
        private GameState _gameState = GameState.Close;
        private ttank _myTank = new ttank(Side.Me);
        private int _myBulletCount;
        int cnt = 0;
        private List<ttank> _listEnemyTank = new List<ttank>();
        private List<Bullet> _listBullet = new List<Bullet>();
        [DllImport("User32"),]
        public static extern int GetAsyncKeyState(long vKey);

        public FormMain()
        {
            InitializeComponent();
        }

        private void MenuItemBegin_Click(object sender, EventArgs e)
        {
            cnt = 0;
            _gameState = GameState.Open;
            _myBulletCount = 0;
            _listEnemyTank.Clear();
            _listBullet.Clear();
            _myBulletCount = 0;
            pictureBox1.Invalidate();
            _myTank = new ttank(Side.Me);

            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;
            timer4.Enabled = true;
        }

        private void MenuItemEnd_Click(object sender, EventArgs e)
        {
            _gameState = GameState.Close;
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            _myTank.DrawMe(e.Graphics);
            for (int i = 0; i <= _listEnemyTank.Count - 1; i++)
                _listEnemyTank[i].DrawMe(e.Graphics);
            foreach (Bullet myBullet in _listBullet)
            {
                myBullet.DrawMe(e.Graphics);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (_gameState == GameState.Open)
            {
                //if (e.KeyCode == Keys.Up)
                //    _myTank.Move(Direction.Up);
                //else if (e.KeyCode == Keys.Down)
                //    _myTank.Move(Direction.Down);
                //else if (e.KeyCode == Keys.Left)
                //    _myTank.Move(Direction.Left);
                //else if (e.KeyCode == Keys.Right)
                //    _myTank.Move(Direction.Right);
                if (e.KeyCode == Keys.Space)
                {
                    if (_myBulletCount < 5)
                    {
                        Bullet myBullet = _myTank.Fire();
                        _listBullet.Add(myBullet);
                        _myBulletCount++;
                    }
                }
                pictureBox1.Invalidate();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_gameState == GameState.Open && cnt < 20 && _listEnemyTank.Count + 1 <= 20 - cnt)
            {
                ttank enemyTank = new ttank(Side.Enemy);
                _listEnemyTank.Add(enemyTank);
                pictureBox1.Invalidate();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (_gameState == GameState.Open)
            {
                Random myRand = new Random(DateTime.Now.Millisecond);
                for (int i = 0; i <= _listEnemyTank.Count - 1; i++)
                {
                    int randd = myRand.Next(1, 51);
                    ttank enemyTank = _listEnemyTank[i];
                    if (randd < 25)
                    {
                        if (Math.Abs(enemyTank._Positon.X - _myTank._Positon.X) <= 10)
                        {
                            if (enemyTank._Positon.Y < _myTank._Positon.Y)
                                enemyTank.Move(Direction.Down);
                            else enemyTank.Move(Direction.Up);
                            _listEnemyTank[i].Fire();
                        }
                        else if (Math.Abs(enemyTank._Positon.Y - _myTank._Positon.Y) <= 10)
                        {
                            if (enemyTank._Positon.X < _myTank._Positon.X)
                                enemyTank.Move(Direction.Right);
                            else enemyTank.Move(Direction.Left);
                            _listEnemyTank[i].Fire();
                        }
                        else
                        {
                            int newDirection = myRand.Next(1, 50);
                            if (newDirection <= 4)
                                _listEnemyTank[i].Move((Direction)newDirection);
                            else
                                _listEnemyTank[i].Move(_listEnemyTank[i]._Direction);
                        }
                    }
                    else
                    {
                        int newDirection = myRand.Next(1, 50);
                        if (newDirection <= 4)
                            _listEnemyTank[i].Move((Direction)newDirection);
                        else
                            _listEnemyTank[i].Move(_listEnemyTank[i]._Direction);
                    }
                    if (Math.Abs(enemyTank._Positon.X - _myTank._Positon.X) <= 20 && Math.Abs(enemyTank._Positon.Y - _myTank._Positon.Y) <= 20)
                    {
                        MenuItemEnd_Click(sender, e);
                        MessageBox.Show(this, "对不起，你输了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                for (int i = 0; i < _listBullet.Count; i++)
                {
                    Bullet myBullet = _listBullet[i];
                    myBullet.Move();
                    if (myBullet._Position.X < 0 || myBullet._Position.X > Screen.PrimaryScreen.Bounds.Width)
                    {
                        if (myBullet._Side == Side.Me)
                            _myBulletCount--;
                        _listBullet.RemoveAt(i);
                    }
                    else if (myBullet._Position.Y < 0 || myBullet._Position.Y > Screen.PrimaryScreen.Bounds.Height)
                    {
                        if (myBullet._Side == Side.Me)
                            _myBulletCount--;
                        _listBullet.RemoveAt(i);
                    }
                    else
                    {
                        if (myBullet._Side == Side.Enemy)
                        {
                            if (Math.Abs(myBullet._Position.X - _myTank._Positon.X) <= 20 && Math.Abs(myBullet._Position.Y - _myTank._Positon.Y) <= 20)
                            {
                                MenuItemEnd_Click(sender, e);
                                MessageBox.Show(this, "对不起，你输了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < _listBullet.Count; j++)
                            {
                                Bullet bullet = _listBullet[j];
                                if (bullet._Side == Side.Enemy)
                                {
                                    if (Math.Abs(myBullet._Position.X - bullet._Position.X) <= 10 && Math.Abs(myBullet._Position.Y - bullet._Position.Y) <= 10)
                                    {
                                        _listBullet.RemoveAt(i);
                                        if (i < j)
                                            _listBullet.RemoveAt(j - 1);
                                        else
                                        {
                                            _listBullet.RemoveAt(j);
                                            i--;
                                        }
                                        i--;
                                        _myBulletCount--;
                                        break;
                                    }
                                }
                            }
                            for (int j = 0; j < _listEnemyTank.Count; j++)
                            {
                                ttank enemyTank = _listEnemyTank[j];
                                if (Math.Abs(myBullet._Position.X - enemyTank._Positon.X) <= 20 && Math.Abs(myBullet._Position.Y - enemyTank._Positon.Y) <= 20)
                                {
                                    _listEnemyTank.RemoveAt(j);
                                    _listBullet.RemoveAt(i);
                                    i--;
                                    _myBulletCount--;
                                    cnt++;
                                    if (cnt == 20)
                                    {
                                        MenuItemEnd_Click(sender, e);
                                        MessageBox.Show(this, "恭喜，你赢了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                pictureBox1.Invalidate();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (_gameState == GameState.Open)
            {
                Random myRand = new Random(DateTime.Now.Millisecond);
                foreach (ttank enemyTank in _listEnemyTank)
                {
                    int fireFlag = myRand.Next(1, 10);
                    if (fireFlag <= 4)
                    {
                        Bullet enemyBullet = enemyTank.Fire();
                        _listBullet.Add(enemyBullet);
                    }
                }
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (_gameState == GameState.Open)
            {
                bool keyDown = (((ushort)GetAsyncKeyState((int)Keys.Down)) & 0xffff) != 0;
                bool keyUp = (((ushort)GetAsyncKeyState((int)Keys.Up)) & 0xffff) != 0;
                bool keyLeft = (((ushort)GetAsyncKeyState((int)Keys.Left)) & 0xffff) != 0;
                bool keyRight = (((ushort)GetAsyncKeyState((int)Keys.Right)) & 0xffff) != 0;
                if (keyDown == true)
                    _myTank.Move(Direction.Down);
                else if (keyUp == true)
                    _myTank.Move(Direction.Up);
                else if (keyLeft == true)
                    _myTank.Move(Direction.Left);
                else if (keyRight == true)
                    _myTank.Move(Direction.Right);
                pictureBox1.Invalidate();
            }
        }
    }
}
