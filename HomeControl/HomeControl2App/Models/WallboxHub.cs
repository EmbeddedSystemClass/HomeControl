﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WallboxHub.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControl2App.Models
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;

    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using HomeControlLib.Wallbox.Models;

    #endregion

    /// <summary>
    /// Class implementing the hub connection and providing access to data.
    /// </summary>
    public class WallboxHub : BaseHub, INotifyPropertyChanged
    {
        #region Private Data Members

        private WallboxData _data;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public WallboxData Data
        {
            get { return _data; }
            set { _data = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public WallboxHub(ILogger<WallboxHub> logger, IOptions<AppSettings> options)
            : base(logger, new HubSettings() { Uri = options.Value.Wallbox })
        {
            _logger.LogDebug("HomeDataHub()");

            _hub.On<WallboxData>("UpdateData", async (data) =>
            {
                _logger.LogDebug("On<WallboxData>()");

                await CoreApplication.MainView
                    .Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal,
                              () => Data = data);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Connect()
        {
            var ok = await base.Connect();

            if (ok)
            {
                await _hub.InvokeAsync("UpdateData");
            }

            return ok;
        }

        #endregion
    }
}
