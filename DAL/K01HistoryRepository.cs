using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class K01HistoryRepository : BaseRepository, IK01HistoryRepository
	{

		private readonly string tableBase = "k01_history";
		private readonly string tableCompany = "sys_company";
		private readonly string tableD01 = "d01_history";
		private readonly string tableD02 = "d02_history";

		public async Task<List<K01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
	 				
					select
						         {tableBase}.id		        							as	ID
								,{tableBase}.form_transaction_history_id						as  FormTransactionHistoryID											
								,{tableBase}.flag_detail								as  FlagDetail																
								,{tableBase}.cif										as  Cif																
								,{tableBase}.tanggal_laporan							as  TanggalLaporan																				
								,case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end as NamaLengkap
					from
						{tableBase}
					left join {tableD01} as d1 on d1.cif = {tableBase}.cif
           			left join {tableD02} as d2 on d2.cif = {tableBase}.cif
					where
                                {tableBase}.form_transaction_history_id          = {p}FormTransactionHistoryID
                    AND
						(
							lower    ({tableBase}.cif)										like	lower({p}Keyword)	
							or	lower({tableBase}.tanggal_laporan)						like	lower({p}Keyword)		
							or lower(case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end) like lower({p}Keyword)			
						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<K01History>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				FormTransactionHistoryID = formTransactionHistoryID
			});
		}
		public async Task<K01History> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

									{tableBase}.id		        							as	ID
									,{tableBase}.form_transaction_history_id  						as  FormTransactionHistoryID											
									,{tableBase}.flag_detail								as  FlagDetail																
									,{tableBase}.cif										as  Cif																
									,{tableBase}.tanggal_laporan							as  TanggalLaporan																				
									,{tableBase}.aset										as  Aset															
									,{tableBase}.aset_lancar								as  AsetLancar																
									,{tableBase}.kas										as  Kas																		
									,{tableBase}.piutang_usaha_lancar 						as  PiutangUsahaLancar 																		
									,{tableBase}.investasi_lain_lancar 						as  InvestasiLainLancar 																
									,{tableBase}.aset_lancar_lain							as  AsetLancarLain																
									,{tableBase}.aset_tidak_lancar							as  AsetTidakLancar																						
									,{tableBase}.piutang_usaha_tidak_lancar 				as  PiutangUsahaTidakLancar 															
									,{tableBase}.investasi_lain_tidak_lancar 				as  InvestasiLainTidakLancar 																							
									,{tableBase}.aset_tidak_lancar_lain						as  AsetTidakLancarLain																							
									,{tableBase}.liabilitas									as  Liabilitas													
									,{tableBase}.liabilitas_jangka_pendek					as  LiabilitasJangkaPendek															
									,{tableBase}.pinjaman_jangka_pendek						as  PinjamanJangkaPendek																
									,{tableBase}.utang_usaha_jangka_pendek 					as  UtangUsahaJangkaPendek 																			
									,{tableBase}.liabilitas_jangka_pendek_lain 				as  LiabilitasJangkaPendekLain 																		
									,{tableBase}.liabilitas_jangka_panjang					as  LiabilitasJangkaPanjang																		
									,{tableBase}.pinjaman_jangka_panjang					as  PinjamanJangkaPanjang											
									,{tableBase}.utang_usaha_jangka_panjang 				as  UtangUsahajangkaPanjang 										
									,{tableBase}.liabilitas_jangka_panjang_lain 			as  LiabilitasJangkaPanjangLain 																			
									,{tableBase}.ekuitas									as  Ekuitas																						
									,{tableBase}.pendapatan_usaha 							as  PendapatanUsaha 																					
									,{tableBase}.beban_operasional 							as  BebanOperasional 															
									,{tableBase}.laba_rugi_bruto							as  LabaRugiBruto																
									,{tableBase}.pendapatan_lain							as  PendapatanLain																		
									,{tableBase}.beban_lain									as  BebanLain																	
									,{tableBase}.laba_rugi_pre_tax							as  LabaRugiPreTax															
									,{tableBase}.laba_rugi_tahun_berjalan 					as  LabaRugiTahunBerjalan 																			
									,{tableBase}.kode_kantor_cabang 						as  KodeKantorCabang 														
									,{tableBase}.operasi_data								as  OperasiData															
									,{tableBase}.operasi_data_ojk_code 						as  OperasiDataOJKCode 									
									,{tableBase}.operasi_data_desc 							as  OperasiDataDesc 															
									,{tableBase}.period										as  Period	
									,sckc.name												as  SysCompanyKantorCabang
							from
								{tableBase}
							left join
							{tableCompany} as sckc on sckc.code = {tableBase}.kode_kantor_cabang	
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<K01History>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}


		public async Task<int> Insert(IDbTransaction transaction, K01History model)
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
										,tanggal_laporan
										,aset
										,aset_lancar
										,kas
										,piutang_usaha_lancar 
										,investasi_lain_lancar 
										,aset_lancar_lain
										,aset_tidak_lancar
										,piutang_usaha_tidak_lancar 
										,investasi_lain_tidak_lancar 
										,aset_tidak_lancar_lain
										,liabilitas
										,liabilitas_jangka_pendek
										,pinjaman_jangka_pendek
										,utang_usaha_jangka_pendek 
										,liabilitas_jangka_pendek_lain 
										,liabilitas_jangka_panjang
										,pinjaman_jangka_panjang
										,utang_usaha_jangka_panjang 
										,liabilitas_jangka_panjang_lain 
										,ekuitas
										,pendapatan_usaha 
										,beban_operasional 
										,laba_rugi_bruto
										,pendapatan_lain
										,beban_lain
										,laba_rugi_pre_tax
										,laba_rugi_tahun_berjalan 
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
										,{p}TanggalLaporan
										,{p}Aset
										,{p}AsetLancar
										,{p}Kas
										,{p}PiutangUsahaLancar 
										,{p}InvestasiLainLancar 
										,{p}AsetLancarLain
										,{p}AsetTidakLancar
										,{p}PiutangUsahaTidakLancar 
										,{p}InvestasiLainTidakLancar 
										,{p}AsetTidakLancarLain
										,{p}Liabilitas
										,{p}LiabilitasJangkaPendek
										,{p}PinjamanJangkaPendek
										,{p}UtangUsahaJangkaPendek 
										,{p}LiabilitasJangkaPendekLain 
										,{p}LiabilitasJangkaPanjang
										,{p}PinjamanJangkaPanjang
										,{p}UtangUsahajangkaPanjang 
										,{p}LiabilitasJangkaPanjangLain 
										,{p}Ekuitas
										,{p}PendapatanUsaha 
										,{p}BebanOperasional 
										,{p}LabaRugiBruto
										,{p}PendapatanLain
										,{p}BebanLain
										,{p}LabaRugiPreTax
										,{p}LabaRugiTahunBerjalan 
										,{p}KodeKantorCabang 
										,{p}OperasiData
										,{p}OperasiDataOJKCode 
										,{p}OperasiDataDesc 
										,{p}Period

									
									)
								";

			return await _command.Insert(transaction, query, model);
		}
		public async Task<int> UpdateByID(IDbTransaction transaction, K01History model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								flag_detail	    							= {p}FlagDetail
								,cif										= {p}Cif			
								,tanggal_laporan							= {p}TanggalLaporan																														
								,aset										= {p}Aset							
								,aset_lancar								= {p}AsetLancar											
								,kas										= {p}Kas						
								,piutang_usaha_lancar 						= {p}PiutangUsahaLancar 									
								,investasi_lain_lancar 						= {p}InvestasiLainLancar 												
								,aset_lancar_lain							= {p}AsetLancarLain									
								,aset_tidak_lancar							= {p}AsetTidakLancar								
								,piutang_usaha_tidak_lancar 				= {p}PiutangUsahaTidakLancar 										
								,investasi_lain_tidak_lancar 				= {p}InvestasiLainTidakLancar 									
								,aset_tidak_lancar_lain						= {p}AsetTidakLancarLain					
								,liabilitas									= {p}Liabilitas											
								,liabilitas_jangka_pendek					= {p}LiabilitasJangkaPendek									
								,pinjaman_jangka_pendek						= {p}PinjamanJangkaPendek								
								,utang_usaha_jangka_pendek 					= {p}UtangUsahaJangkaPendek 					
								,liabilitas_jangka_pendek_lain 				= {p}LiabilitasJangkaPendekLain 						
								,liabilitas_jangka_panjang					= {p}LiabilitasJangkaPanjang						
								,pinjaman_jangka_panjang					= {p}PinjamanJangkaPanjang								
								,utang_usaha_jangka_panjang 				= {p}UtangUsahajangkaPanjang 								
								,liabilitas_jangka_panjang_lain 			= {p}LiabilitasJangkaPanjangLain 														
								,ekuitas									= {p}Ekuitas			
								,pendapatan_usaha 							= {p}PendapatanUsaha 											
								,beban_operasional 							= {p}BebanOperasional 							
								,laba_rugi_bruto							= {p}LabaRugiBruto							
								,pendapatan_lain							= {p}PendapatanLain						
								,beban_lain									= {p}BebanLain											
								,laba_rugi_pre_tax							= {p}LabaRugiPreTax			
								,laba_rugi_tahun_berjalan 					= {p}LabaRugiTahunBerjalan 									
								,kode_kantor_cabang 						= {p}KodeKantorCabang 															
								,operasi_data								= {p}OperasiData				
								,operasi_data_ojk_code 						= {p}OperasiDataOJKCode 								
								,operasi_data_desc 							= {p}OperasiDataDesc 					
								,period										= {p}Period	
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

		public Task<List<K01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

