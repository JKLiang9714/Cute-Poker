using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    public class MusicPlay
    {
        public static uint SND_ASYNC = 0x0001;
        public static uint SND_FILENAME = 0x00020000;
        [DllImport("winmm.dll")]
        public static extern uint mciSendString(string lpstrCommand,
        string lpstrReturnString, uint uReturnLength, uint hWndCallback);

        //开始播放
        public void Play(string p_FileName)
        {
            string aa = "open " + p_FileName + " alias temp_alias";
            mciSendString(@"close temp_alias", null, 0, 0);
            mciSendString(aa, null, 0, 0);
            mciSendString("play temp_alias", null, 0, 0);
        }

        //循环播放
        public void cilPlay(string p_FileName)
        {
            string aa = "open " + p_FileName + " alias temp_alias";
            mciSendString(@"close temp_alias", null, 0, 0);
            mciSendString(aa, null, 0, 0);
            mciSendString("play temp_alias repeat", null, 0, 0);
        }

        //暂停
        public void Stop()
        {
            mciSendString(@"stop temp_alias", null, 0, 0);
        }

        //继续播放
        public void ContinuePlay()
        {
            mciSendString("play temp_alias repeat", null, 0, 0);
        }

        //结束播放
        public void End()
        {
            mciSendString(@"close temp_alias", null, 0, 0);
        }
    }
}
