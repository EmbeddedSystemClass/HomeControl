﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWeb.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace WallboxTest
{
    #region Using Directives

    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using DataValueLib;
    using WallboxLib;
    using WallboxLib.Models;

    #endregion

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("Wallbox Test Collection")]
    public class TestWeb
    {
        #region Private Data Members

        private readonly ILogger _logger;
        private readonly HttpClient _client;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestWeb"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestWeb(ITestOutputHelper output)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(output));
            _logger = loggerFactory.CreateLogger<Wallbox>();
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8009")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Test Methods

        [Fact]
        public async Task TestGetAllData()
        {
            // Act
            var response = await _client.GetAsync("api/wallbox/all");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<WallboxData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetReport1Data()
        {
            // Act
            var response = await _client.GetAsync("api/wallbox/report1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Report1Data>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetReport2Data()
        {
            // Act
            var response = await _client.GetAsync("api/wallbox/report2");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Report2Data>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetReport3Data()
        {
            // Act
            var response = await _client.GetAsync("api/wallbox/report3");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Report3Data>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }

        [Fact]
        public async Task TestGetReport100Data()
        {
            // Act
            var response = await _client.GetAsync("api/wallbox/report100");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ReportsData>(responseString);
            Assert.NotNull(data);
            Assert.Equal(DataStatus.Good, data.Status.Code);
        }


        [Theory]
        [InlineData("Wallbox")]

        [InlineData("Wallbox.Report1")]
        [InlineData("Wallbox.Report1.ID")]
        [InlineData("Wallbox.Report1.Product")]
        [InlineData("Wallbox.Report1.Serial")]
        [InlineData("Wallbox.Report1.Firmware")]
        [InlineData("Wallbox.Report1.ComModule")]
        [InlineData("Wallbox.Report1.Backend")]
        [InlineData("Wallbox.Report1.TimeQ")]
        [InlineData("Wallbox.Report1.DIPSwitch")]
        [InlineData("Wallbox.Report1.Seconds")]

        [InlineData("Wallbox.Report2")]
        [InlineData("Wallbox.Report2.ID")]
        [InlineData("Wallbox.Report2.State")]
        [InlineData("Wallbox.Report2.Error1")]
        [InlineData("Wallbox.Report2.Error2")]
        [InlineData("Wallbox.Report2.Plug")]
        [InlineData("Wallbox.Report2.AuthON")]
        [InlineData("Wallbox.Report2.AuthRequired")]
        [InlineData("Wallbox.Report2.EnableSystem")]
        [InlineData("Wallbox.Report2.EnableUser")]
        [InlineData("Wallbox.Report2.MaxCurrent")]
        [InlineData("Wallbox.Report2.DutyCycle")]
        [InlineData("Wallbox.Report2.CurrentHW")]
        [InlineData("Wallbox.Report2.CurrentUser")]
        [InlineData("Wallbox.Report2.CurrentFS")]
        [InlineData("Wallbox.Report2.TimeoutFS")]
        [InlineData("Wallbox.Report2.CurrentTimer")]
        [InlineData("Wallbox.Report2.TimeoutCT")]
        [InlineData("Wallbox.Report2.SetEnergy")]
        [InlineData("Wallbox.Report2.Output")]
        [InlineData("Wallbox.Report2.Input")]
        [InlineData("Wallbox.Report2.Serial")]
        [InlineData("Wallbox.Report2.Seconds")]

        [InlineData("Wallbox.Report3")]
        [InlineData("Wallbox.Report3.ID")]
        [InlineData("Wallbox.Report3.VoltageL1N")]
        [InlineData("Wallbox.Report3.VoltageL2N")]
        [InlineData("Wallbox.Report3.VoltageL3N")]
        [InlineData("Wallbox.Report3.CurrentL1")]
        [InlineData("Wallbox.Report3.CurrentL2")]
        [InlineData("Wallbox.Report3.CurrentL3")]
        [InlineData("Wallbox.Report3.Power")]
        [InlineData("Wallbox.Report3.PowerFactor")]
        [InlineData("Wallbox.Report3.EnergyCharging")]
        [InlineData("Wallbox.Report3.EnergyTotal")]
        [InlineData("Wallbox.Report3.Serial")]
        [InlineData("Wallbox.Report3.Seconds")]

        [InlineData("Wallbox.Report100")]
        [InlineData("Wallbox.Report100.ID")]
        [InlineData("Wallbox.Report100.SessionID")]
        [InlineData("Wallbox.Report100.CurrentHW")]
        [InlineData("Wallbox.Report100.EnergyConsumption")]
        [InlineData("Wallbox.Report100.EnergyTransferred")]
        [InlineData("Wallbox.Report100.StartedSeconds")]
        [InlineData("Wallbox.Report100.EndedSeconds")]
        [InlineData("Wallbox.Report100.Started")]
        [InlineData("Wallbox.Report100.Ended")]
        [InlineData("Wallbox.Report100.Reason")]
        [InlineData("Wallbox.Report100.TimeQ")]
        [InlineData("Wallbox.Report100.RFID")]
        [InlineData("Wallbox.Report100.Serial")]
        [InlineData("Wallbox.Report100.Seconds")]

        [InlineData("Wallbox.Reports")]
        [InlineData("Wallbox.Reports.Count")]

        [InlineData("Wallbox.Reports[0]")]
        [InlineData("Wallbox.Reports[0].ID")]
        [InlineData("Wallbox.Reports[0].SessionID")]
        [InlineData("Wallbox.Reports[0].CurrentHW")]
        [InlineData("Wallbox.Reports[0].EnergyConsumption")]
        [InlineData("Wallbox.Reports[0].EnergyTransferred")]
        [InlineData("Wallbox.Reports[0].StartedSeconds")]
        [InlineData("Wallbox.Reports[0].EndedSeconds")]
        [InlineData("Wallbox.Reports[0].Started")]
        [InlineData("Wallbox.Reports[0].Ended")]
        [InlineData("Wallbox.Reports[0].Reason")]
        [InlineData("Wallbox.Reports[0].TimeQ")]
        [InlineData("Wallbox.Reports[0].RFID")]
        [InlineData("Wallbox.Reports[0].Serial")]
        [InlineData("Wallbox.Reports[0].Seconds")]

        [InlineData("Wallbox.Data")]
        [InlineData("Wallbox.Data.Report1")]
        [InlineData("Wallbox.Data.Report1.ID")]
        [InlineData("Wallbox.Data.Report1.Product")]
        [InlineData("Wallbox.Data.Report1.Serial")]
        [InlineData("Wallbox.Data.Report1.Firmware")]
        [InlineData("Wallbox.Data.Report1.ComModule")]
        [InlineData("Wallbox.Data.Report1.Backend")]
        [InlineData("Wallbox.Data.Report1.TimeQ")]
        [InlineData("Wallbox.Data.Report1.DipSW")]
        [InlineData("Wallbox.Data.Report1.Sec")]

        [InlineData("Wallbox.Data.Report2")]
        [InlineData("Wallbox.Data.Report2.ID")]
        [InlineData("Wallbox.Data.Report2.State")]
        [InlineData("Wallbox.Data.Report2.Error1")]
        [InlineData("Wallbox.Data.Report2.Error2")]
        [InlineData("Wallbox.Data.Report2.Plug")]
        [InlineData("Wallbox.Data.Report2.AuthON")]
        [InlineData("Wallbox.Data.Report2.AuthReq")]
        [InlineData("Wallbox.Data.Report2.EnableSys")]
        [InlineData("Wallbox.Data.Report2.EnableUser")]
        [InlineData("Wallbox.Data.Report2.MaxCurr")]
        [InlineData("Wallbox.Data.Report2.MaxCurrPercent")]
        [InlineData("Wallbox.Data.Report2.CurrHW")]
        [InlineData("Wallbox.Data.Report2.CurrUser")]
        [InlineData("Wallbox.Data.Report2.CurrFS")]
        [InlineData("Wallbox.Data.Report2.TmoFS")]
        [InlineData("Wallbox.Data.Report2.CurrTimer")]
        [InlineData("Wallbox.Data.Report2.TmoCT")]
        [InlineData("Wallbox.Data.Report2.Setenergy")]
        [InlineData("Wallbox.Data.Report2.Output")]
        [InlineData("Wallbox.Data.Report2.Input")]
        [InlineData("Wallbox.Data.Report2.Serial")]
        [InlineData("Wallbox.Data.Report2.Sec")]

        [InlineData("Wallbox.Data.Report3")]
        [InlineData("Wallbox.Data.Report3.ID")]
        [InlineData("Wallbox.Data.Report3.U1")]
        [InlineData("Wallbox.Data.Report3.U2")]
        [InlineData("Wallbox.Data.Report3.U3")]
        [InlineData("Wallbox.Data.Report3.I1")]
        [InlineData("Wallbox.Data.Report3.I2")]
        [InlineData("Wallbox.Data.Report3.I3")]
        [InlineData("Wallbox.Data.Report3.P")]
        [InlineData("Wallbox.Data.Report3.PF")]
        [InlineData("Wallbox.Data.Report3.Epres")]
        [InlineData("Wallbox.Data.Report3.Etotal")]
        [InlineData("Wallbox.Data.Report3.Serial")]
        [InlineData("Wallbox.Data.Report3.Sec")]

        [InlineData("Wallbox.Data.Report100")]
        [InlineData("Wallbox.Data.Report100.ID")]
        [InlineData("Wallbox.Data.Report100.SessionID")]
        [InlineData("Wallbox.Data.Report100.CurrHW")]
        [InlineData("Wallbox.Data.Report100.Estart")]
        [InlineData("Wallbox.Data.Report100.Epres")]
        [InlineData("Wallbox.Data.Report100.StartedSec")]
        [InlineData("Wallbox.Data.Report100.EndedSec")]
        [InlineData("Wallbox.Data.Report100.Started")]
        [InlineData("Wallbox.Data.Report100.Ended")]
        [InlineData("Wallbox.Data.Report100.Reason")]
        [InlineData("Wallbox.Data.Report100.TimeQ")]
        [InlineData("Wallbox.Data.Report100.RFIDclass")]
        [InlineData("Wallbox.Data.Report100.Serial")]
        [InlineData("Wallbox.Data.Report100.Sec")]

        [InlineData("Wallbox.Data.Reports")]
        [InlineData("Wallbox.Data.Reports[0]")]
        [InlineData("Wallbox.Data.Reports[0].ID")]
        [InlineData("Wallbox.Data.Reports[0].SessionID")]
        [InlineData("Wallbox.Data.Reports[0].CurrHW")]
        [InlineData("Wallbox.Data.Reports[0].Estart")]
        [InlineData("Wallbox.Data.Reports[0].Epres")]
        [InlineData("Wallbox.Data.Reports[0].StartedSec")]
        [InlineData("Wallbox.Data.Reports[0].EndedSec")]
        [InlineData("Wallbox.Data.Reports[0].Started")]
        [InlineData("Wallbox.Data.Reports[0].Ended")]
        [InlineData("Wallbox.Data.Reports[0].Reason")]
        [InlineData("Wallbox.Data.Reports[0].TimeQ")]
        [InlineData("Wallbox.Data.Reports[0].RFIDclass")]
        [InlineData("Wallbox.Data.Reports[0].Serial")]
        [InlineData("Wallbox.Data.Reports[0].Sec")]
        public async Task TestGetPropertyData(string name)
        {
            // Act
            var response = await _client.GetAsync($"api/wallbox/property/{name}");

            // Assert
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        #endregion
    }
}
