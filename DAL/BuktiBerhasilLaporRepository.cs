using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class BuktiBerhasilLaporRepository : BaseRepository, IBuktiBerhasilLaporRepository
	{

		private readonly string tableBase = "bukti_berhasil_lapor";
		private readonly string tableGeneralSubCode = "sys_general_subcode";

		public async Task<List<BuktiBerhasilLapor>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string status)
		{
			var p = db.Symbol();

			string query =
				$@"
					select
						    	{tableBase}.id		        					as	ID
								,{tableBase}.finance_company_type				as	FinanceCompanyType
								,{tableBase}.periode_pelaporan					as	PeriodePelaporan
								,{tableBase}.tanggal_upload    					as  TanggalUpload 
								,{tableBase}.status								as	Status 
								,sgsfct.description								as  SysGeneralSubCodeFinanceCompanyType
											
					from
						{tableBase}
					left join
					{tableGeneralSubCode} as sgsfct on sgsfct.code = {tableBase}.finance_company_type
					where";
			if (status.ToLower() != "all")
			{
				query = query + $@" lower({tableBase}.status) = lower({p}Status)
						and
						";
			}
			query = query + $@"
						(
							lower(sgsfct.description)    			    like	lower({p}Keyword)	
							or	lower({tableBase}.periode_pelaporan)    			like	lower({p}Keyword)	
                            or	cast({tableBase}.tanggal_upload as varchar)    		like	lower({p}Keyword)	
							or	lower({tableBase}.status)          					like	lower({p}Keyword)
						)
					order by
						{tableBase}.mod_date DESC
					";
			query = QueryLimitOffset(query);

			return await _command.GetRows<BuktiBerhasilLapor>(transaction, query,
			 new
			 {
				 Keyword = $"%{keyword}%",
				 Limit = limit,
				 Offset = offset,
				 Status = status
			 });
		}
		public Task<List<BuktiBerhasilLapor>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}

		public async Task<BuktiBerhasilLapor> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								id		        					as		ID
								,code								as 		Code
								,company_code						as		CompanyCode
								,finance_company_type    			as  	FinanceCompanyType 
								,tanggal_pelaporan					as		TanggalPelaporan
								,tanggal_upload						as		TanggalUpload
								,periode_pelaporan	    			as 		PeriodePelaporan
								,month								as		Month
								,year								as      Year
								,file_name							as		FileName 
								,paths								as		Paths 
								,nama_pelapor						as		NamaPelapor 
								,notes								as		Notes 
								,is_backup							as		IsBackup     
								,is_active							as		IsActive  
								,is_auto_generate					as		IsAutoGenerate  
								,status								as		Status    					
									

							from
								{tableBase}
							where
								{tableBase}.id = {p}ID
										";


			return await _command.GetRow<BuktiBerhasilLapor>(transaction, query, new { ID = id });
		}
		public async Task<List<BuktiBerhasilLapor>> GetTop(IDbTransaction transaction, int limit)
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
			var result = await _command.GetRows<BuktiBerhasilLapor>(transaction, query, parameters);
			return result;
		}

		public async Task<int> Insert(IDbTransaction transaction, BuktiBerhasilLapor model)
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
										,tanggal_pelaporan			
										,tanggal_upload				
										,periode_pelaporan	    	
										,month						
										,year						
										,file_name					
										,paths						
										,nama_pelapor				
										,notes						
										,is_backup					
										,is_active					
										,is_auto_generate			
										,status						

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
										,{p}TanggalPelaporan
										,{p}TanggalUpload
										,{p}PeriodePelaporan
										,{p}Month
										,{p}Year
										,{p}FIleName 
										,{p}Paths 
										,{p}NamaPelapor 
										,{p}Notes 
										,{p}IsBackup     
										,{p}IsActive  
										,{p}IsAutoGenerate  
										,'HOLD' 			
									)
									
								";

			return await _command.Insert(transaction, query, model);
		}

		public async Task<int> UpdateByID(IDbTransaction transaction, BuktiBerhasilLapor model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								company_code					 = {p}CompanyCode
								,finance_company_type    		 = {p}FinanceCompanyType
								,tanggal_pelaporan				 = {p}TanggalPelaporan
								,tanggal_upload					 = {p}TanggalUpload
								,periode_pelaporan	    		 = {p}PeriodePelaporan
								,month							 = {p}Month
								,year							 = {p}Year
								,file_name						 = {p}FIleName 
								,paths							 = {p}Paths 
								,nama_pelapor					 = {p}NamaPelapor 
								,notes						     = {p}Notes 
								,is_backup					     = {p}IsBackup     
								,is_auto_generate			     = {p}IsAutoGenerate  
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
		public async Task<int> ChangeStatus(IDbTransaction transaction, BuktiBerhasilLapor model)
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
		public async Task<List<BuktiBerhasilLapor>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
                                 {tableBase}.id       			    as	ID
                                ,{tableBase}.finance_company_type   as 	FinanceCompanyType
                                ,{tableBase}.periode_pelaporan	    as  PeriodePelaporan      
								,{tableBase}.tanggal_upload	       	as  TanggalUpload  				
                    from
						        {tableBase}
					where
					{tableBase}.is_active = 1
					-- AND
					-- {tableBase}.status = 'ON PROCESS'
                    AND
                        (	
                                lower({tableBase}.finance_company_type)         like lower({p}Keyword)	
                            or  lower({tableBase}.periode_pelaporan)	    	like lower({p}Keyword)
							or  cast({tableBase}.tanggal_upload as varchar)    	like	lower({p}Keyword)
						)
                    order by
                                {tableBase}.mod_date DESC
					"
			);
			return await _command.GetRows<BuktiBerhasilLapor>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
			});
		}

	}
}

