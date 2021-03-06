﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipatoHub.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoWeb.Hubs
{
    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using BaseClassLib;
    using ZipatoLib;
    using ZipatoWeb.Models;
    using System;
    using System.Threading;

    #endregion

    /// <summary>
    /// SignalR Hub class for Zipato data.
    /// </summary>
    public class ZipatoHub : BaseHub<AppSettings>
    {
        #region Private Fields

        private readonly IZipato _zipato;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="ZipatoHub"/> class.
        /// </summary>
        /// <param name="zipato">The Zipato instance.</param>
        /// <param name="logger">The application logger.</param>
        /// <param name="options">The application options.</param>
        public ZipatoHub(IZipato zipato,
                         ILogger<ZipatoHub> logger,
                         IOptions<AppSettings> options)
            : base(logger, options)
        {
            _zipato = zipato;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Establish connection and broadcast data.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _logger?.LogDebug("ZipatoHub client connected.");
        }

        /// <summary>
        /// Callback to provide data updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateData()
        {
            await Clients.All.SendAsync("UpdateData", _zipato.Data);
            await Clients.All.SendAsync("UpdateValues", _zipato.Data.Values);
            await Clients.All.SendAsync("UpdateDevices", _zipato.Devices);
            await Clients.All.SendAsync("UpdateSensors", _zipato.Sensors);
            await Clients.All.SendAsync("UpdateOthers", _zipato.Others);
        }

        /// <summary>
        /// Callback to provide data values updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateValues()
        {
            await Clients.All.SendAsync("UpdateValues", _zipato.Data.Values);
            await Clients.All.SendAsync("UpdateDevices", _zipato.Devices);
            await Clients.All.SendAsync("UpdateSensors", _zipato.Sensors);
        }

        /// <summary>
        /// Callback to provide data values updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateDevices()
        {
            await Clients.All.SendAsync("UpdateDevices", _zipato.Devices);
        }

        /// <summary>
        /// Callback to provide data values updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateSensors()
        {
            await Clients.All.SendAsync("UpdateSensors", _zipato.Sensors);
        }

        /// <summary>
        /// Callback to provide data values updates.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateOthers()
        {
            await Clients.All.SendAsync("UpdateOthers", _zipato.Others);
        }

        /// <summary>
        /// Callback to run a single scene.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task RunScene(int index)
        {
            var ok = _zipato.Others.Scenes[index].Run();
            await Clients.All.SendAsync("RunScene", (ok, index));
        }

        /// <summary>
        /// Callback to toggle a single onoff switch.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task ToggleOnOff(int index)
        {
            var ok = _zipato.Devices.OnOffSwitches[index].Toggle();
            await Clients.All.SendAsync("ToggleOnOff", (ok, index, _zipato.Devices.OnOffSwitches[index].State.Value));
        }

        /// <summary>
        /// Callback to toggle a single wallplug.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task TogglePlug(int index)
        {
            var ok = _zipato.Devices.Wallplugs[index].Toggle();
            await Clients.All.SendAsync("TogglePlug", (ok, index, _zipato.Devices.Wallplugs[index].State.Value));
        }

        /// <summary>
        /// Callback to set a dimmer intensity.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SetDimmer(int index, int value)
        {
            var ok = _zipato.Devices.Dimmers[index].SetIntensity(value);
            await Clients.All.SendAsync("SetDimmer", (ok, index, _zipato.Devices.Dimmers[index].Intensity.Value));
        }

        #endregion
    }
}
