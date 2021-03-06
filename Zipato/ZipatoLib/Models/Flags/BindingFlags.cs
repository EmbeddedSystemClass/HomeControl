// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingFlags.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Flags
{
    using System;

    [Flags]
    public enum BindingFlags
    {
        NONE = 0,
        NETWORK = 1,
        DEVICE = 2,
        ENDPOINT = 4,
        FULL = 8,
        ALL = NETWORK | DEVICE | ENDPOINT | FULL
    }
}
