using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace HLACommonLib
{
    /// <summary>
    /// 声音播放辅助类[仅支持wav音频格式]
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class AudioHelper
    {

        [DllImport("winmm.dll")]
        public static extern bool PlaySound(string pszSound, int hmod, int fdwSound);
        public const int SND_FILENAME = 0x00020000;
        public const int SND_ASYNC = 0x0001;


        private static SoundPlayer _SoundPlayer;

        #region Private Methods
        private static void InternalStop(SoundPlayer sound)
        {
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
            try
            {
                sound.Stop();
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }

        private static void Play(SoundPlayer sound, AudioPlayMode mode)
        {
            if (_SoundPlayer != null)
            {
                InternalStop(_SoundPlayer);
            }
            _SoundPlayer = sound;
            switch (mode)
            {
                case AudioPlayMode.WaitToComplete:
                    _SoundPlayer.PlaySync();
                    return;

                case AudioPlayMode.Background:
                    _SoundPlayer.Play();
                    return;

                case AudioPlayMode.BackgroundLoop:
                    _SoundPlayer.PlayLooping();
                    return;
            }
        }

        private static void ValidateAudioPlayModeEnum(AudioPlayMode value, string paramName)
        {
            if ((value < AudioPlayMode.WaitToComplete) || (value > AudioPlayMode.BackgroundLoop))
            {
                throw new InvalidEnumArgumentException(paramName, (int)value, typeof(AudioPlayMode));
            }
        }

        private static string ValidateFilename(string location)
        {
            if (String.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException("location");

            }
            return location;
        }
        #endregion

        #region Public Methods
        public static void PlayWithSystem(string location)
        {
            PlaySound(location, 0, SND_ASYNC | SND_FILENAME);
        }

        /// <summary>播放。wav声音文件。</summary>
        /// <param name="location">String，包含声音文件的名称 </param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread" /><IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public static void Play(string location)
        {
            Play(location, AudioPlayMode.Background);
        }


        /// <summary>播放。wav声音文件.</summary>
        /// <param name="playMode">AudioPlayMode枚举模式播放声音。默认情况下，AudioPlayMode.Background。</param>
        /// <param name="location">String，包含声音文件的名称 </param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread" /><IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public static void Play(string location, AudioPlayMode playMode)
        {
            try {
                ValidateAudioPlayModeEnum(playMode, "playMode");
                string text1 = ValidateFilename(location);
                SoundPlayer player1 = new SoundPlayer(text1);
                Play(player1, playMode);
            }
            catch
            {

            }
        }


        /// <summary>播放。wav声音文件。</summary>
        /// <param name="playMode">AudioPlayMode枚举模式播放声音。默认情况下，AudioPlayMode.Background。</param>
        /// <param name="stream"><see cref="T:System.IO.Stream"></see> 代表的声音文件。</param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread" /><IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public static void Play(Stream stream, AudioPlayMode playMode)
        {
            ValidateAudioPlayModeEnum(playMode, "playMode");
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            Play(new SoundPlayer(stream), playMode);
        }


        /// <summary>播放。wav声音文件。</summary>
        /// <param name="data">字节数组代表的声音文件。</param>
        /// <param name="playMode">AudioPlayMode枚举模式播放声音。默认情况下，AudioPlayMode.Background。</param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread" /><IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public static void Play(byte[] data, AudioPlayMode playMode)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            ValidateAudioPlayModeEnum(playMode, "playMode");
            MemoryStream stream1 = new MemoryStream(data);
            Play(stream1, playMode);
            stream1.Close();
        }




        /// <summary>播放系统声音。</summary>
        /// <param name="systemSound"><see cref="T:System.Media.SystemSound"></see> 对象代表系统播放声音。</param>
        public static void PlaySystemSound(SystemSound systemSound)
        {
            if (systemSound == null)
            {
                throw new ArgumentNullException("systemSound");
            }
            systemSound.Play();
        }


        /// <summary>停止在后台播放声音。</summary>
        /// <filterpriority>1</filterpriority>
        public static void Stop()
        {
            SoundPlayer player1 = new SoundPlayer();
            InternalStop(player1);
        }
        #endregion
    }

    /// <summary>
    /// 指示如何调用音频方法时，播放声音
    /// </summary>
    public enum AudioPlayMode
    {
        /// <summary>
        /// 播放声音，并等待，直到它完成之前调用代码继续。 
        /// </summary>
        WaitToComplete,

        /// <summary>
        /// 在后台播放声音。调用代码继续执行。 
        /// </summary>
        Background,

        /// <summary>
        ///直到stop方法被称为播放背景声音。调用代码继续执行。 
        /// </summary>
        BackgroundLoop
    }

    [Flags]
    public enum SoundFlags
    {
        /// <summary>play synchronously (default)</summary>
        SND_SYNC = 0x0000,
        /// <summary>play asynchronously</summary>
        SND_ASYNC = 0x0001,
        /// <summary>silence (!default) if sound not found</summary>
        SND_NODEFAULT = 0x0002,
        /// <summary>pszSound points to a memory file</summary>
        SND_MEMORY = 0x0004,
        /// <summary>loop the sound until next sndPlaySound</summary>
        SND_LOOP = 0x0008,
        /// <summary>don’t stop any currently playing sound</summary>
        SND_NOSTOP = 0x0010,
        /// <summary>Stop Playing Wave</summary>
        SND_PURGE = 0x40,
        /// <summary>don’t wait if the driver is busy</summary>
        SND_NOWAIT = 0x00002000,
        /// <summary>name is a registry alias</summary>
        SND_ALIAS = 0x00010000,
        /// <summary>alias is a predefined id</summary>
        SND_ALIAS_ID = 0x00110000,
        /// <summary>name is file name</summary>
        SND_FILENAME = 0x00020000,
        /// <summary>name is resource name or atom</summary>
        SND_RESOURCE = 0x00040004
    }
}
