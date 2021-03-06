﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using GTA;
using GTA.Math;
using GTA.Native;

namespace Inferno
{
    /// <summary>
    /// 市民の運転スピード上限増加
    /// </summary>
    class CitizenCrazyDriving : InfernoScript
    {

        private bool IsActive = false;
        private float PlayerAroundDistance = 300f;

        /// <summary>
        /// 3秒間隔
        /// </summary>
        protected override int TickInterval
        {
            get { return 3000; }
        }

        protected override void Setup()
        {
            CreateInputKeywordAsObservable("runaway")
                .Subscribe(_ =>
                {
                    IsActive = !IsActive;
                    DrawText("CitizenCrazyDriving:" + IsActive, 3.0f);

                });

            OnAllOnCommandObservable.Subscribe(_ => IsActive = true);

            OnTickAsObservable
                .Where(_ => IsActive)
                .Subscribe(_ => RunAway());
        }

        private void RunAway()
        {
            var player = this.GetPlayer();

            //プレイヤ周辺の車
            var drivers = CachedVehicles.Where(x => x.IsSafeExist()
                                                   && !x.IsSameEntity(this.GetPlayerVehicle())
                                                   && !x.IsRequiredForMission()
                                                   && (x.Position - player.Position).Length() <= PlayerAroundDistance)
                                           .Select(x => x.GetPedOnSeat(GTA.VehicleSeat.Driver))
                                           .Where(x => x.IsSafeExist());


            foreach (var driver in drivers)
            {
                try
                {
                    driver.DrivingSpeed = 100.0f;
                    driver.MaxDrivingSpeed = 100.0f;
                    driver.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
                }
                catch (Exception e)
                {
                    LogWrite(e.ToString());
                }
            }
        }
    }
}
