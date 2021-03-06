﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;
using GTA.Native;

namespace Inferno
{
    /// <summary>
    /// 死因表示
    /// </summary>
    class DisplayCauseOfDeath : InfernoScript
    {
        private UIContainer _mContainer;
        private int ScreenHeight;
        private int ScreenWidth;

        private Vector2 textPositionScale = new Vector2(0.5f,0.75f);

        protected override int TickInterval
        {
            get { return 500; }
        }

        protected override void Setup()
        {
            var screenResolution = NativeFunctions.GetScreenResolution();
            ScreenHeight = (int)screenResolution.Y;
            ScreenWidth = (int)screenResolution.X;

            _mContainer = new UIContainer(
                new Point(0, 0), new Size(ScreenWidth, ScreenHeight));

            this.OnDrawingTickAsObservable
                .Where(_ => _mContainer.Items.Count > 0)
                .Subscribe(_ => _mContainer.Draw());

            this.OnTickAsObservable
                .Where(_ => this.GetPlayer().IsSafeExist())
                .Select(x => this.GetPlayer().IsAlive)
                .DistinctUntilChanged()
                .Subscribe(isAlive =>
                {
                    var player = this.GetPlayer();
                    _mContainer.Items.Clear();
                    if (isAlive) return;
                    
                    //死んでいたら死因を出す
                    var damageWeapon = getLastDamageWeapon(this.GetPlayer());
                    if(damageWeapon==null)return;
                        
                    var damageName = damageWeapon.ToString();
                    if (player.HasBeenDamagedByPed(player)) damageName += "(SUICIDE)";
                    var text = new UIText(damageName,
                        new Point((int)(ScreenWidth * textPositionScale.X),(int)(ScreenHeight*textPositionScale.Y)),
                        1.0f, Color.White, 0, true);

                    _mContainer.Items.Add(text);
                });

        }

        /// <summary>
        /// 最後にダメージを受けた武器を取得する
        /// </summary>
        /// <param name="ped">市民</param>
        /// <returns>最後に受けたダメージの武器</returns>
        private Weapon? getLastDamageWeapon(Ped ped)
        {
            foreach (Weapon w in Enum.GetValues(typeof(Weapon)))
            {
                if (ped.HasBeenDamagedBy(w))
                {
                    return w;
                }
            }
            return null;
        }
    }
}
