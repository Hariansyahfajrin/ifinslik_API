using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class P01HistoryRepository : BaseRepository, IP01HistoryRepository
	{

		private readonly string tableBase = "p01_history";
		private readonly string tableCompany = "sys_company";
		private readonly string tableD01 = "d01_history";
		private readonly string tableD02 = "d02_history";

		public async Task<List<P01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
	 				
					select

						     {tableBase}.id		        									as	ID  	             	
							,{tableBase}.nomor_identitas 									as 	NomorIdentitas       	         		
							,{tableBase}.nomor_rekening 									as 	NomorRekening        	         				
							,{tableBase}.cif												as 	Cif      	                     						          																					
							,{tableBase}.nama_penjamin										as 	NamaPenjamin                      																		
							,{tableBase}.nama_lengkap_penjamin								as 	NamaLengkapPenjamin               																		               											
							,{tableBase}.presentase_dijamin 								as 	PresentaseDijamin     
							,case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end as NamaLengkap
					from
						{tableBase}
					left join {tableD01} as d1 on d1.cif = {tableBase}.cif
           			left join {tableD02} as d2 on d2.cif = {tableBase}.cif               																						               												        				
					where
                                {tableBase}.form_transaction_history_id          = {p}FormTransactionHistoryID
                    AND
						(						
							lower   ({tableBase}.nomor_identitas) 									like	lower({p}Keyword)															
							or lower({tableBase}.nomor_rekening) 									like	lower({p}Keyword)												
							or lower({tableBase}.cif)												like	lower({p}Keyword)																													
							or lower({tableBase}.nama_penjamin)										like	lower({p}Keyword)															
							or lower({tableBase}.nama_lengkap_penjamin)								like	lower({p}Keyword)																										
							or cast ({tableBase}.presentase_dijamin as varchar) 						like	lower({p}Keyword)	
							or lower(case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end) like lower({p}Keyword)																													

						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<P01History>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				FormTransactionHistoryID = formTransactionHistoryID
			});
		}
		public async Task<P01History> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

									{tableBase}.id		        								as	ID		        								        						
									,{tableBase}.form_transaction_history_id 					as	FormTransactionHistoryID                 
									,{tableBase}.flag_detail									as	FlagDetail        	             	
									,{tableBase}.nomor_identitas 								as	NomorIdentitas       	         		
									,{tableBase}.nomor_rekening 								as	NomorRekening        	         		
									,{tableBase}.cif											as	Cif      	                     		
									,{tableBase}.kode_jenis_segmen_fasilitas					as	KodeJenisSegmenFasilitas     	 		
									,{tableBase}.kode_jenis_segmen_fasilitas_ojk_code			as	KodeJenisSegmenFasilitasOjkCode   		
									,{tableBase}.kode_jenis_segmen_fasilitas_desc				as	KodeJenisSegmenFasilitasDesc     		
									,{tableBase}.kode_jenis_identitas							as	KodeJenisIdentitas       	     		
									,{tableBase}.kode_jenis_identitas_ojk_code					as	KodeJenisIdentitasOjkCode         		
									,{tableBase}.kode_jenis_identitas_desc						as	KodeJenisIdentitasDesc            		
									,{tableBase}.nama_penjamin									as	NamaPenjamin                      		
									,{tableBase}.nama_lengkap_penjamin							as	NamaLengkapPenjamin               		
									,{tableBase}.kode_golongan_penjamin							as	KodeGolonganPenjamin     	     		
									,{tableBase}.kode_golongan_penjamin_ojk_code				as	KodeGolonganPenjaminOjkCode      		
									,{tableBase}.kode_golongan_penjamin_desc					as	KodeGolonganPenjaminDesc          		
									,{tableBase}.alamat_penjamin 								as	AlamatPenjamin                    			
									,{tableBase}.presentase_dijamin 							as	PresentaseDijamin                    		
									,{tableBase}.keterangan										as	Keterangan       	             		
									,{tableBase}.kode_kantor_cabang								as	KodeKantorCabang                  		
									,{tableBase}.operasi_data									as	OperasiData      	             		
									,{tableBase}.operasi_data_ojk_code							as	OperasiDataOjkCode                		
									,{tableBase}.operasi_data_desc								as	OperasiDataDesc                   		
									,{tableBase}.period											as	Period                            								
									,sckc.name										as  SysCompanyKantorCabang

							from
								{tableBase}
							left join
							{tableCompany} as sckc on sckc.code = {tableBase}.kode_kantor_cabang	
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<P01History>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}


		public async Task<int> Insert(IDbTransaction transaction, P01History model)
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
										,form_transaction_history_id 						
										,flag_detail								
										,nomor_identitas 							
										,nomor_rekening 							
										,cif										
										,kode_jenis_segmen_fasilitas					
										,kode_jenis_segmen_fasilitas_ojk_code		
										,kode_jenis_segmen_fasilitas_desc			
										,kode_jenis_identitas						
										,kode_jenis_identitas_ojk_code				
										,kode_jenis_identitas_desc					
										,nama_penjamin								
										,nama_lengkap_penjamin						
										,kode_golongan_penjamin						
										,kode_golongan_penjamin_ojk_code			
										,kode_golongan_penjamin_desc				
										,alamat_penjamin 							
										,presentase_dijamin 			
										,keterangan									
										,kode_kantor_cabang							
										,operasi_data								
										,operasi_data_ojk_code						
										,operasi_data_desc							
										,period										
					
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
										,{p}FormTransactionHistoryID                 
										,{p}FlagDetail        	             	
										,{p}NomorIdentitas       	         		
										,{p}NomorRekening        	         		
										,{p}Cif      	                     		
										,{p}KodeJenisSegmenFasilitas     	 		
										,{p}KodeJenisSegmenFasilitasOjkCode   		
										,{p}KodeJenisSegmenFasilitasDesc     		
										,{p}KodeJenisIdentitas       	     		
										,{p}KodeJenisIdentitasOjkCode         		
										,{p}KodeJenisIdentitasDesc            		
										,{p}NamaPenjamin                      		
										,{p}NamaLengkapPenjamin               		
										,{p}KodeGolonganPenjamin     	     		
										,{p}KodeGolonganPenjaminOjkCode      		
										,{p}KodeGolonganPenjaminDesc          		
										,{p}AlamatPenjamin                    		
										,{p}PresentaseDijamin                    		
										,{p}Keterangan       	             		
										,{p}KodeKantorCabang                  		
										,{p}OperasiData      	             		
										,{p}OperasiDataOjkCode                		
										,{p}OperasiDataDesc                   		
										,{p}Period                            				
									
									)
								";

			return await _command.Insert(transaction, query, model);
		}
		public async Task<int> UpdateByID(IDbTransaction transaction, P01History model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								 flag_detail									= {p}FlagDetail        	             	
								,nomor_identitas 								= {p}NomorIdentitas       	         			
								,nomor_rekening 								= {p}NomorRekening        	         		
								,cif											= {p}Cif      	                     		
								,kode_jenis_segmen_fasilitas					= {p}KodeJenisSegmenFasilitas     	 			
								,kode_jenis_segmen_fasilitas_ojk_code			= {p}KodeJenisSegmenFasilitasOjkCode   				
								,kode_jenis_segmen_fasilitas_desc				= {p}KodeJenisSegmenFasilitasDesc     				
								,kode_jenis_identitas							= {p}KodeJenisIdentitas       	     			
								,kode_jenis_identitas_ojk_code					= {p}KodeJenisIdentitasOjkCode         				
								,kode_jenis_identitas_desc						= {p}KodeJenisIdentitasDesc            			
								,nama_penjamin									= {p}NamaPenjamin                      			
								,nama_lengkap_penjamin							= {p}NamaLengkapPenjamin               				
								,kode_golongan_penjamin							= {p}KodeGolonganPenjamin     	     				
								,kode_golongan_penjamin_ojk_code				= {p}KodeGolonganPenjaminOjkCode      				
								,kode_golongan_penjamin_desc					= {p}KodeGolonganPenjaminDesc          		
								,alamat_penjamin 								= {p}AlamatPenjamin                    		
								,presentase_dijamin 							= {p}PresentaseDijamin                    		
								,keterangan										= {p}Keterangan       	             				
								,kode_kantor_cabang								= {p}KodeKantorCabang                  					
								,operasi_data									= {p}OperasiData      	             					
								,operasi_data_ojk_code							= {p}OperasiDataOjkCode                		
								,operasi_data_desc								= {p}OperasiDataDesc                   		
								,period											= {p}Period    
								,mod_date       								= {p}ModDate															
								,mod_by											= {p}ModBy																		
								,mod_ip_address   								= {p}ModIPAddress															                      		
								
								
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

		public Task<List<P01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

