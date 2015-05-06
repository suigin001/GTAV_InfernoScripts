using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GTA;
using Reactive.Bindings;
using Color = System.Drawing.Color;

namespace Inferno
{
    /// <summary>
    /// インフェルノスクリプトに置けるUI表示管理
    /// </summary>
    public class InfernoUIManager
    {
        /// <summary>
        /// セルの横幅
        /// </summary>
        public int CellWidth { get; set; }
        /// <summary>
        /// セルの縦幅
        /// </summary>
        public int CellHeight { get; set; }
        /// <summary>
        /// 描画が有効か
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 表示位置
        /// </summary>
        public Point Point { get; set; }
        /// <summary>
        /// 描画コンテナ
        /// </summary>
        private UIContainer _container;
        /// <summary>
        /// 描画要素
        /// </summary>
        private List<InfernoUIRow> _uiElementList;

        /// <summary>
        /// 描画要素リストのうち選択可能なリスト一覧
        /// </summary>
        private List<int> selectableIndexList; 
        /// <summary>
        /// 背景色
        /// </summary>
        private Color _backGroundColor = Color.FromArgb(200, 237, 239, 241);

        private Color _selectedColor = Color.PaleVioletRed;

        private int _selectedIndex = -1;


        public InfernoUIManager()
        {
            Point = new Point(10, 240);
            CellHeight = 30;
            CellWidth = 100;
            IsActive = false;
            _container = new UIContainer();
            selectableIndexList = new List<int>();
            _uiElementList = new List<InfernoUIRow>();
        }

        /// <summary>
        /// 描画要素を追加する
        /// </summary>
        /// <param name="element"></param>
        public void AddUIElement(InfernoUIRow element)
        {
            _uiElementList.Add(element);

            //選択可能リストに追加
            if (element.IsSelectable)
            {
                selectableIndexList.Add(_uiElementList.Count - 1);
                _selectedIndex = 0;
            }

            //追加されたタイミングでコンテナを更新する
            UpdateUIContainer();
        }

        /// <summary>
        /// 描画コンテナを更新する
        /// </summary>
        private void UpdateUIContainer()
        {
            //描画領域の高さ
            var containerHeight = _uiElementList.Count*CellHeight;
            //描画領域の横幅
            var containerWidth = CellWidth;

            //コンテナ作成
            _container = new UIContainer(Point, new Size(containerWidth, containerHeight), _backGroundColor);

            for (int index = 0; index < _uiElementList.Count; index++)
            {
                //セルの高さ
                var height = CellHeight*index;

                //現在の要素
                var element = _uiElementList[index];

                //背景(選択中の要素は色を変える
                var isSelected = _selectedIndex >= 0 && selectableIndexList[_selectedIndex] == index;
                _container.Items.Add(new UIRectangle(new Point(0, height),
                    new Size(CellWidth, CellHeight), isSelected ? _selectedColor : element.BackGroundColor));

                //文字
                _container.Items.Add(
                    new UIText(element.Text, new Point(CellWidth/2, CellHeight/4 + CellHeight*index), 0.4f, element.TextColor,
                        0, true)
                    );
            }

        }

        public void Draw()
        {
          _container.Draw();
        }

        /// <summary>
        /// 選択要素を上に動かす
        /// </summary>
        public void MoveUp()
        {
            if (_selectedIndex == -1) { return;}

            var listCount = selectableIndexList.Count;
            _selectedIndex = (_selectedIndex + listCount - 1) % listCount;

            UpdateUIContainer();
        }

        /// <summary>
        /// 選択要素を下に動かす
        /// </summary>
        public void MoveDown()
        {
            if (_selectedIndex == -1) { return; }

            var listCount = selectableIndexList.Count;
            _selectedIndex = (_selectedIndex + 1) % listCount;

            UpdateUIContainer();
        }
    }
}
