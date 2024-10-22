using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class SysGeneralSubCodeRepository : BaseRepository, ISysGeneralSubCodeRepository
	{

		private readonly string tableBase = "sys_general_subcode";
		private readonly string tableSysGeneralCode = "sys_general_code";

		public async Task<List<SysGeneralSubCode>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string sysGeneralCodeID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						                        id		        	as	ID
                                                ,code		        as 	Code
												,description	    as 	Description
												,order_key			as  OrderKey
                                                ,is_active			as 	IsActive
												
					from
						{tableBase}
				
					where
					 	{tableBase}.sys_general_code_id     	 = {p}SysGeneralCodeID
                    and
						(

                            lower(code)    			             	like	lower({p}Keyword)	
                            or	lower(description)          		like	lower({p}Keyword)
							or	cast(order_key as varchar)          like	lower({p}Keyword)		
                            or case is_active
									when 1 then 'yes'
									else		'no'
							end					    				like	lower({p}Keyword)
						
						)
					order by
						mod_date DESC
					"
			);

			return await _command.GetRows<SysGeneralSubCode>(transaction, query, new
			{
				SysGeneralCodeID = sysGeneralCodeID,
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}

		public Task<List<SysGeneralSubCode>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}

		public async Task<SysGeneralSubCode> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								 {tableBase}.id		        				as	ID
								,{tableBase}.sys_general_code_id			as  SysGeneralCodeID
								,{tableBase}.code		        			as 	Code
								,{tableBase}.order_key						as	Orderkey 
								,{tableBase}.description	    			as 	Description
								,{tableBase}.is_active						as 	IsActive
								--
								,{tableSysGeneralCode}.id			 		as 	SysGeneralCodeID	
                       			,{tableSysGeneralCode}.description   		as 	SysGeneralCodeDescription
								,{tableSysGeneralCode}.is_editable 			as 	SysGeneralCodeIsEditable

							from
								{tableBase}
							inner join
							{tableSysGeneralCode} on {tableBase}.sys_general_code_id = {tableSysGeneralCode}.id
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<SysGeneralSubCode>(
				transaction, query, new { ID = id });

			return result;

			//  return await _command.GetRow<SysGeneralSubCode>(transaction, query, new { ID = id });
		}
		public async Task<SysGeneralSubCode> GetRowByCode(IDbTransaction transaction, string? code)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								 {tableBase}.id		        				      as	ID
								,{tableBase}.sys_general_code_id		    as  SysGeneralCodeID
								,{tableBase}.code		        			      as 	Code
								,{tableBase}.order_key						      as	Orderkey 
								,{tableBase}.description	    			    as 	Description
								,{tableBase}.is_active						      as 	IsActive
								--
								,{tableSysGeneralCode}.id			 		      as 	SysGeneralCodeID	
                ,{tableSysGeneralCode}.description   		as 	SysGeneralCodeDescription
								,{tableSysGeneralCode}.is_editable 			as 	SysGeneralCodeIsEditable

							from
								{tableBase}
							inner join
							{tableSysGeneralCode} on {tableBase}.sys_general_code_id = {tableSysGeneralCode}.id
							where
								{tableBase}.code = {p}Code
										";

			var result = await _command.GetRow<SysGeneralSubCode>(
				transaction, query, new { Code = code });

			return result;

			//  return await _command.GetRow<SysGeneralSubCode>(transaction, query, new { ID = id });
		}


		public async Task<int> Insert(IDbTransaction transaction, SysGeneralSubCode model)
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
													,sys_general_code_id
													,code
													,order_key
													,description
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
													,{p}SysGeneralCodeID
													,{p}Code
													,{p}OrderKey	
													,{p}Description  
													,{p}IsActive  
      
												)
											";

			return await _command.Insert(transaction, query, model);
		}

		public async Task<int> UpdateByID(IDbTransaction transaction, SysGeneralSubCode model)
		{
			var p = db.Symbol();

			string query = $@"
												update 
													{tableBase}
												set
												 description     = {p}Description    
												,order_key     = {p}OrderKey  
												,mod_date        = {p}ModDate
												,mod_by			 = {p}ModBy
												,mod_ip_address  = {p}ModIPAddress
												  
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

		public async Task<int> ChangeStatus(IDbTransaction transaction, SysGeneralSubCode model)
		{
			var p = db.Symbol();

			string query = $@"
                update {tableBase}
                  set
                     	 is_active     	  = is_active * -1
					 	,mod_date        = {p}ModDate
						,mod_by			 = {p}ModBy
						,mod_ip_address  = {p}ModIPAddress
                  where
                      id = {p}ID";
			return await _command.Update(transaction, query, model);
		}
		public async Task<List<SysGeneralSubCode>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? code)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
                                 {tableBase}.id       			    as	ID
                                ,{tableBase}.code   	            as 	Code
                                ,{tableBase}.description	        as  Description       				
                    from
						        {tableBase}
                    inner join 
                                {tableSysGeneralCode} on {tableSysGeneralCode}.id = 
                                {tableBase}.sys_general_code_id 
					where
								{tableBase}.is_active = 1
					AND
                                {tableSysGeneralCode}.code          = {p}GeneralCodeCode
                    AND
                        (	
                                lower({tableBase}.code)             like lower({p}Keyword)	
                            or  lower({tableBase}.description)	    like lower({p}Keyword)
						)
                    order by
                                {tableBase}.mod_date DESC
					"
			);
			return await _command.GetRows<SysGeneralSubCode>(transaction, query, new
			{
				GeneralCodeCode = code,
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}


	}
}

