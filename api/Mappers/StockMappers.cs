using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDTO ToStockDto(this Stock stockModel)
        {
            return new StockDTO
            {
                StockId = stockModel.StockId,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDTO stockDTO)
        {
            return new Stock
            {
                Symbol = stockDTO.Symbol,
                CompanyName = stockDTO.CompanyName,
                Purchase = stockDTO.Purchase,
                LastDiv = stockDTO.LastDiv,
                Industry = stockDTO.Industry,
                MarketCap = stockDTO.MarketCap

            };
        }

        public static Stock ToStockFromFMP(this FMPStock fmpStock)
        {
            return new Stock
            {
                Symbol = fmpStock.symbol,
                CompanyName = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                LastDiv = (decimal)fmpStock.lastDiv,
                Industry = fmpStock.industry,
                MarketCap = fmpStock.mktCap

            };
        }
    }
}