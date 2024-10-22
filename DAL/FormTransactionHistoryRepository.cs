using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class FormTransactionHistoryRepository : BaseRepository, IFormTransactionHistoryRepository
	{

		private readonly string tableBase = "form_transaction_history";
		private readonly string tableGeneralSubCode = "sys_general_subcode";

		public async Task<List<FormTransactionHistory>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formType)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
					
					select
						        	 {tableBase}.id		        				as	ID
									,{tableBase}.finance_company_type			as  FinanceCompanyType
									,{tableBase}.form_type						as  FormType
									,{tableBase}.date							as  Date
									,sgsfct.description							as  SysGeneralSubCodeFinanceCompanyType
									,sgsft.description							as  SysGeneralSubCodeFormType

												
					from
						{tableBase}
					left join
					{tableGeneralSubCode} as sgsfct on sgsfct.code = {tableBase}.finance_company_type
					left join
					{tableGeneralSubCode} as sgsft on sgsft.code = {tableBase}.form_type
					where
						{tableBase}.form_type = {p}FormType
					and
						(

                            lower(sgsfct.description)    						like	lower({p}Keyword)	
							or	lower(sgsft.description)          				like	lower({p}Keyword)
							or	cast ({tableBase}.date as varchar)          	like	lower({p}Keyword)
						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<FormTransactionHistory>(transaction, query,
			 new
			 {
				 Keyword = $"%{keyword}%",
				 Limit = limit,
				 Offset = offset,
				 FormType = formType
			 });
		}
		public async Task<List<FormTransactionHistory>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
					
					select
						        	 {tableBase}.id		        				as	ID
									,{tableBase}.finance_company_type			as  FinanceCompanyType
									,{tableBase}.form_type						as  FormType
									,{tableBase}.date							as  Date
									,sgsfct.description							as  SysGeneralSubCodeDescription
									,sgsft.description							as  SysGeneralSubCodeDescription

												
					from
						{tableBase}
					left join
					{tableGeneralSubCode} as sgsfct on sgsfct.code = {tableBase}.finance_company_type
					left join
					{tableGeneralSubCode} as sgsft on sgsft.code = {tableBase}.form_type
					where
						(

                            lower(sgsfct.description)    						like	lower({p}Keyword)	
							or	lower(sgsft.description)          				like	lower({p}Keyword)
							or	cast ({tableBase}.date as varchar)          	like	lower({p}Keyword)
						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<FormTransactionHistory>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}

		public async Task<FormTransactionHistory> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								{tableBase}.id		        			as	ID
								,{tableBase}.code						as 	Code
								,{tableBase}.company_code				as	CompanyCode
								,{tableBase}.finance_company_type    	as  FinanceCompanyType 
								,{tableBase}.form_type					as 	FormType
								,{tableBase}.date						as	Date 				
								--
								,sgsft.description						as  SysGeneralSubCodeFormType
	
 
							from
								{tableBase}
							left join
							{tableGeneralSubCode} as sgsft on sgsft.code = {tableBase}.form_type
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<FormTransactionHistory>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}
		public async Task<List<FormTransactionHistory>> GetTop(IDbTransaction transaction, int limit)
		{
			string query = QueryLimit(
			  $@"
				select
          {tableBase}.code        as Code
				from 
          {tableBase}
				order by
          {tableBase}.cre_date desc
				"
			);

			var parameters = new
			{
				Limit = limit
			};
			var result = await _command.GetRows<FormTransactionHistory>(transaction, query, parameters);
			return result;
		}


		public async Task<int> Insert(IDbTransaction transaction, FormTransactionHistory model)
		{
			var p = db.Symbol();

			string query = $@"
									insert into {tableBase}
									(
										id
										,cre_date		
										,cre_by		    
										,cre_ip_address	
										,mod_date		
										,mod_by		    
										,mod_ip_address	
										,code								
										,company_code				
										,finance_company_type   
										,form_type 	
										,date						
														
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
										,{p}Code
										,{p}CompanyCode
										,{p}FinanceCompanyType 
										,{p}FormType
										,{p}Date
									
									)
								";

			return await _command.Insert(transaction, query, model);
		}
		public async Task<int> UpdateByID(IDbTransaction transaction, FormTransactionHistory model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								company_code					 = {p}CompanyCode
								,finance_company_type    		 = {p}FinanceCompanyType
								,form_type						 = {p}FormType
								,date							 = {p}Date
								,mod_date       				 = {p}ModDate
								,mod_by							 = {p}ModBy
								,mod_ip_address 				 = {p}ModIPAddress		
								
								where
										id = {p}ID
							";

			return await _command.Update(transaction, query, model);
		}

		public async Task<int> DeleteByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			string query = $@"
												delete from {tableBase}
												where 
													id = {p}ID
												";

			return await _command.DeleteByID(transaction, query, id.ToString());
		}


	}
}

