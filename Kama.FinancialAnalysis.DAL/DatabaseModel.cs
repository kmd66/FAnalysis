using System;
using Kama.DatabaseModel;
using System.Threading.Tasks;
 using System.Collections.Generic;

namespace  Kama.FinancialAnalysis.DAL
{
public class PBL: Database
{
#region Constructors
public PBL(string connectionString)
	:base(connectionString){}

public PBL(string connectionString, IModelValueBinder modelValueBinder)
	:base(connectionString, modelValueBinder){}
#endregion

#region GetPriceViewBases

public System.Data.SqlClient.SqlCommand GetCommand_GetPriceViewBases(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetPriceViewBases", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@AFromDate", IsOutput = false, Value = _fromDate == null ? DBNull.Value : (object)_fromDate }, 
					new Parameter { Name = "@AToDate", IsOutput = false, Value = _toDate == null ? DBNull.Value : (object)_toDate }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetPriceViewBasesAsync(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetPriceViewBases(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetPriceViewBasesDapperAsync<T>(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetPriceViewBases",new {AType=_type,AFromDate=_fromDate,AToDate=_toDate} , timeout );
}

public ResultSet GetPriceViewBases(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetPriceViewBases(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddBiggerThanSDs

public System.Data.SqlClient.SqlCommand GetCommand_AddBiggerThanSDs(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddBiggerThanSDs", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddBiggerThanSDsAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddBiggerThanSDs(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddBiggerThanSDsDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddBiggerThanSDs",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddBiggerThanSDs(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddBiggerThanSDs(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListFromIdsMovingAverage

public System.Data.SqlClient.SqlCommand GetCommand_AddListFromIdsMovingAverage(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListFromIdsMovingAverage", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListFromIdsMovingAverageAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListFromIdsMovingAverage(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListFromIdsMovingAverageDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListFromIdsMovingAverage",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListFromIdsMovingAverage(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListFromIdsMovingAverage(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetBiggerThanSDs

public System.Data.SqlClient.SqlCommand GetCommand_GetBiggerThanSDs(byte? _type, byte? _session, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetBiggerThanSDs", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@ASession", IsOutput = false, Value = _session == null ? DBNull.Value : (object)_session }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetBiggerThanSDsAsync(byte? _type, byte? _session, int? timeout = null)
{
	using(var cmd = GetCommand_GetBiggerThanSDs(_type, _session, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetBiggerThanSDsDapperAsync<T>(byte? _type, byte? _session, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetBiggerThanSDs",new {AType=_type,ASession=_session} , timeout );
}

public ResultSet GetBiggerThanSDs(byte? _type, byte? _session, int? timeout = null)
{
	using(var cmd = GetCommand_GetBiggerThanSDs(_type, _session, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddBiggerThanSdOtherSymbols

public System.Data.SqlClient.SqlCommand GetCommand_AddBiggerThanSdOtherSymbols(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddBiggerThanSdOtherSymbols", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddBiggerThanSdOtherSymbolsAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddBiggerThanSdOtherSymbols(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddBiggerThanSdOtherSymbolsDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddBiggerThanSdOtherSymbols",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddBiggerThanSdOtherSymbols(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddBiggerThanSdOtherSymbols(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListRsi

public System.Data.SqlClient.SqlCommand GetCommand_AddListRsi(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListRsi", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListRsiAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListRsi(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListRsiDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListRsi",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListRsi(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListRsi(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetListRsi

public System.Data.SqlClient.SqlCommand GetCommand_GetListRsi(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetListRsi", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetListRsiAsync(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListRsi(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetListRsiDapperAsync<T>(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetListRsi",new {AType=_type,APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetListRsi(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListRsi(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetListCci

public System.Data.SqlClient.SqlCommand GetCommand_GetListCci(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetListCci", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetListCciAsync(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListCci(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetListCciDapperAsync<T>(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetListCci",new {AType=_type,APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetListCci(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListCci(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListCci

public System.Data.SqlClient.SqlCommand GetCommand_AddListCci(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListCci", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListCciAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListCci(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListCciDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListCci",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListCci(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListCci(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region ListMovingAverage

public System.Data.SqlClient.SqlCommand GetCommand_ListMovingAverage(byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spListMovingAverage", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> ListMovingAverageAsync(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_ListMovingAverage(_type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> ListMovingAverageDapperAsync<T>(byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spListMovingAverage",new {AType=_type} , timeout );
}

public ResultSet ListMovingAverage(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_ListMovingAverage(_type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetListIchimoku

public System.Data.SqlClient.SqlCommand GetCommand_GetListIchimoku(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetListIchimoku", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetListIchimokuAsync(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListIchimoku(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetListIchimokuDapperAsync<T>(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetListIchimoku",new {AType=_type,APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetListIchimoku(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListIchimoku(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetListBollingerBands

public System.Data.SqlClient.SqlCommand GetCommand_GetListBollingerBands(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetListBollingerBands", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetListBollingerBandsAsync(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListBollingerBands(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetListBollingerBandsDapperAsync<T>(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetListBollingerBands",new {AType=_type,APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetListBollingerBands(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListBollingerBands(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetListMacd

public System.Data.SqlClient.SqlCommand GetCommand_GetListMacd(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetListMacd", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetListMacdAsync(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListMacd(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetListMacdDapperAsync<T>(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetListMacd",new {AType=_type,APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetListMacd(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetListMacd(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListIchimoku

public System.Data.SqlClient.SqlCommand GetCommand_AddListIchimoku(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListIchimoku", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListIchimokuAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListIchimoku(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListIchimokuDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListIchimoku",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListIchimoku(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListIchimoku(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListBollingerBands

public System.Data.SqlClient.SqlCommand GetCommand_AddListBollingerBands(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListBollingerBands", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListBollingerBandsAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListBollingerBands(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListBollingerBandsDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListBollingerBands",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListBollingerBands(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListBollingerBands(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListStandardDeviation

public System.Data.SqlClient.SqlCommand GetCommand_AddListStandardDeviation(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListStandardDeviation", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListStandardDeviationAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListStandardDeviation(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListStandardDeviationDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListStandardDeviation",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListStandardDeviation(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListStandardDeviation(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GeEmptyWorkingHours

public System.Data.SqlClient.SqlCommand GetCommand_GeEmptyWorkingHours(byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGeEmptyWorkingHours", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GeEmptyWorkingHoursAsync(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GeEmptyWorkingHours(_type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GeEmptyWorkingHoursDapperAsync<T>(byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGeEmptyWorkingHours",new {AType=_type} , timeout );
}

public ResultSet GeEmptyWorkingHours(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GeEmptyWorkingHours(_type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListMacd

public System.Data.SqlClient.SqlCommand GetCommand_AddListMacd(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListMacd", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListMacdAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListMacd(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListMacdDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListMacd",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListMacd(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListMacd(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetSessions

public System.Data.SqlClient.SqlCommand GetCommand_GetSessions(int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetSessions", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetSessionsAsync(int? timeout = null)
{
	using(var cmd = GetCommand_GetSessions(timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetSessionsDapperAsync<T>(int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetSessions",new {} , timeout );
}

public ResultSet GetSessions(int? timeout = null)
{
	using(var cmd = GetCommand_GetSessions(timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddPriceMinutelys1

public System.Data.SqlClient.SqlCommand GetCommand_AddPriceMinutelys1(string _json, byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddPriceMinutelys1", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddPriceMinutelys1Async(string _json, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddPriceMinutelys1(_json, _type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddPriceMinutelys1DapperAsync<T>(string _json, byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddPriceMinutelys1",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json),AType=_type} , timeout );
}

public ResultSet AddPriceMinutelys1(string _json, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddPriceMinutelys1(_json, _type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListMovingAverage

public System.Data.SqlClient.SqlCommand GetCommand_AddListMovingAverage(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListMovingAverage", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListMovingAverageAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListMovingAverage(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListMovingAverageDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListMovingAverage",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListMovingAverage(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListMovingAverage(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddPriceMinutely

public System.Data.SqlClient.SqlCommand GetCommand_AddPriceMinutely(long? _id, DateTime? _date, float? _open, float? _close, float? _max, float? _min, byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddPriceMinutely", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@ADate", IsOutput = false, Value = _date == null ? DBNull.Value : (object)_date }, 
					new Parameter { Name = "@AOpen", IsOutput = false, Value = _open == null ? DBNull.Value : (object)_open }, 
					new Parameter { Name = "@AClose", IsOutput = false, Value = _close == null ? DBNull.Value : (object)_close }, 
					new Parameter { Name = "@AMax", IsOutput = false, Value = _max == null ? DBNull.Value : (object)_max }, 
					new Parameter { Name = "@AMin", IsOutput = false, Value = _min == null ? DBNull.Value : (object)_min }, 
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddPriceMinutelyAsync(long? _id, DateTime? _date, float? _open, float? _close, float? _max, float? _min, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddPriceMinutely(_id, _date, _open, _close, _max, _min, _type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddPriceMinutelyDapperAsync<T>(long? _id, DateTime? _date, float? _open, float? _close, float? _max, float? _min, byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddPriceMinutely",new {AID=_id,ADate=_date,AOpen=_open,AClose=_close,AMax=_max,AMin=_min,AType=_type} , timeout );
}

public ResultSet AddPriceMinutely(long? _id, DateTime? _date, float? _open, float? _close, float? _max, float? _min, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddPriceMinutely(_id, _date, _open, _close, _max, _min, _type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddZigZag

public System.Data.SqlClient.SqlCommand GetCommand_AddZigZag(long? _id, DateTime? _date, long? _approved, bool? _up, byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddZigZag", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AID", IsOutput = false, Value = _id == null ? DBNull.Value : (object)_id }, 
					new Parameter { Name = "@ADate", IsOutput = false, Value = _date == null ? DBNull.Value : (object)_date }, 
					new Parameter { Name = "@AApproved", IsOutput = false, Value = _approved == null ? DBNull.Value : (object)_approved }, 
					new Parameter { Name = "@AUp", IsOutput = false, Value = _up == null ? DBNull.Value : (object)_up }, 
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddZigZagAsync(long? _id, DateTime? _date, long? _approved, bool? _up, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddZigZag(_id, _date, _approved, _up, _type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddZigZagDapperAsync<T>(long? _id, DateTime? _date, long? _approved, bool? _up, byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddZigZag",new {AID=_id,ADate=_date,AApproved=_approved,AUp=_up,AType=_type} , timeout );
}

public ResultSet AddZigZag(long? _id, DateTime? _date, long? _approved, bool? _up, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddZigZag(_id, _date, _approved, _up, _type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddPriceMinutelys

public System.Data.SqlClient.SqlCommand GetCommand_AddPriceMinutelys(string _json, byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddPriceMinutelys", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddPriceMinutelysAsync(string _json, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddPriceMinutelys(_json, _type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddPriceMinutelysDapperAsync<T>(string _json, byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddPriceMinutelys",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json),AType=_type} , timeout );
}

public ResultSet AddPriceMinutelys(string _json, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddPriceMinutelys(_json, _type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetFromToZigZag

public System.Data.SqlClient.SqlCommand GetCommand_GetFromToZigZag(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetFromToZigZag", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@AFromDate", IsOutput = false, Value = _fromDate == null ? DBNull.Value : (object)_fromDate }, 
					new Parameter { Name = "@AToDate", IsOutput = false, Value = _toDate == null ? DBNull.Value : (object)_toDate }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetFromToZigZagAsync(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromToZigZag(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetFromToZigZagDapperAsync<T>(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetFromToZigZag",new {AType=_type,AFromDate=_fromDate,AToDate=_toDate} , timeout );
}

public ResultSet GetFromToZigZag(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromToZigZag(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetFromToIchimoku

public System.Data.SqlClient.SqlCommand GetCommand_GetFromToIchimoku(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetFromToIchimoku", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@AFromDate", IsOutput = false, Value = _fromDate == null ? DBNull.Value : (object)_fromDate }, 
					new Parameter { Name = "@AToDate", IsOutput = false, Value = _toDate == null ? DBNull.Value : (object)_toDate }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetFromToIchimokuAsync(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromToIchimoku(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetFromToIchimokuDapperAsync<T>(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetFromToIchimoku",new {AType=_type,AFromDate=_fromDate,AToDate=_toDate} , timeout );
}

public ResultSet GetFromToIchimoku(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromToIchimoku(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetFromToMacd

public System.Data.SqlClient.SqlCommand GetCommand_GetFromToMacd(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetFromToMacd", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@AFromDate", IsOutput = false, Value = _fromDate == null ? DBNull.Value : (object)_fromDate }, 
					new Parameter { Name = "@AToDate", IsOutput = false, Value = _toDate == null ? DBNull.Value : (object)_toDate }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetFromToMacdAsync(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromToMacd(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetFromToMacdDapperAsync<T>(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetFromToMacd",new {AType=_type,AFromDate=_fromDate,AToDate=_toDate} , timeout );
}

public ResultSet GetFromToMacd(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromToMacd(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetEmptysMovingAverage

public System.Data.SqlClient.SqlCommand GetCommand_GetEmptysMovingAverage(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetEmptysMovingAverage", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetEmptysMovingAverageAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_GetEmptysMovingAverage(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetEmptysMovingAverageDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetEmptysMovingAverage",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet GetEmptysMovingAverage(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_GetEmptysMovingAverage(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetLastPriceMinutely

public System.Data.SqlClient.SqlCommand GetCommand_GetLastPriceMinutely(byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetLastPriceMinutely", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetLastPriceMinutelyAsync(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GetLastPriceMinutely(_type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetLastPriceMinutelyDapperAsync<T>(byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetLastPriceMinutely",new {AType=_type} , timeout );
}

public ResultSet GetLastPriceMinutely(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GetLastPriceMinutely(_type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetFromToBollingerBands

public System.Data.SqlClient.SqlCommand GetCommand_GetFromToBollingerBands(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetFromToBollingerBands", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@AFromDate", IsOutput = false, Value = _fromDate == null ? DBNull.Value : (object)_fromDate }, 
					new Parameter { Name = "@AToDate", IsOutput = false, Value = _toDate == null ? DBNull.Value : (object)_toDate }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetFromToBollingerBandsAsync(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromToBollingerBands(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetFromToBollingerBandsDapperAsync<T>(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetFromToBollingerBands",new {AType=_type,AFromDate=_fromDate,AToDate=_toDate} , timeout );
}

public ResultSet GetFromToBollingerBands(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromToBollingerBands(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetEmptysStandardDeviation

public System.Data.SqlClient.SqlCommand GetCommand_GetEmptysStandardDeviation(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetEmptysStandardDeviation", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetEmptysStandardDeviationAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_GetEmptysStandardDeviation(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetEmptysStandardDeviationDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetEmptysStandardDeviation",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet GetEmptysStandardDeviation(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_GetEmptysStandardDeviation(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetPriceMinutelys

public System.Data.SqlClient.SqlCommand GetCommand_GetPriceMinutelys(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetPriceMinutelys", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetPriceMinutelysAsync(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetPriceMinutelys(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetPriceMinutelysDapperAsync<T>(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetPriceMinutelys",new {AType=_type,APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetPriceMinutelys(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetPriceMinutelys(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddWorkingHours

public System.Data.SqlClient.SqlCommand GetCommand_AddWorkingHours(byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddWorkingHours", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddWorkingHoursAsync(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddWorkingHours(_type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddWorkingHoursDapperAsync<T>(byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddWorkingHours",new {AType=_type} , timeout );
}

public ResultSet AddWorkingHours(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_AddWorkingHours(_type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddListZigZag

public System.Data.SqlClient.SqlCommand GetCommand_AddListZigZag(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddListZigZag", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddListZigZagAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListZigZag(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddListZigZagDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddListZigZag",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddListZigZag(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddListZigZag(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetLastWorkingHour

public System.Data.SqlClient.SqlCommand GetCommand_GetLastWorkingHour(int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetLastWorkingHour", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetLastWorkingHourAsync(int? timeout = null)
{
	using(var cmd = GetCommand_GetLastWorkingHour(timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetLastWorkingHourDapperAsync<T>(int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetLastWorkingHour",new {} , timeout );
}

public ResultSet GetLastWorkingHour(int? timeout = null)
{
	using(var cmd = GetCommand_GetLastWorkingHour(timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetFromTOPriceMinutelys

public System.Data.SqlClient.SqlCommand GetCommand_GetFromTOPriceMinutelys(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetFromTOPriceMinutelys", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@AFromDate", IsOutput = false, Value = _fromDate == null ? DBNull.Value : (object)_fromDate }, 
					new Parameter { Name = "@AToDate", IsOutput = false, Value = _toDate == null ? DBNull.Value : (object)_toDate }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetFromTOPriceMinutelysAsync(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromTOPriceMinutelys(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetFromTOPriceMinutelysDapperAsync<T>(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetFromTOPriceMinutelys",new {AType=_type,AFromDate=_fromDate,AToDate=_toDate} , timeout );
}

public ResultSet GetFromTOPriceMinutelys(byte? _type, DateTime? _fromDate, DateTime? _toDate, int? timeout = null)
{
	using(var cmd = GetCommand_GetFromTOPriceMinutelys(_type, _fromDate, _toDate, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetMovingAverages

public System.Data.SqlClient.SqlCommand GetCommand_GetMovingAverages(int? _pageSize, int? _pageIndex, byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetMovingAverages", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetMovingAveragesAsync(int? _pageSize, int? _pageIndex, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GetMovingAverages(_pageSize, _pageIndex, _type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetMovingAveragesDapperAsync<T>(int? _pageSize, int? _pageIndex, byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetMovingAverages",new {APageSize=_pageSize,APageIndex=_pageIndex,AType=_type} , timeout );
}

public ResultSet GetMovingAverages(int? _pageSize, int? _pageIndex, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GetMovingAverages(_pageSize, _pageIndex, _type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetStandardDeviations

public System.Data.SqlClient.SqlCommand GetCommand_GetStandardDeviations(int? _pageSize, int? _pageIndex, byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetStandardDeviations", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetStandardDeviationsAsync(int? _pageSize, int? _pageIndex, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GetStandardDeviations(_pageSize, _pageIndex, _type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetStandardDeviationsDapperAsync<T>(int? _pageSize, int? _pageIndex, byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetStandardDeviations",new {APageSize=_pageSize,APageIndex=_pageIndex,AType=_type} , timeout );
}

public ResultSet GetStandardDeviations(int? _pageSize, int? _pageIndex, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GetStandardDeviations(_pageSize, _pageIndex, _type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetBiggerThanSDBetweenID

public System.Data.SqlClient.SqlCommand GetCommand_GetBiggerThanSDBetweenID(long? _fromID, long? _toID, byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetBiggerThanSDBetweenID", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AFromID", IsOutput = false, Value = _fromID == null ? DBNull.Value : (object)_fromID }, 
					new Parameter { Name = "@AToID", IsOutput = false, Value = _toID == null ? DBNull.Value : (object)_toID }, 
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetBiggerThanSDBetweenIDAsync(long? _fromID, long? _toID, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GetBiggerThanSDBetweenID(_fromID, _toID, _type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetBiggerThanSDBetweenIDDapperAsync<T>(long? _fromID, long? _toID, byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetBiggerThanSDBetweenID",new {AFromID=_fromID,AToID=_toID,AType=_type} , timeout );
}

public ResultSet GetBiggerThanSDBetweenID(long? _fromID, long? _toID, byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_GetBiggerThanSDBetweenID(_fromID, _toID, _type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetDistanceMeasurements

public System.Data.SqlClient.SqlCommand GetCommand_GetDistanceMeasurements(byte? _type, byte? _session, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetDistanceMeasurements", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@ASession", IsOutput = false, Value = _session == null ? DBNull.Value : (object)_session }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetDistanceMeasurementsAsync(byte? _type, byte? _session, int? timeout = null)
{
	using(var cmd = GetCommand_GetDistanceMeasurements(_type, _session, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetDistanceMeasurementsDapperAsync<T>(byte? _type, byte? _session, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetDistanceMeasurements",new {AType=_type,ASession=_session} , timeout );
}

public ResultSet GetDistanceMeasurements(byte? _type, byte? _session, int? timeout = null)
{
	using(var cmd = GetCommand_GetDistanceMeasurements(_type, _session, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddDistanceMeasurements

public System.Data.SqlClient.SqlCommand GetCommand_AddDistanceMeasurements(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddDistanceMeasurements", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddDistanceMeasurementsAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddDistanceMeasurements(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddDistanceMeasurementsDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddDistanceMeasurements",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddDistanceMeasurements(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddDistanceMeasurements(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddAllMovingAverage

public System.Data.SqlClient.SqlCommand GetCommand_AddAllMovingAverage(int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddAllMovingAverage", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddAllMovingAverageAsync(int? timeout = null)
{
	using(var cmd = GetCommand_AddAllMovingAverage(timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddAllMovingAverageDapperAsync<T>(int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddAllMovingAverage",new {} , timeout );
}

public ResultSet AddAllMovingAverage(int? timeout = null)
{
	using(var cmd = GetCommand_AddAllMovingAverage(timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region ListBiggerThanSD

public System.Data.SqlClient.SqlCommand GetCommand_ListBiggerThanSD(byte? _type, byte? _session, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spListBiggerThanSD", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@ASession", IsOutput = false, Value = _session == null ? DBNull.Value : (object)_session }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> ListBiggerThanSDAsync(byte? _type, byte? _session, int? timeout = null)
{
	using(var cmd = GetCommand_ListBiggerThanSD(_type, _session, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> ListBiggerThanSDDapperAsync<T>(byte? _type, byte? _session, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spListBiggerThanSD",new {AType=_type,ASession=_session} , timeout );
}

public ResultSet ListBiggerThanSD(byte? _type, byte? _session, int? timeout = null)
{
	using(var cmd = GetCommand_ListBiggerThanSD(_type, _session, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region ListDistanceMeasurement

public System.Data.SqlClient.SqlCommand GetCommand_ListDistanceMeasurement(byte? _type, byte? _session, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spListDistanceMeasurement", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
					new Parameter { Name = "@ASession", IsOutput = false, Value = _session == null ? DBNull.Value : (object)_session }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> ListDistanceMeasurementAsync(byte? _type, byte? _session, int? timeout = null)
{
	using(var cmd = GetCommand_ListDistanceMeasurement(_type, _session, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> ListDistanceMeasurementDapperAsync<T>(byte? _type, byte? _session, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spListDistanceMeasurement",new {AType=_type,ASession=_session} , timeout );
}

public ResultSet ListDistanceMeasurement(byte? _type, byte? _session, int? timeout = null)
{
	using(var cmd = GetCommand_ListDistanceMeasurement(_type, _session, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddAllDxy

public System.Data.SqlClient.SqlCommand GetCommand_AddAllDxy(int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddAllDxy", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddAllDxyAsync(int? timeout = null)
{
	using(var cmd = GetCommand_AddAllDxy(timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddAllDxyDapperAsync<T>(int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddAllDxy",new {} , timeout );
}

public ResultSet AddAllDxy(int? timeout = null)
{
	using(var cmd = GetCommand_AddAllDxy(timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region ListStandardDeviation

public System.Data.SqlClient.SqlCommand GetCommand_ListStandardDeviation(byte? _type, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spListStandardDeviation", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AType", IsOutput = false, Value = _type == null ? DBNull.Value : (object)_type }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> ListStandardDeviationAsync(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_ListStandardDeviation(_type, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> ListStandardDeviationDapperAsync<T>(byte? _type, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spListStandardDeviation",new {AType=_type} , timeout );
}

public ResultSet ListStandardDeviation(byte? _type, int? timeout = null)
{
	using(var cmd = GetCommand_ListStandardDeviation(_type, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddStandardDeviation

public System.Data.SqlClient.SqlCommand GetCommand_AddStandardDeviation(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddStandardDeviation", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddStandardDeviationAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddStandardDeviation(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddStandardDeviationDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddStandardDeviation",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddStandardDeviation(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddStandardDeviation(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

}

}
