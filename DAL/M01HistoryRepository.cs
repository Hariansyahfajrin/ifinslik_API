using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class M01HistoryRepository : BaseRepository, IM01HistoryRepository
	{

		private readonly string tableBase = "m01_history";
		private readonly string tableD02 = "d02_history";
		private readonly string tableCompany = "sys_company";

		public async Task<List<M01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
	 				
					select

						     {tableBase}.id		        							as  ID                       
							,{tableBase}.nomor_identitas							as  NomorIdentitas                     
							,{tableBase}.cif										as  Cif                                           					
							,{tableBase}.nama_pengurus								as  NamaPengurus                       										
							,{tableBase}.jenis_kelamin								as  JenisKelamin 
							,d2.nama_badan_usaha									as  NamaBadanUsaha      
					from
						{tableBase}
					left join {tableD02} as d2 on d2.cif = {tableBase}.cif
					where
                                {tableBase}.form_transaction_history_id          = {p}FormTransactionHistoryID
                    AND
						(	lower   ({tableBase}.nomor_identitas)								like	lower({p}Keyword)															
							or lower({tableBase}.cif)											like	lower({p}Keyword)																								
							or lower({tableBase}.nama_pengurus)									like	lower({p}Keyword)												
							or lower({tableBase}.jenis_kelamin)									like	lower({p}Keyword)	
							or lower(d2.nama_badan_usaha)								like	lower({p}Keyword)																																

						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<M01History>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				FormTransactionHistoryID = formTransactionHistoryID
			});
		}

		public async Task<M01History> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

									{tableBase}.id		        								as  ID				        						
									,{tableBase}.form_transaction_history_id							as  FormTransactionHistoryID                  
									,{tableBase}.flag_detail									as  FlagDetail                         	
									,{tableBase}.nomor_identitas								as  NomorIdentitas                     		
									,{tableBase}.cif											as  Cif                                		
									,{tableBase}.kode_jenis_identitas							as  KodeJenisIdentitas                 			
									,{tableBase}.kode_jenis_identitas_ojk_code					as  KodeJenisIdentitasOjkCode          			
									,{tableBase}.kode_jenis_identitas_desc						as  KodeJenisIdentitasDesc             			
									,{tableBase}.nama_pengurus									as  NamaPengurus                       			
									,{tableBase}.jenis_kelamin									as  JenisKelamin                       			
									,{tableBase}.jenis_kelamin_ojk_code							as  JenisKelaminOjkCode                			
									,{tableBase}.jenis_kelamin_desc								as  JenisKelaminDesc                   			
									,{tableBase}.alamat											as  Alamat                             			
									,{tableBase}.kelurahan 										as  Kelurahan                          			
									,{tableBase}.kecamatan										as  Kecamatan                          			
									,{tableBase}.kode_dati_2									as  KodeDati2                          			
									,{tableBase}.kode_dati_2_ojk_code							as  KodeDati2OjkCode                   			
									,{tableBase}.kode_dati_2_desc								as  KodeDati2Desc                      				
									,{tableBase}.kode_jabatan									as  KodeJabatan                        	   		
									,{tableBase}.kode_jabatan_ojk_code							as  KodeJabatanOjkCode                 			
									,{tableBase}.kode_jabatan_desc								as  KodeJabatanDesc                    			
									,{tableBase}.pangsa_kepemilikan								as  PangsaKepemilikan                  			
									,{tableBase}.status_pengurus								as  StatusPengurus                     			
									,{tableBase}.status_pengurus_ojk_code						as  StatusPengurusOjkCode              			
									,{tableBase}.status_pengurus_desc							as  StatusPengurusDesc                 
									,{tableBase}.kode_kantor_cabang								as  KodeKantorCabang                   	
									,{tableBase}.operasi_data									as  OperasiData                        	
									,{tableBase}.operasi_data_ojk_code							as  OperasiDataOjkCode                 	
									,{tableBase}.operasi_data_desc								as  OperasiDatadesc                    	
									,{tableBase}.period											as  period                             	
									,sckc.name													as  SysCompanyKantorCabang
							from
								{tableBase}
							left join
							{tableCompany} as sckc on sckc.code = {tableBase}.kode_kantor_cabang	
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<M01History>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}

		public async Task<int> Insert(IDbTransaction transaction, M01History model)
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
										,cif										
										,kode_jenis_identitas						
										,kode_jenis_identitas_ojk_code					
										,kode_jenis_identitas_desc					
										,nama_pengurus								
										,jenis_kelamin								
										,jenis_kelamin_ojk_code						
										,jenis_kelamin_desc							
										,alamat										
										,kelurahan 									
										,kecamatan									
										,kode_dati_2								
										,kode_dati_2_ojk_code						
										,kode_dati_2_desc							
										,kode_jabatan							
										,kode_jabatan_ojk_code						
										,kode_jabatan_desc							
										,pangsa_kepemilikan							
										,status_pengurus							
										,status_pengurus_ojk_code					
										,status_pengurus_desc					
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
										,{p}Cif                              		
										,{p}KodeJenisIdentitas               		
										,{p}KodeJenisIdentitasOjkCode        		
										,{p}KodeJenisIdentitasDesc           		
										,{p}NamaPengurus                     		
										,{p}JenisKelamin                     		
										,{p}JenisKelaminOjkCode              		
										,{p}JenisKelaminDesc                 		
										,{p}Alamat                           		
										,{p}Kelurahan                        		
										,{p}Kecamatan                        		
										,{p}KodeDati2                        		
										,{p}KodeDati2OjkCode                 		
										,{p}KodeDati2Desc                    		
										,{p}KodeJabatan                         		
										,{p}KodeJabatanOjkCode               		
										,{p}KodeJabatanDesc                  		
										,{p}PangsaKepemilikan                		
										,{p}StatusPengurus                   		
										,{p}StatusPengurusOjkCode            		
										,{p}StatusPengurusDesc               
										,{p}KodeKantorCabang                 
										,{p}OperasiData                      
										,{p}OperasiDataOjkCode               
										,{p}OperasiDatadesc                  
										,{p}period                           	
									
									)
								";

			return await _command.Insert(transaction, query, model);
		}
		public async Task<int> UpdateByID(IDbTransaction transaction, M01History model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								 flag_detail								= {p}FlagDetail                        	
								,nomor_identitas							= {p}NomorIdentitas                    			
								,cif										= {p}Cif                               		
								,kode_jenis_identitas						= {p}KodeJenisIdentitas                		
								,kode_jenis_identitas_ojk_code				= {p}KodeJenisIdentitasOjkCode         			
								,kode_jenis_identitas_desc					= {p}KodeJenisIdentitasDesc              				
								,nama_pengurus								= {p}NamaPengurus                       				
								,jenis_kelamin								= {p}JenisKelamin                      			
								,jenis_kelamin_ojk_code						= {p}JenisKelaminOjkCode                 				
								,jenis_kelamin_desc							= {p}JenisKelaminDesc                    			
								,alamat										= {p}Alamat                              			
								,kelurahan 									= {p}Kelurahan                           				
								,kecamatan									= {p}Kecamatan                         				
								,kode_dati_2								= {p}KodeDati2                          				
								,kode_dati_2_ojk_code						= {p}KodeDati2OjkCode                    		
								,kode_dati_2_desc							= {p}KodeDati2Desc                       		
								,kode_jabatan								= {p}KodeJabatan                            		
								,kode_jabatan_ojk_code						= {p}KodeJabatanOjkCode                				
								,kode_jabatan_desc							= {p}KodeJabatanDesc                     					
								,pangsa_kepemilikan							= {p}PangsaKepemilikan                 					
								,status_pengurus							= {p}StatusPengurus                      		
								,status_pengurus_ojk_code					= {p}StatusPengurusOjkCode               		
								,status_pengurus_desc						= {p}StatusPengurusDesc               
								,kode_kantor_cabang							= {p}KodeKantorCabang                 								
								,operasi_data								= {p}OperasiData                      													
								,operasi_data_ojk_code						= {p}OperasiDataOjkCode               															
								,operasi_data_desc							= {p}OperasiDatadesc                  												
								,period										= {p}period                  
								,mod_date       							= {p}ModDate		
								,mod_by										= {p}ModBy			
								,mod_ip_address   							= {p}ModIPAddress	
								                                 		
								
								
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

		public Task<List<M01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

