//using AutoMapper.Configuration;
using EmcureNPD.Business.Core.Interface;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class CurrencyConverter : ICurrencyConverter
    {

        private readonly IConfiguration _configuration;

        public CurrencyConverter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<double> ConvertCurrencyAsync(string fromCurrency, string toCurrency)
        {
            try
            {
                return 100.00;
                //var APIKEY = _configuration.GetSection("ConvertCurrency:ApiKey").Value;
                //var converter = new EmcureNPD.CurrencyConverter.Converter(APIKEY);
                //var from = (EmcureNPD.CurrencyConverter.Enums.CurrencyType)Enum.Parse(typeof(EmcureNPD.CurrencyConverter.Enums.CurrencyType), fromCurrency);
                //var to = (EmcureNPD.CurrencyConverter.Enums.CurrencyType)Enum.Parse(typeof(EmcureNPD.CurrencyConverter.Enums.CurrencyType), toCurrency);
                //var convertAmount = await converter.ConvertAsync(1, from, to);
                //return convertAmount;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Error: {ex.Message}");
            }
        }
    }
}
