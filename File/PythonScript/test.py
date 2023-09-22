import MetaTrader5 as mt5
from datetime import datetime
import pandas as pd

account = 51828929

mt5.initialize()
authorized=mt5.login(account)


# Get the list of available symbols.
#symbols = mt5.symbols_get()

#print(symbols)
# Get the list of open positions.
positions = mt5.positions_get()
for position in positions:
    print(position)


# Get the list of open orders.
orders = mt5.orders_get()
print(orders)

