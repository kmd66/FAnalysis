import json
import sys
import MetaTrader5 as mt5
 


class mtConnection:
    def getData(self, _symbol, _from, _len):
        if not mt5.initialize():
            mt5.shutdown()
            return None
        rates = mt5.copy_rates_from_pos(_symbol, mt5.TIMEFRAME_M1, _from, _len)
        mt5.shutdown()
        return rates



symbol = sys.argv[1] #"EURUSD"
fromDate =int(sys.argv[2])
lenDate =int(sys.argv[3])
filePath = sys.argv[4]

connection = mtConnection()
rates = connection.getData(symbol, fromDate, lenDate)

if rates is None:
    print("failure")
else:
    f = open(filePath, "w")
    f.write(json.dumps(rates.tolist()))
    f.close()
    print("success")
