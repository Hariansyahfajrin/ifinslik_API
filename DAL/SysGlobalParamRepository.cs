using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
    public class SysGlobalParamRepository : BaseRepository, ISysGlobalParamRepository
    {
       
       private readonly string tableSysGlobalParam = "sys_global_param";

	   public async Task<List<SysGlobalParam>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						        id		        				as	ID
                                ,code		        			as 	Code
								,description	    			as 	Description
                                ,value		        			as 	Value
                                ,is_editable					as 	IsEditable
					from
						{tableSysGlobalParam}
					where
						(

                            lower (code)          			           	 like	lower({p}Keyword)	
                            or  lower	(description)          		     like	lower({p}Keyword)	
                            or	lower	(value)          			     like	lower({p}Keyword)	
                            or case is_editable
									when 1 then 'yes'
									else		'no'
								end					    				 like	lower({p}Keyword)
						
						)
					order by
						mod_date DESC
					"
			);

			return await _command.GetRows<SysGlobalParam>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}

		public async Task<SysGlobalParam> GetRowByID(IDbTransaction transaction, string id)
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
								,value		        as 	Value
								,is_editable		as 	IsEditable

							from
								{tableSysGlobalParam}
							where
								id = {p}ID
										";
			return await _command.GetRow<SysGlobalParam>(transaction, query, new { ID = id });
		}
    

        public async Task<int> Insert(IDbTransaction transaction, SysGlobalParam model)
		{
			var p = db.Symbol();

			string query = $@"
								insert into {tableSysGlobalParam}
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
									,value
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
									,{p}Value 
									,{p}IsEditable 
								)
							";

			return await _command.Insert(transaction, query, model);
		}

       public async Task<int> UpdateByID(IDbTransaction transaction, SysGlobalParam model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableSysGlobalParam}
								set
								 description     = {p}Description     
                                ,value           = {p}Value
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
								delete from {tableSysGlobalParam}
								where 
									id = {p}ID
								";

			return await _command.DeleteByID(transaction, query,id.ToString());
		}

		public async Task<int> ChangeEditableStatus(IDbTransaction transaction, SysGlobalParam model)
		{
			var p = db.Symbol();

			string query = $@"
                update {tableSysGlobalParam}
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

