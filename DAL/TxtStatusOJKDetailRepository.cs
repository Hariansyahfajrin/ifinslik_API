using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class TxtStatusOJKDetailRepository : BaseRepository, ITxtStatusOJKDetailRepository
	{

		private readonly string tableBase = "txt_status_ojk_detail";
		private readonly string tableTxtStatusOJK = "txt_status_ojk";
		private readonly string tableForm = "form_transaction";
		private readonly string tableGeneralSubCode = "sys_general_subcode";

		public async Task<List<TxtStatusOJKDetail>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string txtStatusOJKID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						            {tableBase}.id		        				as	ID
                                    ,{tableBase}.txt_status_ojk_id		        as 	TxtStatusOJKID
									,{tableBase}.finance_company_type			as  FinanceCompanyType
									,{tableBase}.form_type	    				as 	FormType
									,{tableBase}.description	    			as 	Description
									,{tableBase}.is_valid						as  IsValid
									,sgsft.description							as  SysGeneralSubCodeFormType
								
					from
						{tableBase}
					left join
					{tableGeneralSubCode} as sgsft on sgsft.code = {tableBase}.form_type
					where
                                {tableBase}.txt_status_ojk_id         = {p}TxtStatusOJKID
					and
						(
								lower(sgsft.description)          				like	lower({p}Keyword)
							or	lower({tableBase}.description)          		like	lower({p}Keyword)
							or	cast({tableBase}.is_valid as varchar)          			like	lower({p}Keyword)
						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<TxtStatusOJKDetail>(transaction, query,
			new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				TxtStatusOJKID = txtStatusOJKID
			});
		}


		public async Task<TxtStatusOJKDetail> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select
								{tableBase}.id		        				as	ID
								,{tableBase}.txt_status_ojk_id				as 	TxtStatusOJKID
								,{tableBase}.form_type						as	FormType
								,{tableBase}.finance_company_type    		as  FinanceCompanyType 
								,{tableBase}.description					as	Description
								,{tableBase}.is_valid						as	IsValid 		
								--
								,{tableTxtStatusOJK}.id			 			as 	TxtStatusOJKID	
                       			,{tableTxtStatusOJK}.code 					as 	TxtStatusOJKCode
								,{tableTxtStatusOJK}.company_code 			as 	TxtStatusOJKCompanyCode			
								,{tableTxtStatusOJK}.date 					as 	TxtStatusOJKDate
							from
								{tableBase}
							inner join
							{tableTxtStatusOJK} on {tableBase}.txt_status_ojk_id = {tableTxtStatusOJK}.id
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<TxtStatusOJKDetail>(
				transaction, query, new { ID = id });

			return result;


		}

		public async Task<int> Insert(IDbTransaction transaction, TxtStatusOJKDetail model)
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
													,txt_status_ojk_id								
													,form_type				
													,finance_company_type    
													,description	
													,is_valid							
																	
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
													,{p}TxtStatusOJKID
													,{p}FormType
													,{p}FinanceCompanyType
													,{p}Description 
													,{p}IsValid
												
												)
											";

			return await _command.Insert(transaction, query, model);
		}

		public async Task<int> UpdateByID(IDbTransaction transaction, TxtStatusOJKDetail model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								form_type						 = {p}FormType
								,finance_company_type    		 = {p}FinanceCompanyType
								,description					 = {p}Description
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

		public Task<List<TxtStatusOJKDetail>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

