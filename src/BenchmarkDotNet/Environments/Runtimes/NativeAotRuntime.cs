﻿using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Portability;
using System;

namespace BenchmarkDotNet.Environments
{
    public class NativeAotRuntime : Runtime
    {
        /// <summary>
        /// NativeAOT compiled as net6.0
        /// </summary>
        public static readonly NativeAotRuntime Net60 = new NativeAotRuntime(RuntimeMoniker.NativeAot60, "net6.0", "NativeAOT 6.0");
        /// <summary>
        /// NativeAOT compiled as net7.0
        /// </summary>
        public static readonly NativeAotRuntime Net70 = new NativeAotRuntime(RuntimeMoniker.NativeAot70, "net7.0", "NativeAOT 7.0");
        /// <summary>
        /// NativeAOT compiled as net8.0
        /// </summary>
        public static readonly NativeAotRuntime Net80 = new NativeAotRuntime(RuntimeMoniker.NativeAot80, "net8.0", "NativeAOT 8.0");
        /// <summary>
        /// NativeAOT compiled as net9.0
        /// </summary>
        public static readonly NativeAotRuntime Net90 = new NativeAotRuntime(RuntimeMoniker.NativeAot90, "net9.0", "NativeAOT 9.0");
        /// <summary>
        /// NativeAOT compiled as net10.0
        /// </summary>
        public static readonly NativeAotRuntime Net10_0 = new NativeAotRuntime(RuntimeMoniker.NativeAot10_0, "net10.0", "NativeAOT 10.0");

        public override bool IsAOT => true;

        private NativeAotRuntime(RuntimeMoniker runtimeMoniker, string msBuildMoniker, string displayName)
            : base(runtimeMoniker, msBuildMoniker, displayName)
        {
        }

        public static NativeAotRuntime GetCurrentVersion()
        {
            if (!RuntimeInformation.IsNetCore && !RuntimeInformation.IsNativeAOT)
            {
                throw new NotSupportedException("It's impossible to reliably detect the version of NativeAOT if the process is not a .NET or NativeAOT process!");
            }

            if (!CoreRuntime.TryGetVersion(out var version))
            {
                throw new NotSupportedException("Failed to recognize NativeAOT version");
            }

            switch (version)
            {
                case Version v when v.Major == 6 && v.Minor == 0: return Net60;
                case Version v when v.Major == 7 && v.Minor == 0: return Net70;
                case Version v when v.Major == 8 && v.Minor == 0: return Net80;
                case Version v when v.Major == 9 && v.Minor == 0: return Net90;
                case Version v when v.Major == 10 && v.Minor == 0: return Net10_0;
                default:
                    return new NativeAotRuntime(RuntimeMoniker.NotRecognized, $"net{version.Major}.{version.Minor}", $"NativeAOT {version.Major}.{version.Minor}");
            }
        }
    }
}
