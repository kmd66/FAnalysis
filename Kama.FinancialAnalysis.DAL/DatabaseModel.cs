﻿using System;
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

#region GetPriceViewBases

public System.Data.SqlClient.SqlCommand GetCommand_GetPriceViewBases(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetPriceViewBases", 
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

public async Task<ResultSet> GetPriceViewBasesAsync(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetPriceViewBases(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetPriceViewBasesDapperAsync<T>(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetPriceViewBases",new {AType=_type,APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetPriceViewBases(byte? _type, int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetPriceViewBases(_type, _pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetMovingAverages

public System.Data.SqlClient.SqlCommand GetCommand_GetMovingAverages(int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetMovingAverages", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetMovingAveragesAsync(int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetMovingAverages(_pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetMovingAveragesDapperAsync<T>(int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetMovingAverages",new {APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetMovingAverages(int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetMovingAverages(_pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region GetStandardDeviations

public System.Data.SqlClient.SqlCommand GetCommand_GetStandardDeviations(int? _pageSize, int? _pageIndex, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGetStandardDeviations", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@APageSize", IsOutput = false, Value = _pageSize == null ? DBNull.Value : (object)_pageSize }, 
					new Parameter { Name = "@APageIndex", IsOutput = false, Value = _pageIndex == null ? DBNull.Value : (object)_pageIndex }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GetStandardDeviationsAsync(int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetStandardDeviations(_pageSize, _pageIndex, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GetStandardDeviationsDapperAsync<T>(int? _pageSize, int? _pageIndex, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGetStandardDeviations",new {APageSize=_pageSize,APageIndex=_pageIndex} , timeout );
}

public ResultSet GetStandardDeviations(int? _pageSize, int? _pageIndex, int? timeout = null)
{
	using(var cmd = GetCommand_GetStandardDeviations(_pageSize, _pageIndex, timeout))
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

#region GeEmptyWorkingHours

public System.Data.SqlClient.SqlCommand GetCommand_GeEmptyWorkingHours(int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spGeEmptyWorkingHours", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> GeEmptyWorkingHoursAsync(int? timeout = null)
{
	using(var cmd = GetCommand_GeEmptyWorkingHours(timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> GeEmptyWorkingHoursDapperAsync<T>(int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spGeEmptyWorkingHours",new {} , timeout );
}

public ResultSet GeEmptyWorkingHours(int? timeout = null)
{
	using(var cmd = GetCommand_GeEmptyWorkingHours(timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddWorkingHours

public System.Data.SqlClient.SqlCommand GetCommand_AddWorkingHours(int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddWorkingHours", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddWorkingHoursAsync(int? timeout = null)
{
	using(var cmd = GetCommand_AddWorkingHours(timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddWorkingHoursDapperAsync<T>(int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddWorkingHours",new {} , timeout );
}

public ResultSet AddWorkingHours(int? timeout = null)
{
	using(var cmd = GetCommand_AddWorkingHours(timeout))
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

#region AddMovingAverages

public System.Data.SqlClient.SqlCommand GetCommand_AddMovingAverages(string _json, int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddMovingAverages", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
					new Parameter { Name = "@AJson", IsOutput = false, Value = string.IsNullOrWhiteSpace(_json) ? DBNull.Value : (object)ReplaceArabicWithPersianChars(_json) }, 
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddMovingAveragesAsync(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddMovingAverages(_json, timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddMovingAveragesDapperAsync<T>(string _json, int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddMovingAverages",new {AJson=string.IsNullOrWhiteSpace(_json) ? _json : ReplaceArabicWithPersianChars(_json)} , timeout );
}

public ResultSet AddMovingAverages(string _json, int? timeout = null)
{
	using(var cmd = GetCommand_AddMovingAverages(_json, timeout))
{
	return new ResultSet(cmd, Execute(cmd), _modelValueBinder);
}
}

#endregion

#region AddAllStandardDeviation

public System.Data.SqlClient.SqlCommand GetCommand_AddAllStandardDeviation(int? timeout = null)
{
var cmd = base.CreateCommand("pbl.spAddAllStandardDeviation", 
	System.Data.CommandType.StoredProcedure, 
	new Parameter[]{
	});

			if (timeout != null)
				cmd.CommandTimeout = (int)timeout;
			return cmd;
}

public async Task<ResultSet> AddAllStandardDeviationAsync(int? timeout = null)
{
	using(var cmd = GetCommand_AddAllStandardDeviation(timeout))
{
	return new ResultSet(cmd, await ExecuteAsync(cmd), _modelValueBinder);
}
}

public async Task<AppCore.Result<IEnumerable<T>>> AddAllStandardDeviationDapperAsync<T>(int? timeout = null)
{
	return await  DapperQueryAsync<T>("pbl.spAddAllStandardDeviation",new {} , timeout );
}

public ResultSet AddAllStandardDeviation(int? timeout = null)
{
	using(var cmd = GetCommand_AddAllStandardDeviation(timeout))
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