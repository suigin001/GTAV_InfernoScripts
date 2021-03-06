﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inferno.ChaosMode
{
    /// <summary>
    /// jsonの読み込み結果をマッピングするDTO
    /// </summary>
    public class ChaosModeSettingDTO
    {
        public int Radius { get; set; } = 1000;
        public int Interval { get; set; } = 500;
        public bool IsChangeMissionCharacterWeapon { get; set; } = true;
        public int DefaultMissionCharacterTreatment { get; set; } = 1;
        public bool IsAttackPlayerCorrectionEnabled { get; set; } = false;
        public int AttackPlayerCorrectionProbabillity { get; set; } = 100;
        public string[] WeaponList { get; set; } = new[] {""};
        public string[] WeaponListForDriveBy { get; set; } = new[] { "" };
        public bool IsStupidShooting { get; set; } = true;
        public int ShootAccuracy { get; set; } = 100;
        public int WeaponChangeProbabillity { get; set; } = 30;
    }
}
