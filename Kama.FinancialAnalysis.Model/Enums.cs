
public enum SymbolType : byte
{
    Unknown = 0,

    //eurusd = 1,
    xauusd = 2,
    usdchf = 3,
    eurjpy = 4,

    //usdjpy = 6,
    //gbpusd = 7,
    //usdcad = 8,
    //usdsek = 9,


    nq100m = 10,
    DYX = 11,
}
public enum SessionType : byte
{
    Unknown = 0,

    eurusd = 1,
    xauusd = 2,
    usdchf = 3,
    eurjpy = 4,

    usdjpy = 6,
    gbpusd = 7,
    usdcad = 8,
    usdsek = 9,

    nq100m = 10,
    DYX = 11,


    newYork = 110,
    london = 111,
    sydney = 112,
}
public enum SessionOCType : byte
{
    Unknown = 0,

    dayOpen = 1,
    newYorkOpen = 10,
    londonOpen = 11,
    sydneyOpen = 12,

    newYorkClose = 20,
    londonClose = 21,
    sydneyClose = 22,
}

public enum PriceType : byte
{
    Unknown = 0,

    Highest = 1,
    Lowest = 2,
}

public enum TransactionType : byte
{
    Unknown = 0,

    Sell = 1,
    Buy = 2,
}
