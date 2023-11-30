using Newtonsoft.Json;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrep.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrep.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetPrice(string symbol)
        {
            using (FinancialModelingPrepHttpClient client = new FinancialModelingPrepHttpClient())
            {
                string uri = "stock/real-time-price/" + symbol + "&apikey=tJ65KrxbPl9Sm9d22RiGljaxWMMWzwX2";
                
                StockPriceResult stockPriceResult = await client.GetAsync<StockPriceResult>(uri);

                if (stockPriceResult == null)
                {
                    throw new InvalidSymbolException(symbol);
                }

                return stockPriceResult.Price;
            }
        }
    }
}
