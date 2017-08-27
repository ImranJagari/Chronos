using Chronos.Core.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Network
{

    public enum VersionCheckingSeverity
    {
        /// <summary>
        /// Do not check version
        /// </summary>
        None,
        /// <summary>
        /// Check major minor and release values
        /// </summary>
        Light,
        /// <summary>
        /// Check revision value too
        /// </summary>
        Medium,
        /// <summary>
        /// Check all values
        /// </summary>
        Heavy,
    }

    public static class VersionExtension
    {
        /// <summary>
        ///   Define the severity of the client version checking. Set to Light/NoCheck if you have any bugs with it.
        /// </summary>
        [Variable(true)]
        public static VersionCheckingSeverity Severity = VersionCheckingSeverity.Light;

        /// <summary>
        /// Version for the client. 
        /// </summary>
        [Variable(true)]
        public static VersionExtended ExpectedVersion = new VersionExtended(2,2,2);

        /// <summary>
        /// Compare the given version and the required version
        /// </summary>
        /// <param name="versionToCompare"></param>
        /// <returns></returns>
        public static bool IsUpToDate(this Version versionToCompare)
        {
            switch (Severity)
            {
                case VersionCheckingSeverity.None:
                    return true;
                case VersionCheckingSeverity.Light:
                    return ExpectedVersion.Major == versionToCompare.Major;
                case VersionCheckingSeverity.Medium:
                    return ExpectedVersion.Major == versionToCompare.Major &&
                           ExpectedVersion.Minor == versionToCompare.Minor; 
                case VersionCheckingSeverity.Heavy:
                    return ExpectedVersion.Major == versionToCompare.Major &&
                           ExpectedVersion.Minor == versionToCompare.Minor &&
                           ExpectedVersion.Build == versionToCompare.Build;
            }
            
            return false;
        }
        /// <summary>
        /// Get version deserialized.
        /// 
        /// Other deserialize:
        /// 
        /// major = ((int)version >> 24) & 0xFF;
        /// minor = ((int) version >> 16) & 0xFF;
        /// build = ((int) version >>  0) & 0xFFFF; 
        /// </summary>
        /// <param name="version"> version serialized</param>
        /// <returns></returns>
        public static Version GetVersionFromCompacted(this uint version)
        {
            return new Version((int)version >> 24, (int)version >> 16, (int)(version & 255u));
        }
        public static UInt32 GetCompactedVersion()
        {
             return (UInt32)(((byte)ExpectedVersion.Major) << 24 | ((byte)ExpectedVersion.Minor) << 16 | (ushort)ExpectedVersion.Build); 
        }
    }
}