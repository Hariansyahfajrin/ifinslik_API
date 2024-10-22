using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
    public class SysGeneralCodeRepository : BaseRepository, ISysGeneralCodeRepository
    {
       
       private readonly string tableSysGeneralCode = "sys_general_code";


	   public async Task<List<SysGeneralCode>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						    	id		        			as	ID
                            	,code		        		as 	Code
								,description	    		as 	Description
                            	,is_editable				as 	IsEditable
					from
						{tableSysGeneralCode}
					where
						(

                          	lower(code)          			        like	lower({p}Keyword)	
                            or	lower(description)          		like	lower({p}Keyword)		
                            or case is_editable
									when 1 then 'yes'
									else		'no'
								end					    			like	lower({p}Keyword)
						
						)
					order by
						mod_date DESC
					"
			);

			return await _command.GetRows<SysGeneralCode>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}

		public async Task<SysGeneralCode> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								id		        	as	ID
								,cre_date		    as 	CreDate
								,cre_by		        as 	CreBy
								,cre_ip_address		as 	CreIPAddress
								,mod_date		    as 	ModDate
								,mod_by		        as 	ModBy
								,mod_ip_address		as 	ModIPAddress
								,code		        as 	Code
								,description	    as 	Description
								,is_editable		as 	IsEditable

							from
								{tableSysGeneralCode}
							where
								id = {p}ID
										";
			return await _command.GetRow<SysGeneralCode>(transaction, query, new { ID = id });
		}


        public async Task<int> Insert(IDbTransaction transaction, SysGeneralCode model)
		{
			var p = db.Symbol();

			string query = $@"
								insert into {tableSysGeneralCode}
								(
									id
									,cre_date		
									,cre_by		    
									,cre_ip_address	
									,mod_date		
									,mod_by		    
									,mod_ip_address	
									,code
									,description
									,is_editable	    
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
									,{p}Description  
									,{p}IsEditable 
								)
							";

			return await _command.Insert(transaction, query, model);
		}

       public async Task<int> UpdateByID(IDbTransaction transaction, SysGeneralCode model)
		{
			var p = db.Symbol();

			string query = $@"
									update 
										{tableSysGeneralCode}
									set
									 description     = {p}Description     
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
								delete from {tableSysGeneralCode}
								where 
									id = {p}ID
								";

			return await _command.DeleteByID(transaction, query,id.ToString());
		}

		public async Task<int> ChangeStatus(IDbTransaction transaction, SysGeneralCode model)
		{
			var p = db.Symbol();

			string query = $@"
                update {tableSysGeneralCode}
                  set
                      is_editable          = is_editable * -1
                      --
                      ,mod_date           = {p}ModDate
                      ,mod_by             = {p}ModBy
                      ,mod_ip_address     = {p}ModIpAddress
                  where
                      id = {p}ID";
			return await _command.Update(transaction, query, model);
		}
    
    }
}

