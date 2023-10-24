
using Kama.FinancialAnalysis.Model;
using System.Collections.Generic;
using System;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Kama.FinancialAnalysis.DAL;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;
using System.Threading.Tasks;

namespace Kama.FinancialAnalysis.Domain.Tr1
{
    public class Tr1Helper
    {
        TransactionDataSource _dataSource = new TransactionDataSource();

        PositionType _position = PositionType.WaitingForSignal;
        TransactionType _transactionType = TransactionType.Unknown;
        SignalModel _signalModel = new SignalModel();
        Transaction _transactionModel = new Transaction();

        double HighestMacd12 = 0;
        double LowestMacd12 = 0;

        PriceMinutely HighestPriceItem = new PriceMinutely();
        PriceMinutely LowestPriceItem = new PriceMinutely();

        public async Task Start()
        {
            var list = DbIndexPrice.XauUsd.OrderBy(x => x.ID);
            foreach (var item in list)
            {
                
                if (_position == PositionType.WaitingForSignal)
                    await SignalDetection(item);

                else if (_position == PositionType.WaitingForTransaction)
                    await TransactionDetection(item);

                else if (_position == PositionType.Transaction)
                    await CheckTransaction(item);
            }
        }

        public async Task SignalDetection(PriceMinutely item)
        {
            if (item.Date.Hour < 2 || item.Date.Hour > 21)
                return;

            var cci = DbIndexCci.XauUsd.FirstOrDefault(x => x.ID == item.ID);
            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);
            if ((cci.Value14 > 200 && macd.M12 > 0.9)
                || (cci.Value14 < -200 && macd.M12 < -0.9))
            {
                _signalModel.TransactionItem = item;
                _signalModel.MacdItem = macd;
                _position = PositionType.WaitingForTransaction;

                if (macd.M12 < -0.9)
                    _transactionType = TransactionType.Sell;
                else
                    _transactionType = TransactionType.Buy;
            }
        }

        public async Task TransactionDetection(PriceMinutely item)
        {
            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);

            //_signalModel.TransactionItem = item;
            _signalModel.MacdItem = macd;
            HighestMacd12 = macd.M12;
            LowestMacd12 = macd.M12;

            if (_transactionType == TransactionType.Sell)
            {
                if (_signalModel.MacdItem.M12 > macd.M12)
                {
                }
                else
                    await SellOrBuy(item);
            }
            else
            {
                if (_signalModel.MacdItem.M12 < macd.M12)
                {
                }
                else
                    await SellOrBuy(item);
            }
        }

        public async Task SellOrBuy(PriceMinutely item)
        {
            _position = PositionType.Transaction;
            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);

            _transactionModel = new Transaction
            {
                ID = Guid.NewGuid(),
                SignalPriceID = _signalModel.TransactionItem.ID,
                StartPriceID = item.ID,
                Type = _transactionType
            };
        }

        public async Task CheckTransaction(PriceMinutely item)
        {
            if (item.Date.Hour > 23)
            {
                await CloseTransaction(item);
                return;
            }

            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);

            if (macd.M12 > HighestMacd12)
            {
                HighestMacd12 = macd.M12;

            }
            else if (macd.M12 < LowestMacd12)
            {
                LowestMacd12 = macd.M12;

            }

            if (_transactionType == TransactionType.Sell)
                await CheckTransactionSell(item);
            else
                await CheckTransactionBuy(item);

            if (_position == PositionType.Transaction)
                await CheckPriceItem(item);

            if (_position == PositionType.WaitingForSignal)
            {
                //if (macd.M12 > 1 || macd.M12 < -1)
                //{
                //    _signalModel.TransactionItem = item;
                //    _signalModel.MacdItem = macd;
                //    _position = PositionType.WaitingForTransaction;

                //    if (macd.M12 < -1)
                //        _transactionType = TransactionType.Sell;
                //    else
                //        _transactionType = TransactionType.Buy;
                //    await SellOrBuy(item);
                //    _transactionModel.Returned = true;
                //}
            }
        }

        public async Task CheckTransactionSell(PriceMinutely item)
        {
            //var start = DbIndexPrice.XauUsd.FirstOrDefault(x => x.ID == _transactionModel.StartPriceID);
            //if (item.Close - start.Close < -4)
            //{
            //    await CloseTransaction(item);
            //    return;
            //}

            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);

            if (macd.M12 > 0.2 )
            {
                if (macd.M12 + 0.16 < HighestMacd12)
                {
                    await CloseTransaction(item);
                    return;
                }

                if (HighestMacd12 > 0.99)
                {
                    if (macd.M12 + 0.14 < HighestMacd12)
                    {
                        await CloseTransaction(item);
                        return;
                    }
                }
            }

        }

        public async Task CheckTransactionBuy(PriceMinutely item)
        {
            //var start = DbIndexPrice.XauUsd.FirstOrDefault(x => x.ID == _transactionModel.StartPriceID);
            //if (start.Close - item.Close < -4)
            //{
            //    await CloseTransaction(item);
            //    return;
            //}

            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);

            if (macd.M12 < -0.2)
            {
                if (macd.M12 - 0.16 < LowestMacd12)
                {
                    await CloseTransaction(item);
                    return;
                }

                if (LowestMacd12 < -0.99)
                {
                    if (macd.M12 - 0.14 < LowestMacd12)
                    {
                        await CloseTransaction(item);
                        return;
                    }
                }
            }

        }
        public async Task CheckPriceItem(PriceMinutely item)
        {
            var type = PriceType.Unknown;

            if (item.Close > HighestPriceItem.Close)
            {
                type = PriceType.Highest;
                HighestPriceItem = item;
            }
            if (item.Close < LowestPriceItem.Close)
            {
                type = PriceType.Lowest;
                LowestPriceItem = item;
            }

            if (type != PriceType.Unknown)
                await _dataSource.AddBestPriceAsync(new BestPrice
                {
                    ID = Guid.NewGuid(),
                    TransactionID = _transactionModel.ID,
                    PriceID = item.ID,
                    Type = type
                });
        }
        public async Task CloseTransaction(PriceMinutely item)
        {
            _position = PositionType.WaitingForSignal;
            _transactionModel.EndPriceID = item.ID;
            var start = DbIndexPrice.XauUsd.FirstOrDefault(x => x.ID == _transactionModel.StartPriceID);

            if (_transactionType == TransactionType.Sell)
                _transactionModel.Profit = item.Close - start.Close;
            else
                _transactionModel.Profit = start.Close - item.Close;

            await _dataSource.AddTransactionAsync(_transactionModel);
        }
    }
}
