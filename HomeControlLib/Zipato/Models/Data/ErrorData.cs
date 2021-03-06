﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Zipato.Models.Data
{
    public class ErrorData
    {
        public bool Success { get; set; } = true;
        public string Error { get; set; }
    }
}
