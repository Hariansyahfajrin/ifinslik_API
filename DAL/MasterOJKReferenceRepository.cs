using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class MasterOJKReferenceRepository : BaseRepository, IMasterOJKReferenceRepository
	{

		private readonly string tableBase = "master_ojk_reference";
		private readonly string tableSysGeneralSubCode = "sys_general_subcode";


		public async Task<List<MasterOJKReference>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string sysGeneralSubCodeID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						        id		        					as	ID
                                ,code		        				as 	Code
								,description	    				as 	Description
								,ojk_code							as  OJKCode
								,is_active							as 	IsActive
												
					from
						{tableBase}
				
					where
					 	{tableBase}.reference_type_id 	 = {p}SysGeneralSubCodeID
                    and
						(

                            lower(code)    			             	like	lower({p}Keyword)	
                            or	lower(description)          		like	lower({p}Keyword)	
							or	lower(ojk_code)          			like	lower({p}Keyword)	
							or case is_active
									when 1 then 'yes'
									else		'no'
								end					    			like	lower({p}Keyword)	

						)
					order by
						mod_date DESC
					"
			);

			return await _command.GetRows<MasterOJKReference>(transaction, query, new
			{
				SysGeneralSubCodeID = sysGeneralSubCodeID,
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}
		public Task<List<MasterOJKReference>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}


		public async Task<MasterOJKReference> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								 {tableBase}.id		        		as	ID
								,{tableBase}.code		        	as 	Code
								,{tableBase}.reference_type_id		as  ReferenceTypeID
								,{tableBase}.description	    	as 	Description
								,{tableBase}.ojk_code				as  OJKCode
								,{tableBase}.is_active				as 	IsActive
								--
								,{tableSysGeneralSubCode}.id			 		as 	SysGeneralSubCodeID	
                       			,{tableSysGeneralSubCode}.description   		as 	SysGeneralSubCodeDescription
								,{tableSysGeneralSubCode}.is_active				as 	SysGeneralSubCodeIsActive


							from
								{tableBase}
							inner join
							{tableSysGeneralSubCode} on {tableBase}.reference_type_id = {tableSysGeneralSubCode}.id
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<MasterOJKReference>(
				transaction, query, new { ID = id });

			return result;


			// return await _command.GetRow<MasterOJKReference>(transaction, query, new { ID = id });
		}

		public async Task<int> Insert(IDbTransaction transaction, MasterOJKReference model)
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
										,reference_type_id
										,ojk_code
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
										,{p}Code
										,{p}ReferenceTypeID	
										,{p}OJKCode
										,{p}Description  
										,{p}IsActive  
									)
								";

			return await _command.Insert(transaction, query, model);
		}

		public async Task<int> UpdateByID(IDbTransaction transaction, MasterOJKReference model)
		{
			var p = db.Symbol();

			string query = $@"
									update 
										{tableBase}
									set
									 description     = {p}Description   
									,ojk_code		 = {p}OJKCode 
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

		public async Task<int> ChangeStatus(IDbTransaction transaction, MasterOJKReference model)
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

		public async Task<int> ChangeStatusBySubCode(IDbTransaction transaction, SysGeneralSubCode sysGeneralSubCode)
		{
			var p = db.Symbol();
			string query = $@"
                update {tableBase}
                  set
                      is_active     	 = {p}IsActive
					  ,mod_date       	 = {p}ModDate
					  ,mod_by			 = {p}ModBy
					  ,mod_ip_address 	 = {p}ModIPAddress
                  where
                      reference_type_id	 = {p}ID";
			return await _command.Update(transaction, query, sysGeneralSubCode);
		}
		public async Task<List<MasterOJKReference>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? code)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
                                 {tableBase}.id       			    as	ID
								,{tableBase}.code   	       		as 	Code
                                ,{tableBase}.ojk_code   	        as 	OJKCode
                                ,{tableBase}.description	        as  Description       				
                    from
						        {tableBase}
                    inner join 
                                {tableSysGeneralSubCode} on {tableSysGeneralSubCode}.id = 
                                {tableBase}.reference_type_id	
					where
								{tableBase}.is_active = 1
					AND
                                {tableSysGeneralSubCode}.code          = {p}GeneralSubCodeCode
                    AND
                        (	
                                lower({tableBase}.ojk_code)         like lower({p}Keyword)	
                            or  lower({tableBase}.description)	    like lower({p}Keyword)
						)
                    order by
                                {tableBase}.mod_date DESC
					"
			);
			return await _command.GetRows<MasterOJKReference>(transaction, query, new
			{
				GeneralSubCodeCode = code,
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}


	}
}

