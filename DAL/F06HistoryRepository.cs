using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class F06HistoryRepository : BaseRepository, IF06HistoryRepository
	{

		private readonly string tableBase = "f06_history";
		private readonly string tableCompany = "sys_company";
		private readonly string tableD01 = "d01_history";
		private readonly string tableD02 = "d02_history";

		public async Task<List<F06History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
	 				
					select

						     {tableBase}.id		        									as	ID  	             	
							,{tableBase}.nomor_rekening 									as 	NomorRekening        	         				
							,{tableBase}.cif												as 	Cif      	   
							,{tableBase}.kode_jenis_fasilitas_lainnya_desc	    	         as  KodeJenisFasilitasLainnyaDesc	                  						          																					
                     		,case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end as NamaLengkap
					from
						{tableBase}
					left join {tableD01} as d1 on d1.cif = {tableBase}.cif
           			left join {tableD02} as d2 on d2.cif = {tableBase}.cif         
					where
                                {tableBase}.form_transaction_history_id          = {p}FormTransactionHistoryID
                    AND
						(											
							   lower({tableBase}.nomor_rekening) 									like	lower({p}Keyword)												
							or lower({tableBase}.cif)												like	lower({p}Keyword)	
							or lower({tableBase}.kode_jenis_fasilitas_lainnya_desc) 						like	lower({p}Keyword)	
							or lower(case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end) like lower({p}Keyword)																																																																																		

						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<F06History>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				FormTransactionHistoryID = formTransactionHistoryID
			});
		}
		public async Task<F06History> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

									{tableBase}.id		        								as	ID		        								        						
									,{tableBase}.form_transaction_history_id  					as	FormTransactionHistoryID                 
									,{tableBase}.flag_detail									as	FlagDetail        	             	      	         		
									,{tableBase}.nomor_rekening 								as	NomorRekening        	         		
									,{tableBase}.cif											as	Cif      
									,{tableBase}.kode_jenis_fasilitas_lainnya					as  KodeJenisFasilitasLainnya										
									,{tableBase}.kode_jenis_fasilitas_lainnya_ojk_code			as  KodeJenisFasilitasLainnyaOJKCode																
									,{tableBase}.kode_jenis_fasilitas_lainnya_desc				as  KodeJenisFasilitasLainnyaDesc																	
									,{tableBase}.sumber_dana									as  SumberDana																		
									,{tableBase}.sumber_dana_ojk_code							as  SumberDanaOJKCode														
									,{tableBase}.sumber_dana_desc								as  SumberDanaDesc														
									,{tableBase}.tanggal_mulai									as  TanggalMulai													
									,{tableBase}.tanggal_jatuh_tempo							as  TanggalJatuhTempo																		
									,{tableBase}.presentase_suku_bunga							as  PresentaseSukuBunga																		
									,{tableBase}.kode_valuta									as  KodeValuta																				
									,{tableBase}.kode_valuta_ojk_code							as  KodeValutaOJKCode																						
									,{tableBase}.kode_valuta_desc								as  KodeValutaDesc																									
									,{tableBase}.nominal_jumlah_kewajiban 						as  NominalJumlahKewajiban 																								
									,{tableBase}.nilai_dalam_mata_uang_asal 					as  NilaiDalamMataUangAsal 																								
									,{tableBase}.kode_kolektibilitas							as  KodeKolektibilitas																										
									,{tableBase}.kode_kolektibilitas_ojk_code					as  KodeKolektibilitasOJKCode																											
									,{tableBase}.kode_kolektibilitas_desc						as  KodeKolektibilitasDesc																	
									,{tableBase}.tanggal_macet									as  TanggalMacet																						
									,{tableBase}.kode_sebab_macet								as  KodeSebabMacet																
									,{tableBase}.kode_sebab_macet_ojk_code						as  KodeSebabMacetOJKCode																				
									,{tableBase}.kode_sebab_macet_desc							as  KodeSebabMacetDesc															
									,{tableBase}.tunggakan										as  Tunggakan												
									,{tableBase}.jumlah_hari_tunggakan							as  JumlahHariTunggakan																
									,{tableBase}.kode_kondisi									as  KodeKondisi												
									,{tableBase}.kode_kondisi_ojk_code							as  KodeKondisiOJKCode																						
									,{tableBase}.kode_kondisi_desc								as  KodeKondisiDesc															
									,{tableBase}.tanggal_kondisi								as  TanggalKondisi																	
									,{tableBase}.keterangan										as  Keterangan																	
									,{tableBase}.kode_kantor_cabang								as  KodeKantorCabang															
									,{tableBase}.operasi_data									as  OperasiData												
									,{tableBase}.operasi_data_ojk_code							as  OperasiDataOJKCode													
									,{tableBase}.operasi_data_desc								as  OperasiDataDesc										
									,{tableBase}.period	                						as  Period																									     									
									,sckc.name													as  SysCompanyKantorCabang

							from
								{tableBase}
							left join
							{tableCompany} as sckc on sckc.code = {tableBase}.kode_kantor_cabang	
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<F06History>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}


		public async Task<int> Insert(IDbTransaction transaction, F06History model)
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
										,nomor_rekening
										,cif
										,kode_jenis_fasilitas_lainnya
										,kode_jenis_fasilitas_lainnya_ojk_code
										,kode_jenis_fasilitas_lainnya_desc
										,sumber_dana
										,sumber_dana_ojk_code
										,sumber_dana_desc
										,tanggal_mulai
										,tanggal_jatuh_tempo
										,presentase_suku_bunga
										,kode_valuta
										,kode_valuta_ojk_code
										,kode_valuta_desc
										,nominal_jumlah_kewajiban 
										,nilai_dalam_mata_uang_asal 
										,kode_kolektibilitas
										,kode_kolektibilitas_ojk_code
										,kode_kolektibilitas_desc
										,tanggal_macet
										,kode_sebab_macet
										,kode_sebab_macet_ojk_code
										,kode_sebab_macet_desc
										,tunggakan
										,jumlah_hari_tunggakan
										,kode_kondisi
										,kode_kondisi_ojk_code		
										,kode_kondisi_desc
										,tanggal_kondisi
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
										,{p}NomorRekening
										,{p}Cif
										,{p}KodeJenisFasilitasLainnya
										,{p}KodeJenisFasilitasLainnyaOJKCode
										,{p}KodeJenisFasilitasLainnyaDesc
										,{p}SumberDana
										,{p}SumberDanaOJKCode
										,{p}SumberDanaDesc
										,{p}TanggalMulai
										,{p}TanggalJatuhTempo
										,{p}PresentaseSukuBunga
										,{p}KodeValuta
										,{p}KodeValutaOJKCode
										,{p}KodeValutaDesc
										,{p}NominalJumlahKewajiban 
										,{p}NilaiDalamMataUangAsal 
										,{p}KodeKolektibilitas
										,{p}KodeKolektibilitasOJKCode
										,{p}KodeKolektibilitasDesc
										,{p}TanggalMacet
										,{p}KodeSebabMacet
										,{p}KodeSebabMacetOJKCode
										,{p}KodeSebabMacetDesc
										,{p}Tunggakan
										,{p}JumlahHariTunggakan
										,{p}KodeKondisi
										,{p}KodeKondisiOJKCode	
										,{p}KodeKondisiDesc
										,{p}TanggalKondisi
										,{p}Keterangan
										,{p}KodeKantorCabang
										,{p}OperasiData
										,{p}OperasiDataOJKCode
										,{p}OperasiDataDesc
										,{p}Period		
									
									)
								";

			return await _command.Insert(transaction, query, model);
		}
		public async Task<int> UpdateByID(IDbTransaction transaction, F06History model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								flag_detail									    = {p}FlagDetail        	             	      	         		
								,nomor_rekening 								= {p}NomorRekening        	         		
								,cif											= {p}Cif      
								,kode_jenis_fasilitas_lainnya					= {p}KodeJenisFasilitasLainnya										
								,kode_jenis_fasilitas_lainnya_ojk_code			= {p}KodeJenisFasilitasLainnyaOJKCode																
								,kode_jenis_fasilitas_lainnya_desc				= {p}KodeJenisFasilitasLainnyaDesc																	
								,sumber_dana									= {p}SumberDana																		
								,sumber_dana_ojk_code							= {p}SumberDanaOJKCode														
								,sumber_dana_desc								= {p}SumberDanaDesc														
								,tanggal_mulai									= {p}TanggalMulai													
								,tanggal_jatuh_tempo							= {p}TanggalJatuhTempo																		
								,presentase_suku_bunga							= {p}PresentaseSukuBunga																		
								,kode_valuta									= {p}KodeValuta																				
								,kode_valuta_ojk_code							= {p}KodeValutaOJKCode																						
								,kode_valuta_desc								= {p}KodeValutaDesc																									
								,nominal_jumlah_kewajiban 						= {p}NominalJumlahKewajiban 																								
								,nilai_dalam_mata_uang_asal 					= {p}NilaiDalamMataUangAsal 																								
								,kode_kolektibilitas							= {p}KodeKolektibilitas																										
								,kode_kolektibilitas_ojk_code					= {p}KodeKolektibilitasOJKCode																											
								,kode_kolektibilitas_desc						= {p}KodeKolektibilitasDesc																	
								,tanggal_macet									= {p}TanggalMacet																						
								,kode_sebab_macet								= {p}KodeSebabMacet																
								,kode_sebab_macet_ojk_code						= {p}KodeSebabMacetOJKCode																				
								,kode_sebab_macet_desc							= {p}KodeSebabMacetDesc															
								,tunggakan										= {p}Tunggakan												
								,jumlah_hari_tunggakan							= {p}JumlahHariTunggakan																
								,kode_kondisi									= {p}KodeKondisi												
								,kode_kondisi_ojk_code							= {p}KodeKondisiOJKCode																						
								,kode_kondisi_desc								= {p}KodeKondisiDesc															
								,tanggal_kondisi								= {p}TanggalKondisi																	
								,keterangan										= {p}Keterangan																	
								,kode_kantor_cabang								= {p}KodeKantorCabang															
								,operasi_data									= {p}OperasiData												
								,operasi_data_ojk_code							= {p}OperasiDataOJKCode													
								,operasi_data_desc								= {p}OperasiDataDesc										
								,period	                						= {p}Period				   	                     		
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

		public Task<List<F06History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

