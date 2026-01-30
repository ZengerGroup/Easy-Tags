using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Tags
{
    public class TrayTag
    {
        public static void GetTrayTagStr(string job, string File_Name, string filenum, string Pack_ID_Start, string Pack_ID_Stop, string tagnumber, out string TagString)
        {
            string TrayTagString = "";
            string CR = Environment.NewLine;
            Int32 fs = 16;

            if (File_Name.Length > 30)
            {
                fs = 14;
            }

            TrayTagString = TrayTagString + "^D57" + CR +
            "6,832,381,0,0,7,3,1,332,0,0 " + CR +
            "1,130,315," + (tagnumber.Length + 5) + ",1,12,0,4,1,1,,,,,0 " + CR +
            "2,416,290," + job.Length + ",1,16,0,4,1,1,,,,,0 " + CR +
            "3,416,244," + File_Name.Length + ",1," + fs.ToString() + ",0,4,1,1,,,,,0 " + CR +
            "4,416,118," + filenum.Length + ",1,20,0,4,2,2,,,,,0 " + CR +
            "5,416,77," + (Pack_ID_Start.Length + 1) + ",1,16,0,4,1,1,,,,,0 " + CR +
            "6,416,37," + Pack_ID_Stop.Length + ",1,16,0,4,1,1,,,,,0 " + CR +
            "^D56 " + CR +
            "^D2 " + CR +
            "TAG#-" + tagnumber + CR +
            job + CR +
            File_Name + CR +
            filenum + CR +
            Pack_ID_Start + "-" + CR +
            Pack_ID_Stop + CR +
            "^D3 " + CR;

            TagString = TrayTagString;

        }

    }
}
