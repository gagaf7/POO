using System;
using System.Collections.Generic;
using TP3_Kremser_Gaetan;

namespace SpaceInvadersArmory
{
    public interface IWeapon
    {
        string Name { get; set; }
        EWeaponType Type { get; set; }
        double MinDamage { get; set; }
        double MaxDamage { get; set; }
        double AverageDamage { get; }
        double ReloadTime { get; set; }
        double TimeBeforReload { get; set; }
        bool IsReload { get; }
        double Shoot();
    }
}