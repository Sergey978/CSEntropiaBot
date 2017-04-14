using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EntropiaBot.EntropiaWindow
{
    class Radar
    {
       
        public static readonly Area RadarAr = new Area(1006,713, 1156, 863,"RadarAr");
        public static readonly Area AR1 = new Area(1073, 777,1078, 787,"AR1");
        public static readonly Area AR2 = new Area(1073, 715, 1078, 778, "AR2");
        public static readonly Area AR3 = new Area(1045, 715, 1073, 788, "AR3");
        public static readonly Area AR4 = new Area(1078, 715, 1105, 788, "AR4");
        public static readonly Area AR5 = new Area(1000, 715, 1044, 788, "AR5");
        public static readonly Area AR6 = new Area(1105, 715, 1150, 788, "AR6");
        public static readonly Area AR7 = new Area(1000, 788, 1075, 860, "AR7");
        public static readonly Area AR8 = new Area(1075, 788, 1150, 860, "AR8");

        public static Area[] AreaArr = { AR1, AR2 , AR3, AR4, AR5, AR6, AR7, AR8};

        public static readonly Area MobDryAr = new Area(377, 852, 403, 858, "MobDryAr");

        public static readonly Area HealthLow = new Area(901, 835, 907, 851, "HealthLow");
        public static readonly Area HealthNorm = new Area(976, 839, 982, 845, "HealthNorm");


        public static  readonly Color TP = System.Drawing.ColorTranslator.FromHtml("#0180FF");
        public static  readonly Color NPCColor = System.Drawing.ColorTranslator.FromHtml("#FF8000");
        public static  readonly Color MobColor = System.Drawing.ColorTranslator.FromHtml("#FA3C32");
        public static  readonly Color OtherPlayerColor = System.Drawing.ColorTranslator.FromHtml("#00FA3C");
        public static  readonly Color YellowColor = System.Drawing.ColorTranslator.FromHtml("#BFC206");
        public static  readonly Color HealthLowColor = System.Drawing.ColorTranslator.FromHtml("#D27921");
        public static  readonly Color HealthNormColor = System.Drawing.ColorTranslator.FromHtml("#42A400");

    }
}
