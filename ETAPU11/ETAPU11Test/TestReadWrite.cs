﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestReadWrite.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ETAPU11Test
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Logger;

    using ETAPU11Lib;
    using ETAPU11Lib.Models;

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    [Collection("ETAPU11 Test Collection")]
    public class TestReadWrite : IClassFixture<ETAPU11Fixture>
    {
        #region Private Data Members

        private readonly ILogger<ETAPU11> _logger;
        private readonly IETAPU11 _etapu11;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TestWrite"/> class.
        /// </summary>
        /// <param name="outputHelper"></param>
        public TestReadWrite(ETAPU11Fixture fixture, ITestOutputHelper outputHelper)
        {
            // Set the default culture.
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(outputHelper));
            _logger = loggerFactory.CreateLogger<ETAPU11>();

            _etapu11 = fixture.ETAPU11;

            // Default to localhost for read/write tests.
            _etapu11.TcpSlave.Address = "127.0.0.1";
        }

        [Fact]
        public async Task TestETAPU11ReadWrite()
        {
            await _etapu11.WriteAllAsync();
            Assert.True(_etapu11.Data.IsGood);
            await _etapu11.ReadAllAsync();
            Assert.True(_etapu11.Data.IsGood);
        }

        [Theory]
        [InlineData("HopperFillUpTime", "19:00:00")]
        [InlineData("AshRemovalStartIdleTime", "21:00:00")]
        [InlineData("AshRemovalDurationIdleTime", "10:00:00")]
        public async Task TestETAPU11ReadWriteTimeSpan(string property, string data)
        {
            var status = await _etapu11.WritePropertyAsync(property, data);
            Assert.True(status.IsGood);
            _etapu11.Data = new ETAPU11Data();
            status = await _etapu11.ReadPropertyAsync(property);
            Assert.True(status.IsGood);
            Assert.Equal(data, ((TimeSpan)_etapu11.Data.GetPropertyValue(property)).ToString());
        }

        [Theory]
        [InlineData("HeatingHolidayStart", "2018-07-26T00:00:00")]
        [InlineData("HeatingHolidayEnd", "2018-08-02T23:59:00")]
        public async Task TestETAPU11ReadWriteDateTime(string property, string data)
        {
            var status = await _etapu11.WritePropertyAsync(property, data);
            Assert.True(status.IsGood);
            _etapu11.Data = new ETAPU11Data();
            status = await _etapu11.ReadPropertyAsync(property);
            Assert.True(status.IsGood);
            Assert.Equal(data, ((DateTimeOffset)_etapu11.Data.GetPropertyValue(property)).DateTime.ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        [Theory]
        [InlineData("EmptyAshBoxAfter", 1000.0)]
        [InlineData("HopperPelletBinContents", 30.0)]
        [InlineData("HotwaterSwitchonDiff", 15.0)]
        [InlineData("HeatingTemperature", 20.0)]
        [InlineData("FlowAtMinus10", 55.0)]
        [InlineData("FlowAtPlus10", 35.0)]
        [InlineData("FlowSetBack", 15.0)]
        [InlineData("DayHeatingThreshold", 16.0)]
        [InlineData("NightHeatingThreshold", 2.0)]
        [InlineData("Stock", 2000.0)]
        [InlineData("StockWarningLimit", 800.0)]
        [InlineData("OutsideTemperature", 22.0)]
        public async Task TestETAPU11ReadWriteDouble(string property, double data)
        {
            var status = await _etapu11.WritePropertyAsync(property, data.ToString());
            Assert.True(status.IsGood);
            _etapu11.Data = new ETAPU11Data();
            status = await _etapu11.ReadPropertyAsync(property);
            Assert.True(status.IsGood);
            Assert.Equal(data, (double)_etapu11.Data.GetPropertyValue(property));
        }

        [Theory]
        [InlineData("HopperFillUpPelletBin", ETAPU11Data.StartValues.Yes)]
        [InlineData("OnOffButton", ETAPU11Data.OnOffStates.On)]
        [InlineData("DeAshButton", ETAPU11Data.OnOffStates.On)]
        [InlineData("ChargeButton", ETAPU11Data.OnOffStates.On)]
        [InlineData("HeatingDayButton", ETAPU11Data.OnOffStates.On)]
        [InlineData("HeatingAutoButton", ETAPU11Data.OnOffStates.On)]
        [InlineData("HeatingNightButton", ETAPU11Data.OnOffStates.On)]
        [InlineData("HeatingOnOffButton", ETAPU11Data.OnOffStates.On)]
        [InlineData("HeatingHomeButton", ETAPU11Data.OnOffStates.On)]
        [InlineData("HeatingAwayButton", ETAPU11Data.OnOffStates.On)]
        public async Task TestETAPU11ReadWriteEnum(string property, dynamic data)
        {
            var status = await _etapu11.WritePropertyAsync(property, data.ToString());
            Assert.True(status.IsGood);
            _etapu11.Data = new ETAPU11Data();
            status = await _etapu11.ReadPropertyAsync(property);
            Assert.True(status.IsGood);
            Assert.Equal((int)data, (int)_etapu11.Data.GetPropertyValue(property));
        }
    }
}
