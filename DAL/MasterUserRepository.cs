using System.Data;
using DAL.Helper;
using Domain.Abstract.Repository;
using Domain.Models;

namespace DAL
{
	public class MasterUserRepository : BaseRepository, IMasterUserRepository
	{
		private readonly string tableBase = "master_user";

		#region GetRows
		public async Task<List<MasterUser>> GetRows(IDbTransaction transaction, string? keyword, int offset, int limit)
		{
			string p = db.Symbol();
		
			string query = $@"
							select
								{tableBase}.id as ID
								,{tableBase}.employee_code as EmployeeCode
								,{tableBase}.employee_name as EmployeeName
							from
								{tableBase}
							where
								(
									lower({tableBase}.employee_code) like lower({p}Keyword)
									or lower({tableBase}.employee_name) like lower({p}Keyword)
								)
							order by
								{tableBase}.mod_date desc
					";
		
			query = QueryLimitOffset(query);
		
			object parameters = new
			{
				Keyword = $"%{keyword}%",
				Offset = offset,
				Limit = limit
			};
		
			List<MasterUser> result = await _command.GetRows<MasterUser>(transaction, query, parameters);
		
			return result;
		}
		#endregion

		#region GetRowByID
		public async Task<MasterUser> GetRowByID(IDbTransaction transaction, string id)
		{
			string p = db.Symbol();
		
			string query = $@"
							select
								{tableBase}.id as ID
								,{tableBase}.employee_code as EmployeeCode
								,{tableBase}.employee_name as EmployeeName
							from
								{tableBase}
							where
								{tableBase}.id = {p}ID
					";
		
			object parameters = new
			{
				ID = id
			};
		
			MasterUser result = await _command.GetRow<MasterUser>(transaction, query, parameters);
		
			return result;
		}
		#endregion

		#region Insert
		public async Task<int> Insert(IDbTransaction transaction, MasterUser model)
		{
			string p = db.Symbol();
		
			string query = $@"insert into {tableBase}
							(
								id
								,cre_date
								,cre_by
								,cre_ip_address
								,mod_date
								,mod_by
								,mod_ip_address
								--
								,employee_code
								,employee_name
							)
							values
							(
								{p}ID
								,{p}CreDate
								,{p}CreBy
								,{p}CreIPAddress
								,{p}ModDate
								,{p}ModBy
								,{p}ModIPAddress
								--
								,{p}EmployeeCode
								,{p}EmployeeName
							)
					";
		
			return await _command.Insert(transaction, query, model);
		}
		#endregion

		#region UpdateByID
		public async Task<int> UpdateByID(IDbTransaction transaction, MasterUser model)
		{
			string p = db.Symbol();
		
			string query = $@"update {tableBase}
							set
								employee_code = {p}EmployeeCode
								,employee_name = {p}EmployeeName
								--
								,mod_date = {p}ModDate
								,mod_by = {p}ModBy
								,mod_ip_address = {p}ModIPAddress
							where
								id = {p}ID
										";
		
			return await _command.Update(transaction, query, model);
		}
		#endregion

		#region DeleteByID
		public async Task<int> DeleteByID(IDbTransaction transaction, string id)
		{
			string p = db.Symbol();
		
			string query = $"delete from {tableBase} where id = {p}ID";
		
			return await _command.DeleteByID(transaction, query, id);
		}
		#endregion
		
	}
}