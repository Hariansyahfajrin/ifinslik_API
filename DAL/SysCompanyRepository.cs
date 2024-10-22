using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
    public class SysCompanyRepository : BaseRepository, ISysCompanyRepository
    {
       
       private readonly string tableBase= "sys_company";

	     public async Task<List<SysCompany>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						    id		        		as	ID
                            ,code					as 	Code
							,name	   				as 	Name
							,idpp					as  Idpp
							,sandi_kantor_cabang	as  SandiKantorCabang
    
					from
						{tableBase}
					where
						(

                            lower (code)          			like	lower({p}Keyword)	
                            or	lower(name)          		like	lower({p}Keyword)	
							or	lower(idpp)          		like	lower({p}Keyword)	
							or	lower(sandi_kantor_cabang)          		like	lower({p}Keyword)	
                           
						)
					order by
						mod_date DESC
					"
			);

			return await _command.GetRows<SysCompany>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}

		public async Task<SysCompany> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								id		        			as	ID
								,code      					as 	Code
								,name						as 	Name
								,idpp						as 	Idpp
								,sandi_kantor_cabang		as 	SandiKantorCabang


							from
								{tableBase}
							where
								id = {p}ID
										";
			return await _command.GetRow<SysCompany>(transaction, query, new { ID = id });
		}
    

      
        public async Task<int> Insert(IDbTransaction transaction, SysCompany model)
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
									,name    
									,idpp
									,sandi_kantor_cabang 
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
									,{p}Name
									,{p}Idpp
									,{p}SandiKantorCabang
								)
							";

			return await _command.Insert(transaction, query, model);
		}

       public async Task<int> UpdateByID(IDbTransaction transaction, SysCompany model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								code      			 				= {p}Code
								,name	  			 				= {p}Name
								,idpp	  			 				= {p}Idpp
								,sandi_kantor_cabang				= {p}SandiKantorCabang
								,mod_date        	 				= {p}ModDate
								,mod_by			 	 				= {p}ModBy
								,mod_ip_address  	 				= {p}ModIPAddress
								  
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

			return await _command.DeleteByID(transaction, query,id.ToString());
		}
		public async Task<List<SysCompany>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit)
        {
            var p = db.Symbol();

            string query = QueryLimitOffset(
                $@"
					select
                                 {tableBase}.id       			    as	ID
                                ,{tableBase}.code   	            as 	Code
                                ,{tableBase}.name	        		as  Name       				
                    from
						        {tableBase}
					where
                        (	
                                lower({tableBase}.code)         like lower({p}Keyword)	
                            or  lower({tableBase}.name)	    	like lower({p}Keyword)
						)
                    order by
                                {tableBase}.mod_date DESC
					"
            );
            return await _command.GetRows<SysCompany>(transaction, query, new
            {
                Keyword = $"%{keyword}%",
                Limit = limit,
                Offset = offset
            });
        }
    
    }

}

