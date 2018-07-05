using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommon
{
    class CommonService
    {
        //0 1 2 
        public static void playSound(int re)
        {
            try
            {
                if (re == 0)
                {
                    AudioHelper.Play(".\\Res\\success.wav");
                }
                else if(re == 1)
                {
                    AudioHelper.Play(".\\Res\\fail.wav");
                }
                else if (re == 2)
                {
                    AudioHelper.Play(".\\Res\\warningwav.wav");
                }
            }
            catch (Exception)
            { }
        }
    }
}
