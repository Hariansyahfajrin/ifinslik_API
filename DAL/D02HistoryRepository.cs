using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class D02HistoryRepository : BaseRepository, ID02HistoryRepository
	{

		private readonly string tableBase = "d02_history";
		private readonly string tableCompany = "sys_company";
		private readonly string tableForm = "form_transaction_history";

		public async Task<List<D02History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
	 				
					select

						        id		        								as	ID														
								,cif											as  Cif															
								,nomor_identitas_badan_usaha					as  NomorIdentitasBadanUsaha												
								,nama_badan_usaha								as  NamaBadanUsaha																	
								,kode_jenis_badan_usaha_desc					as  KodeJenisBadanUsahaDesc														

					from
						{tableBase}
					where
                                {tableBase}.form_transaction_history_id          = {p}FormTransactionHistoryID
                    AND
						(
							lower(cif)																		like	lower({p}Keyword)	
							or	lower(nomor_identitas_badan_usaha)											like	lower({p}Keyword)	
							or	lower(nama_badan_usaha)														like	lower({p}Keyword)	
							or	lower(kode_jenis_badan_usaha_desc)												like	lower({p}Keyword)	
						)
					order by
						mod_date DESC
					"
			);

			return await _command.GetRows<D02History>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				FormTransactionHistoryID = formTransactionHistoryID
			});
		}

		public async Task<D02History> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								{tableBase}.id		        								as	ID		        						
								,{tableBase}.form_transaction_history_id					as  FormTransactionHistoryID
								,{tableBase}.flag_detail									as  FlagDetail														
								,{tableBase}.cif											as  Cif															
								,{tableBase}.nomor_identitas_badan_usaha					as  NomorIdentitasBadanUsaha												
								,{tableBase}.nama_badan_usaha								as  NamaBadanUsaha																	
								,{tableBase}.kode_jenis_badan_usaha							as  KodeJenisBadanUsaha														
								,{tableBase}.kode_jenis_badan_usaha_ojk_code				as  KodeJenisBadanUsahaOjkCode														
								,{tableBase}.kode_jenis_badan_usaha_desc					as  KodeJenisBadanUsahaDesc													
								,{tableBase}.tempat_pendirian								as  TempatPendirian											
								,{tableBase}.nomor_akta_pendirian							as  NomorAktaPendirian																	
								,{tableBase}.tanggal_akta_pendirian							as  TanggalAktaPendirian										
								,{tableBase}.nomor_akta_perubahan_terakhir					as  NomorAktaPerubahanTerakhir 																	 
								,{tableBase}.tanggal_akta_perubahan_terakhir 				as  TanggalAktaPerubahanTerakhir 																										
								,{tableBase}.nomor_telepon									as  NomorTelepon																		
								,{tableBase}.nomor_handphone								as  NomorHandphone											
								,{tableBase}.email											as  Email						
								,{tableBase}.alamat											as  Alamat						
								,{tableBase}.kelurahan										as  Kelurahan 														 
								,{tableBase}.kecamatan 										as  Kecamatan 					
								,{tableBase}.kode_dati_2									as  KodeDati2																																	
								,{tableBase}.kode_dati_2_ojk_code							as  KodeDati2OjkCode								
								,{tableBase}.kode_dati_2_desc								as  KodeDati2Desc								
								,{tableBase}.kode_pos										as  KodePos									
								,{tableBase}.kode_negara_domisili							as  KodeNegaraDomisili											
								,{tableBase}.kode_negara_domisili_ojk_code					as  KodeNegaraDomisiliOjkCode												
								,{tableBase}.kode_negara_domisili_desc						as  KodeNegaraDomisiliDesc												
								,{tableBase}.kode_bidang_usaha								as  KodeBidangUsaha											
								,{tableBase}.kode_bidang_usaha_ojk_code						as  KodeBidangUsahaOjkCode										
								,{tableBase}.kode_bidang_usaha_desc							as  KodeBidangUsahaDesc									
								,{tableBase}.kode_hubungan_dengan_pelapor					as  KodeHubunganDenganPelapor																
								,{tableBase}.kode_hubungan_dengan_pelapor_ojk_code			as  KodeHubunganDenganPelaporOjkCode																			
								,{tableBase}.kode_hubungan_dengan_pelapor_desc				as  KodeHubunganDenganPelaporDesc												
								,{tableBase}.melanggar_bmpk									as  MelanggarBmpk					
								,{tableBase}.melanggar_bmpk_ojk_code						as  MelanggarBmpkOjkCode							
								,{tableBase}.melanggar_bmpk_desc							as  MelanggarBmpkDesc													
								,{tableBase}.melampaui_bmpk									as  MelampauiBmpk				
								,{tableBase}.melampaui_bmpk_ojk_code						as  MelampauiBmpkOjkCode							
								,{tableBase}.melampaui_bmpk_desc							as  MelampauiBmpkDesc																															
								,{tableBase}.go_public										as  GoPublic																		
								,{tableBase}.go_public_ojk_code								as  GoPublicOjkCode												
								,{tableBase}.go_public_desc									as  GoPublicDesc													
								,{tableBase}.kode_golongan_debitur							as  KodeGolonganDebitur													
								,{tableBase}.kode_golongan_debitur_ojk_code					as  KodeGolonganDebiturOjkCode														
								,{tableBase}.kode_golongan_debitur_desc						as  KodeGolonganDebiturDesc												
								,{tableBase}.peringkat_debitur								as  PeringkatDebitur							
								,{tableBase}.lembaga_pemeringkat							as  LembagaPemeringkat																	
								,{tableBase}.lembaga_pemeringkat_ojk_code					as  LembagaPemeringkatOjkCode											
								,{tableBase}.lembaga_pemeringkat_desc						as  LembagaPemeringkatDesc													
								,{tableBase}.tanggal_pemeringkatan							as  TanggalPemeringkatan														
								,{tableBase}.nama_grup_debitur								as  NamaGrupDebitur														
								,{tableBase}.kode_kantor_cabang								as  KodeKantorCabang													
								,{tableBase}.operasi_data									as  OperasiData															
								,{tableBase}.operasi_data_ojk_code							as  OperasiDataOjkCode												
								,{tableBase}.operasi_data_desc								as  OperasiDataDesc										
								,{tableBase}.period	        								as  Period		
								,sckc.name													as  SysCompanyKantorCabang
							
							from
								{tableBase}
							left join
							{tableCompany} as sckc on sckc.code = {tableBase}.kode_kantor_cabang	
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<D02History>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}


		public async Task<int> Insert(IDbTransaction transaction, D02History model)
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
										,cif									
										,nomor_identitas_badan_usaha			
										,nama_badan_usaha						
										,kode_jenis_badan_usaha					
										,kode_jenis_badan_usaha_ojk_code		
										,kode_jenis_badan_usaha_desc			
										,tempat_pendirian						
										,nomor_akta_pendirian					
										,tanggal_akta_pendirian					
										,nomor_akta_perubahan_terakhir			
										,tanggal_akta_perubahan_terakhir 		
										,nomor_telepon							
										,nomor_handphone						
										,email									
										,alamat									
										,kelurahan								
										,kecamatan 								
										,kode_dati_2							
										,kode_dati_2_ojk_code					
										,kode_dati_2_desc						
										,kode_pos								
										,kode_negara_domisili					
										,kode_negara_domisili_ojk_code			
										,kode_negara_domisili_desc				
										,kode_bidang_usaha						
										,kode_bidang_usaha_ojk_code				
										,kode_bidang_usaha_desc					
										,kode_hubungan_dengan_pelapor			
										,kode_hubungan_dengan_pelapor_ojk_code	
										,kode_hubungan_dengan_pelapor_desc		
										,melanggar_bmpk							
										,melanggar_bmpk_ojk_code				
										,melanggar_bmpk_desc					
										,melampaui_bmpk							
										,melampaui_bmpk_ojk_code				
										,melampaui_bmpk_desc					
										,go_public								
										,go_public_ojk_code						
										,go_public_desc							
										,kode_golongan_debitur					
										,kode_golongan_debitur_ojk_code			
										,kode_golongan_debitur_desc				
										,peringkat_debitur						
										,lembaga_pemeringkat					
										,lembaga_pemeringkat_ojk_code			
										,lembaga_pemeringkat_desc				
										,tanggal_pemeringkatan					
										,nama_grup_debitur						
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
										,{p}Cif		
										,{p}NomorIdentitasBadanUsaha			
										,{p}NamaBadanUsaha						
										,{p}KodeJenisBadanUsaha					
										,{p}KodeJenisBadanUsahaOjkCode			
										,{p}KodeJenisBadanUsahaDesc				
										,{p}TempatPendirian						
										,{p}NomorAktaPendirian					
										,{p}TanggalAktaPendirian				
										,{p}NomorAktaPerubahanTerakhir 			
										,{p}TanggalAktaPerubahanTerakhir 		
										,{p}NomorTelepon						
										,{p}NomorHandphone						
										,{p}Email						
										,{p}Alamat						
										,{p}Kelurahan 							
										,{p}Kecamatan 					
										,{p}KodeDati2							
										,{p}KodeDati2OjkCode					
										,{p}KodeDati2Desc						
										,{p}KodePos								
										,{p}KodeNegaraDomisili					
										,{p}KodeNegaraDomisiliOjkCode			
										,{p}KodeNegaraDomisiliDesc				
										,{p}KodeBidangUsaha						
										,{p}KodeBidangUsahaOjkCode				
										,{p}KodeBidangUsahaDesc					
										,{p}KodeHubunganDenganPelapor			
										,{p}KodeHubunganDenganPelaporOjkCode	
										,{p}KodeHubunganDenganPelaporDesc		
										,{p}MelanggarBmpk					
										,{p}MelanggarBmpkOjkCode				
										,{p}MelanggarBmpkDesc					
										,{p}MelampauiBmpk				
										,{p}MelampauiBmpkOjkCode				
										,{p}MelampauiBmpkDesc					
										,{p}GoPublic							
										,{p}GoPublicOjkCode						
										,{p}GoPublicDesc						
										,{p}KodeGolonganDebitur				
										,{p}KodeGolonganDebiturOjkCode			
										,{p}KodeGolonganDebiturDesc				
										,{p}PeringkatDebitur					
										,{p}LembagaPemeringkat					
										,{p}LembagaPemeringkatOjkCode			
										,{p}LembagaPemeringkatDesc				
										,{p}TanggalPemeringkatan				
										,{p}NamaGrupDebitur						
										,{p}KodeKantorCabang					
										,{p}OperasiData							
										,{p}OperasiDataOjkCode					
										,{p}OperasiDataDesc						
										,{p}Period													
											
									
									)
								";

			return await _command.Insert(transaction, query, model);
		}
		public async Task<int> UpdateByID(IDbTransaction transaction, D02History model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								flag_detail									= {p}FlagDetail					
								,cif										= {p}Cif	
								,nomor_identitas_badan_usaha				= {p}NomorIdentitasBadanUsaha																	
								,nama_badan_usaha							= {p}NamaBadanUsaha																					
								,kode_jenis_badan_usaha						= {p}KodeJenisBadanUsaha																		
								,kode_jenis_badan_usaha_ojk_code			= {p}KodeJenisBadanUsahaOjkCode																	
								,kode_jenis_badan_usaha_desc				= {p}KodeJenisBadanUsahaDesc																		
								,tempat_pendirian							= {p}TempatPendirian																					
								,nomor_akta_pendirian						= {p}NomorAktaPendirian																		
								,tanggal_akta_pendirian						= {p}TanggalAktaPendirian																	
								,nomor_akta_perubahan_terakhir				= {p}NomorAktaPerubahanTerakhir 														
								,tanggal_akta_perubahan_terakhir 			= {p}TanggalAktaPerubahanTerakhir 																	
								,nomor_telepon								= {p}NomorTelepon																					
								,nomor_handphone							= {p}NomorHandphone																					
								,email										= {p}Email																								
								,alamat										= {p}Alamat																										
								,kelurahan									= {p}Kelurahan 																			
								,kecamatan 									= {p}Kecamatan 																					
								,kode_dati_2								= {p}KodeDati2																		
								,kode_dati_2_ojk_code						= {p}KodeDati2OjkCode																		
								,kode_dati_2_desc							= {p}KodeDati2Desc																
								,kode_pos									= {p}KodePos																	
								,kode_negara_domisili						= {p}KodeNegaraDomisili											
								,kode_negara_domisili_ojk_code				= {p}KodeNegaraDomisiliOjkCode											
								,kode_negara_domisili_desc					= {p}KodeNegaraDomisiliDesc											
								,kode_bidang_usaha							= {p}KodeBidangUsaha																															
								,kode_bidang_usaha_ojk_code					= {p}KodeBidangUsahaOjkCode															
								,kode_bidang_usaha_desc						= {p}KodeBidangUsahaDesc																
								,kode_hubungan_dengan_pelapor				= {p}KodeHubunganDenganPelapor															
								,kode_hubungan_dengan_pelapor_ojk_code		= {p}KodeHubunganDenganPelaporOjkCode													
								,kode_hubungan_dengan_pelapor_desc			= {p}KodeHubunganDenganPelaporDesc																
								,melanggar_bmpk								= {p}MelanggarBmpk																	
								,melanggar_bmpk_ojk_code					= {p}MelanggarBmpkOjkCode														
								,melanggar_bmpk_desc						= {p}MelanggarBmpkDesc																	
								,melampaui_bmpk								= {p}MelampauiBmpk															
								,melampaui_bmpk_ojk_code					= {p}MelampauiBmpkOjkCode																	
								,melampaui_bmpk_desc						= {p}MelampauiBmpkDesc																	
								,go_public									= {p}GoPublic													
								,go_public_ojk_code							= {p}GoPublicOjkCode																	
								,go_public_desc								= {p}GoPublicDesc																		
								,kode_golongan_debitur						= {p}KodeGolonganDebitur																
								,kode_golongan_debitur_ojk_code				= {p}KodeGolonganDebiturOjkCode													
								,kode_golongan_debitur_desc					= {p}KodeGolonganDebiturDesc																					
								,peringkat_debitur							= {p}PeringkatDebitur																
								,lembaga_pemeringkat						= {p}LembagaPemeringkat														
								,lembaga_pemeringkat_ojk_code				= {p}LembagaPemeringkatOjkCode																					
								,lembaga_pemeringkat_desc					= {p}LembagaPemeringkatDesc																			
								,tanggal_pemeringkatan						= {p}TanggalPemeringkatan														
								,nama_grup_debitur							= {p}NamaGrupDebitur																	
								,kode_kantor_cabang							= {p}KodeKantorCabang																					
								,operasi_data								= {p}OperasiData																			
								,operasi_data_ojk_code						= {p}OperasiDataOjkCode																	
								,operasi_data_desc							= {p}OperasiDataDesc																							
								,period	        							= {p}Period																																									
								,mod_date       				 			= {p}ModDate
								,mod_by							 			= {p}ModBy
								,mod_ip_address 				 			= {p}ModIPAddress	
								
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
		public async Task<List<D02History>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? financeCompanyType)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
				SELECT 
						{tableBase}.cif as Cif
       					,{tableBase}.nama_badan_usaha as NamaBadanUsaha
					from 
						{tableBase} 
					INNER JOIN 
						{tableForm} ftr on ftr.id = {tableBase}.form_transaction_history_id
					WHERE 
						period = {p}Period
  					AND 
						ftr.finance_company_type = {p}FinanceCompanyType
 					 AND
                        (	
                                lower({tableBase}.cif)         				like lower({p}Keyword)	
                            or  lower({tableBase}.nama_badan_usaha)	    	like lower({p}Keyword)
						)
                    order by
                                {tableBase}.mod_date DESC
					"
			);
			return await _command.GetRows<D02History>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				Period = period,
				FinanceCompanyType = financeCompanyType,
			});
		}

		public Task<List<D02History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

