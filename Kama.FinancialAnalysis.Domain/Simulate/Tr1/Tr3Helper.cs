
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
    public class Tr3Helper
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

        DateTime _ignoreDtae = DateTime.Now;
        bool _waitingForTransaction_Macd70 = false;
        public async Task Start()
        {
            var list = DbIndexPrice.XauUsd.OrderBy(x => x.ID);
            _ignoreDtae = list.First().Date.AddDays(-1);

            foreach (var item in list)
            {
                if (_ignoreDtae < item.Date.AddMinutes(-30)) {

                    if (_position == PositionType.WaitingForSignal)
                        await SignalDetection(item);

                    else if (_position == PositionType.WaitingForTransaction)
                        await TransactionDetection(item);

                    else if (_position == PositionType.Transaction)
                        await CheckTransaction(item);
                }
            }
        }

        public async Task SignalDetection(PriceMinutely item)
        {
            if ((item.Date.Hour < 2 && item.Date.Minute < 20) || (item.Date.Hour > 20 && item.Date.Minute > 50))
                return;

            //var cci = DbIndexCci.XauUsd.FirstOrDefault(x => x.ID == item.ID);
            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);
            if (macd.M12 < -0.9)
            {
                var MaMacd = fnMacdMovingAverage(item.ID);
                if(MaMacd > 0)
                {
                    _ignoreDtae = item.Date;
                }
                else
                {
                    _waitingForTransaction_Macd70 = false;
                    _signalModel.TransactionItem = item;
                    _signalModel.MacdItem = macd;
                    _position = PositionType.WaitingForTransaction;
                    _transactionType = TransactionType.Sell;
                }
            }
        }

        public async Task TransactionDetection(PriceMinutely item)
        {
            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);
            if (!_waitingForTransaction_Macd70)
            {
                _signalModel.MacdItem = macd;
                HighestMacd12 = macd.M12;
                LowestMacd12 = macd.M12;

                if (_transactionType == TransactionType.Sell)
                {
                    if (_signalModel.MacdItem.M12 > macd.M12)
                    {
                    }
                    else {
                        _waitingForTransaction_Macd70 = true;
                        HighestMacd12 = macd.M60;
                        LowestMacd12 = macd.M60;
                    }
                }
            }
            else
            {
                if (LowestMacd12 > macd.M60)
                {
                    HighestMacd12 = macd.M60;
                    LowestMacd12 = macd.M60;
                }
                else
                {
                    HighestMacd12 = macd.M12;
                    LowestMacd12 = macd.M12;
                    await SellOrBuy(item);
                }
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
            if (item.Date.Hour > 22 && item.Date.Minute > 30)
            {
                await CloseTransaction(item);
                return;
            }
            var macd = DbIndexMacd.XauUsd.FirstOrDefault(x => x.ID == item.ID);

            if (macd.M12 > HighestMacd12)
            {
                HighestMacd12 = macd.M12;

            }
            await CheckTransactionSell(item);
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

            if (macd.M12 > 0.2)
            {
                if (macd.M12 + 0.16 < HighestMacd12)
                {
                    await CloseTransaction(item);
                    return;
                }

            }

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
        public double? fnMacdMovingAverage(long id)
        {
            var list = DbIndexMacd.XauUsd.Where(x => x.ID < id).Take(200).ToList();
            if (list.Count == 0)
                return null ;
            return list.Sum(x => x.M60) / list.Count;
        }
    }
}
