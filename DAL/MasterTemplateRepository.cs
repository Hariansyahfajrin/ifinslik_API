using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class MasterTemplateRepository : BaseRepository, IMasterTemplateRepository
	{

		private readonly string tableBase = "master_template";
		private readonly string tableGeneralCode = "general_code";
		private readonly string tableGeneralSubCode = "sys_general_subcode";

		public async Task<List<MasterTemplate>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						    {tableBase}.id		        					as	ID
                            ,{tableBase}.finance_company_type		        as 	FinanceCompanyType
							,{tableBase}.form_type							as  FormType
							,{tableBase}.description	    				as 	Description
							,{tableBase}.is_active							as  IsActive
							,sgsfct.description								as  SysGeneralSubCodeFinanceCompanyType
							,sgsft.description								as  SysGeneralSubCodeFormType

												
					from
						{tableBase}
					left join
					{tableGeneralSubCode} as sgsfct on sgsfct.code = {tableBase}.finance_company_type
					left join
					{tableGeneralSubCode} as sgsft on sgsft.code = {tableBase}.form_type
					where
						(

                            lower(sgsfct.description)    				like	lower({p}Keyword)	
							or	lower(sgsft.description)          		like	lower({p}Keyword)
                            or	lower({tableBase}.description)          like	lower({p}Keyword)	
							   or case {tableBase}.is_active
									when 1 then 'yes'
									else		'no'
								end					    			like	lower({p}Keyword)	

						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<MasterTemplate>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}

		public async Task<MasterTemplate> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								{tableBase}.id		        		as	ID
								,{tableBase}.code		        	as 	Code
								,{tableBase}.finance_company_type	as  FinanceCompanyType
								,{tableBase}.form_type				as	FormType 
								,{tableBase}.description	    	as 	Description
								,{tableBase}.delimiter_start	    as 	DelimiterStart
								,{tableBase}.delimiter_center	    as 	DelimiterCenter
								,{tableBase}.delimiter_end	    	as 	DelimiterEnd
								,{tableBase}.is_active				as 	IsActive
								--
								,sgsft.description					as  SysGeneralSubCodeFormType

							from
								{tableBase}
							left join
							{tableGeneralSubCode} as sgsft on sgsft.code = {tableBase}.form_type
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<MasterTemplate>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}

		public async Task<List<MasterTemplate>> GetTop(IDbTransaction transaction, int limit)
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
			var result = await _command.GetRows<MasterTemplate>(transaction, query, parameters);
			return result;
		}
		public async Task<int> Insert(IDbTransaction transaction, MasterTemplate model)
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
													,finance_company_type
													,form_type	
													,description
													,delimiter_start
													,delimiter_center
													,delimiter_end
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
													,{p}Code
													,{p}FinanceCompanyType
													,{p}FormType
													,{p}Description
													,{p}DelimiterStart
													,{p}DelimiterCenter	
													,{p}DelimiterEnd
													,{p}IsActive  
      
												)
											";

			return await _command.Insert(transaction, query, model);
		}

		public async Task<int> UpdateByID(IDbTransaction transaction, MasterTemplate model)
		{
			var p = db.Symbol();

			string query = $@"
												update 
													{tableBase}
												set
												finance_company_type	 = {p}FinanceCompanyType
												,form_type	 	 		 = {p}FormType
												,description 		     = {p}Description
												,delimiter_start		 = {p}DelimiterStart
												,delimiter_center 		 = {p}DelimiterCenter	
												,delimiter_end 			 = {p}DelimiterEnd
												,mod_date       		 = {p}ModDate
												,mod_by					 = {p}ModBy
												,mod_ip_address 		 = {p}ModIPAddress
												  
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
		public async Task<int> ChangeStatus(IDbTransaction transaction, MasterTemplate model)
		{
			var p = db.Symbol();
			string query = $@"
                update {tableBase}
                  set
                      	is_active     	  = is_active * -1
					 	,mod_date         = {p}ModDate
						,mod_by			  = {p}ModBy
						,mod_ip_address   = {p}ModIPAddress
                  where
                      id = {p}ID";
			return await _command.Update(transaction, query, model);
		}

	}
}

