using System;
using System.Drawing;
using GTA;

namespace Inferno
{
    /// <summary>
    /// インフェルノスクリプトで使うUIの行要素
    /// </summary>
    public class InfernoUIRow
    {
        /// <summary>
        /// 選択された時に実行するAction
        /// </summary>
        private Action _action;

        /// <summary>
        /// 表示文字列
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 文字色
        /// </summary>
        public Color TextColor { get; private set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackGroundColor { get; private set; }

        /// <summary>
        /// フォント？
        /// </summary>
        public int Font { get; private set; }

        /// <summary>
        /// 選択可能か
        /// </summary>
        public bool IsSelectable { get; private set; }


        /// <summary>
        /// UI行
        /// </summary>
        /// <param name="text">表示文字</param>
        /// <param name="textColor">テキストの色</param>
        /// <param name="font">フォント(?)</param>
        /// <param name="isSelectable">選択可能か</param>
        /// <param name="action">決定時に実行するメソッド</param>
        public InfernoUIRow(String text, Color textColor,Color backgroundColor, int font,bool isSelectable, Action action)
        {
            _action = action;
            Text = text;
            TextColor = textColor;
            BackGroundColor = backgroundColor;
            Font = font;
            IsSelectable = isSelectable;
        }

        /// <summary>
        /// 決定
        /// </summary>
        public void Decide()
        {
            _action();
        }

 
    }
}
