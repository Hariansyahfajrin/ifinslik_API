using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class TxtStatusOJKRepository : BaseRepository, ITxtStatusOJKRepository
	{

		private readonly string tableBase = "txt_status_ojk";
		private readonly string tableGeneralSubCode = "sys_general_subcode";

		public async Task<List<TxtStatusOJK>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						         {tableBase}.id		        				as	ID
                                ,{tableBase}.code		        			as 	Code
								,{tableBase}.company_code	    			as 	CompanyCode
								,{tableBase}.date							as  Date
								,sgsfct.description							as  SysGeneralSubCodeFinanceCompanyType
												
					from
						{tableBase}
											left join
					{tableGeneralSubCode} as sgsfct on sgsfct.code = {tableBase}.finance_company_type
					where
						(

                            lower    ({tableBase}.code)    			             	like	lower({p}Keyword)	
                            or	lower({tableBase}.company_code)          		like	lower({p}Keyword)	
							or	lower(sgsfct.description)         				like	lower({p}Keyword)
							or	cast({tableBase}.date as varchar)          		like	lower({p}Keyword)
						
						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<TxtStatusOJK>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}


		public async Task<TxtStatusOJK> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								id		        					as		ID
								,code								as 		Code
								,company_code						as		CompanyCode
								,finance_company_type    			as  	FinanceCompanyType 
								,date								as		Date 	
								,date								as		PeriodeDate 			
							from
								{tableBase}
							where
								{tableBase}.id = {p}ID
										";


			return await _command.GetRow<TxtStatusOJK>(transaction, query, new { ID = id });
		}

		public async Task<List<TxtStatusOJK>> GetTop(IDbTransaction transaction, int limit)
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
			var result = await _command.GetRows<TxtStatusOJK>(transaction, query, parameters);
			return result;
		}
		public async Task<int> Insert(IDbTransaction transaction, TxtStatusOJK model)
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
									,{p}Date
								
								)
							";

			return await _command.Insert(transaction, query, model);
		}

		public async Task<int> UpdateByID(IDbTransaction transaction, TxtStatusOJK model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								finance_company_type    		 = {p}FinanceCompanyType
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

