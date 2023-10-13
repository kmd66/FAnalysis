﻿using DeviceDetectorNET.Class.Device;
using Kama.AppCore;
using Kama.FinancialAnalysis.DAL;
using Kama.FinancialAnalysis.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace Kama.FinancialAnalysis.Domain
{
    public class ZigZagHelper
    {
        int _backStep = 5;
        int _depth = 2;
        double _darsad = 25;
        
        ZigZagDataSource _dataSource = new ZigZagDataSource();
        
        public ZigZagHelper()
        {
        }
        public async Task StartHistory()
        {
            await insertDbHistory(SymbolType.xauusd);
            insertDbHistory(SymbolType.usdchf);
            insertDbHistory(SymbolType.eurjpy);
            insertDbHistory(SymbolType.nq100m);
            await insertDbHistory(SymbolType.DYX);

        }

        private async Task insertDbHistory(SymbolType symbolType)
        {

            var list = DbIndexPrice.GetByType(symbolType);

            var listGroup = list.GroupBy(x => x.Date.ToString("yyyyMMdd")).Select(x=>x.ToList()).ToList();
            var listInsert = new List<ZigZag>();


            foreach (var l in listGroup)
            {
                l.Reverse();
                listInsert = new List<ZigZag>();

                PriceMinutely lastFavorableItem = null;
                double lastFavorableValue = 0;
                bool lastFavorableUp = false;

                PriceMinutely lastP = null;
                double valueP = 0;


                valueP = l[0].Close - l[0].Open;
                lastP = l[0];
                listInsert.Add(new ZigZag { 
                    ID= l[0].ID,
                    Date= l[0].Date,
                    Approved= l[0].ID,
                    Up=false,
                    Value = valueP,
                    Type = symbolType
                });

                foreach (var item in l)
                {
                    var up = item.Close > item.Open;
                    if (lastFavorableItem == null && listInsert.Count == 1)
                    {
                        if (Math.Abs(valueP * _backStep) < Math.Abs(item.Close - lastP.Close))
                        {
                            lastFavorableItem = item;
                            lastFavorableValue = item.Close - lastP.Close;
                            lastFavorableUp = up;
                        }
                    }

                    else
                    {
                        if (up == lastFavorableUp)
                        {
                            if (Math.Abs(lastFavorableValue) < Math.Abs(item.Close - lastP.Close))
                            {
                                lastFavorableItem = item;
                                lastFavorableValue = item.Close - lastP.Close;
                                valueP = item.Close - item.Open;
                            }
                        }
                        //else if (Math.abs(valueP * 5) < Math.abs(item.Close - lastP.Close)) {
                        else //if (Math.Abs(valueP * _darsad) < Math.Abs(item.Close - lastFavorableItem.Close))
                        {
                            var len = _darsad * 3;
                            if (listInsert.Count == 1)
                                len = _darsad;
                            var v = (Math.Abs(item.Close - lastFavorableItem.Close) * 100) / Math.Abs(lastFavorableValue);
                            var t1 = l.FindIndex(x => x.ID == item.ID);
                            var t2 = l.FindIndex(x => x.ID == lastFavorableItem.ID);
                            if (v >= _darsad && l.FindIndex(x => x.ID == item.ID) - l.FindIndex(x => x.ID == lastFavorableItem.ID) >= len)
                            {
                                lastP = lastFavorableItem;
                                listInsert.Add(new ZigZag
                                {
                                    ID = lastFavorableItem.ID,
                                    Date = lastFavorableItem.Date,
                                    Approved = item.ID,
                                    Up = lastFavorableUp,
                                    Value = lastFavorableValue,
                                    Type = symbolType
                                });

                                lastFavorableItem = item;
                                lastFavorableValue = item.Close - lastP.Close;
                                lastFavorableUp = !lastFavorableUp;
                            }
                        }

                    }
                }

                await _dataSource.AddList(listInsert);
            }

        }
        public static long[] MinMAax(List<PriceMinutely>list, long priceID, SymbolType type)
        {
            var l = list.Where(x => x.ID > priceID && x.Type == type).Take(240).ToList();
            if (l.Count == 0)
                return new long[2] { 0, 0 };

            var itemMax = l.First(x => x.Close == l.Max(y => y.Close));
            var itemMin = l.First(x => x.Close == l.Min(y => y.Close));
            return new long[2] { itemMax.ID, itemMin.ID };
        }
    }
}
