using System.Data;
using DAL.Helper;
using Domain.Abstract.Repository;
using Domain.Models;

namespace DAL
{
	public class MasterValidationRepository : BaseRepository, IMasterValidationRepository
	{
		private readonly string tableBase = "master_validation";

		#region GetRows
		public async Task<List<MasterValidation>> GetRows(IDbTransaction transaction, string? keyword, int offset, int limit)
		{
			string p = db.Symbol();
		
			string query = $@"
							select
								{tableBase}.id as ID
								,{tableBase}.code as Code
								,{tableBase}.finance_company_type as FinanceCompanyType
								,{tableBase}.form_type as FormType
								,{tableBase}.description as Description
								,{tableBase}.function_name as FunctionName
								,{tableBase}.is_active as IsActive
							from
								{tableBase}
							where
								(
									lower({tableBase}.code) like lower({p}Keyword)
									or lower({tableBase}.finance_company_type) like lower({p}Keyword)
									or lower({tableBase}.form_type) like lower({p}Keyword)
									or lower({tableBase}.description) like lower({p}Keyword)
									or lower({tableBase}.function_name) like lower({p}Keyword)
									or case {tableBase}.is_active
											when 1 then 'yes'
											else 'no'
										end like lower({p}Keyword)
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
		
			List<MasterValidation> result = await _command.GetRows<MasterValidation>(transaction, query, parameters);
		
			return result;
		}
		#endregion

		#region GetRowByID
		public async Task<MasterValidation> GetRowByID(IDbTransaction transaction, string id)
		{
			string p = db.Symbol();
		
			string query = $@"
							select
								{tableBase}.id as ID
								,{tableBase}.code as Code
								,{tableBase}.finance_company_type as FinanceCompanyType
								,{tableBase}.form_type as FormType
								,{tableBase}.description as Description
								,{tableBase}.function_name as FunctionName
								,{tableBase}.is_active as IsActive
							from
								{tableBase}
							where
								{tableBase}.id = {p}ID
					";
		
			object parameters = new
			{
				ID = id
			};
		
			MasterValidation result = await _command.GetRow<MasterValidation>(transaction, query, parameters);
		
			return result;
		}
		#endregion

		#region Insert
		public async Task<int> Insert(IDbTransaction transaction, MasterValidation model)
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
								,code
								,finance_company_type
								,form_type
								,description
								,function_name
								,is_active
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
								,{p}Code
								,{p}FinanceCompanyType
								,{p}FormType
								,{p}Description
								,{p}FunctionName
								,{p}IsActive
							)
					";
		
			return await _command.Insert(transaction, query, model);
		}
		#endregion

		#region UpdateByID
		public async Task<int> UpdateByID(IDbTransaction transaction, MasterValidation model)
		{
			string p = db.Symbol();
		
			string query = $@"update {tableBase}
							set
								code = {p}Code
								,finance_company_type = {p}FinanceCompanyType
								,form_type = {p}FormType
								,description = {p}Description
								,function_name = {p}FunctionName
								,is_active = {p}IsActive
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
		
		#region ChangeStatus
		public async Task<int> ChangeStatus(IDbTransaction transaction, MasterValidation model)
		{
			var p = db.Symbol();
		
			string query = $@"
				update {tableBase} 
				set
					is_active       = is_active * -1
					--
					,mod_date       = {p}ModDate
					,mod_by         = {p}ModBy
					,mod_ip_address = {p}ModIpAddress
				where
					id = {p}ID
			";
		
			return await _command.Update(transaction, query, model);
		}
		#endregion
	}
}