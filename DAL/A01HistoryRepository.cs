using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class A01HistoryRepository : BaseRepository, IA01HistoryRepository
	{

		private readonly string tableBase = "a01_history";

		private readonly string tableCompany = "sys_company";

		public async Task<List<A01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
	 				
					select
						        id		        								as	ID
								,kode_agunan									as  KodeAgunan
								,nomor_rekening									as  NomorRekening
								,cif											as  Cif
								,kode_jenis_agunan_desc							as	KodeJenisAgunanDesc
								,nama_pemilik									as	NamaPemilik
					from
						{tableBase}
					where
                                {tableBase}.form_transaction_history_id          = {p}FormTransactionHistoryID
                    AND
						(
							lower(kode_agunan)          							like	lower({p}Keyword)	
							or	lower(nomor_rekening)          							like	lower({p}Keyword)	
							or	lower(cif)          									like	lower({p}Keyword)	
							or	lower(kode_jenis_agunan_desc)          						like	lower({p}Keyword)	
							or	lower(nama_pemilik)          							like	lower({p}Keyword)		
						)
					order by
						mod_date DESC
					"
			);

			return await _command.GetRows<A01History>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				FormTransactionHistoryID = formTransactionHistoryID
			});
		}

		public async Task<A01History> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

									{tableBase}.id		        						as	ID
									,{tableBase}.form_transaction_history_id	    	as 	FormTransactionHistoryID
									,{tableBase}.flag_detail	    					as 	FlagDetail
									,{tableBase}.kode_agunan							as  KodeAgunan
									,{tableBase}.nomor_rekening							as  NomorRekening
									,{tableBase}.cif									as  Cif
									,{tableBase}.kode_jenis_segmen_fasilitas			as  KodeJenisSegmenFasilitas
									,{tableBase}.kode_jenis_segmen_fasilitas_ojk_code	as	KodeJenisSegmenFasilitasOJKCode
									,{tableBase}.kode_jenis_segmen_fasilitas_desc		as	KodeJenisSegmenFasilitasDesc
									,{tableBase}.kode_status_agunan						as  KodeStatusAgunan
									,{tableBase}.kode_status_agunan_ojk_code			as	KodeStatusAgunanOJKCode
									,{tableBase}.kode_status_agunan_desc				as	KodeStatusAgunanDesc
									,{tableBase}.kode_jenis_agunan						as	KodeJenisAgunan
									,{tableBase}.kode_jenis_agunan_ojk_code				as	KodeJenisAgunanOJKCode
									,{tableBase}.kode_jenis_agunan_desc					as	KodeJenisAgunanDesc
									,{tableBase}.peringkat_agunan						as	PeringkatAgunan
									,{tableBase}.kode_lembaga_pemeringkat 				as	KodeLembagaPemeringkat
									,{tableBase}.kode_lembaga_pemeringkat_ojk_code 		as	KodeLembagaPemeringkatOJKCode
									,{tableBase}.kode_lembaga_pemeringkat_desc			as	KodeLembagaPemeringkatDesc
									,{tableBase}.kode_jenis_pengikatan					as	KodeJenisPengikatan
									,{tableBase}.kode_jenis_pengikatan_ojk_code			as	KodeJenisPengikatanOJKCode
									,{tableBase}.kode_jenis_pengikatan_desc				as	KodeJenisPengikatanDesc
									,{tableBase}.tanggal_pengikatan						as	TanggalPengikatan
									,{tableBase}.nama_pemilik							as	NamaPemilik
									,{tableBase}.bukti_kepemilikan						as	BuktiKepemilikan
									,{tableBase}.alamat_agunan							as	AlamatAgunan
									,{tableBase}.kode_dati_2							as	KodeDati2
									,{tableBase}.kode_dati_2_ojk_code					as	KodeDati2OJKCode
									,{tableBase}.kode_dati_2_desc						as	KodeDati2Desc
									,{tableBase}.nilai_agunan_njop						as	NilaiAgunanNJOP
									,{tableBase}.nilai_agunan_ljk						as	NilaiAgunanLJK
									,{tableBase}.tanggal_penilaian_ljk					as	TanggalPenilaianLJK
									,{tableBase}.nilai_agunan_independen				as	NilaiAgunanIndependen
									,{tableBase}.nama_penilai_independen				as	NamaPenilaiIndependen
									,{tableBase}.tanggal_penilaian_independen			as	TanggalPenilaianIndependen
									,{tableBase}.status_paripasu						as	StatusParipasu
									,{tableBase}.status_paripasu_ojk_code				as	StatusParipasuOJKCode
									,{tableBase}.status_paripasu_desc					as	StatusParipasuDesc
									,{tableBase}.presentase_paripasu					as	PresentaseParipasu
									,{tableBase}.status_kredit_join						as	StatusKreditJoin
									,{tableBase}.status_kredit_join_ojk_code			as	StatusKreditJoinOJKCode
									,{tableBase}.status_kredit_join_desc				as	StatusKreditJoinDesc
									,{tableBase}.diasuransikan							as	Diasuransikan
									,{tableBase}.diasuransikan_ojk_code					as	DiasuransikanOJKCode
									,{tableBase}.diasuransikan_desc						as	DiasuransikanDesc
									,{tableBase}.keterangan								as	Keterangan
									,{tableBase}.kode_kantor_cabang						as	KodeKantorCabang
									,{tableBase}.operasi_data							as	OperasiData
									,{tableBase}.operasi_data_ojk_code					as	OperasiDataOJKCode
									,{tableBase}.operasi_data_desc						as	OperasiDataDesc
									,{tableBase}.period									as	Period	
									,sckc.name											as  SysCompanyKantorCabang
							from
								{tableBase}
							left join
							{tableCompany} as sckc on sckc.code = {tableBase}.kode_kantor_cabang	
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<A01History>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}


		public async Task<int> Insert(IDbTransaction transaction, A01History model)
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
										,kode_agunan							
										,nomor_rekening							
										,cif									
										,kode_jenis_segmen_fasilitas			
										,kode_jenis_segmen_fasilitas_ojk_code	
										,kode_jenis_segmen_fasilitas_desc		
										,kode_status_agunan						
										,kode_status_agunan_ojk_code			
										,kode_status_agunan_desc					
										,kode_jenis_agunan						
										,kode_jenis_agunan_ojk_code				
										,kode_jenis_agunan_desc					
										,peringkat_agunan						
										,kode_lembaga_pemeringkat 				
										,kode_lembaga_pemeringkat_ojk_code 		
										,kode_lembaga_pemeringkat_desc			
										,kode_jenis_pengikatan					
										,kode_jenis_pengikatan_ojk_code		
										,kode_jenis_pengikatan_desc				
										,tanggal_pengikatan						
										,nama_pemilik							
										,bukti_kepemilikan						
										,alamat_agunan							
										,kode_dati_2							
										,kode_dati_2_ojk_code					
										,kode_dati_2_desc						
										,nilai_agunan_njop						
										,nilai_agunan_ljk						
										,tanggal_penilaian_ljk					
										,nilai_agunan_independen				
										,nama_penilai_independen				
										,tanggal_penilaian_independen			
										,status_paripasu						
										,status_paripasu_ojk_code				
										,status_paripasu_desc					
										,presentase_paripasu					
										,status_kredit_join						
										,status_kredit_join_ojk_code			
										,status_kredit_join_desc				
										,diasuransikan							
										,diasuransikan_ojk_code				
										,diasuransikan_desc						
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
										,{p}KodeAgunan
										,{p}NomorRekening
										,{p}Cif
										,{p}KodeJenisSegmenFasilitas
										,{p}KodeJenisSegmenFasilitasOJKCode
										,{p}KodeJenisSegmenFasilitasDesc
										,{p}KodeStatusAgunan
										,{p}KodeStatusAgunanOJKCode
										,{p}KodeStatusAgunanDesc
										,{p}KodeJenisAgunan
										,{p}KodeJenisAgunanOJKCode
										,{p}KodeJenisAgunanDesc
										,{p}PeringkatAgunan
										,{p}KodeLembagaPemeringkat
										,{p}KodeLembagaPemeringkatOJKCode
										,{p}KodeLembagaPemeringkatDesc
										,{p}KodeJenisPengikatan
										,{p}KodeJenisPengikatanOJKCode
										,{p}KodeJenisPengikatanDesc
										,{p}TanggalPengikatan
										,{p}NamaPemilik
										,{p}BuktiKepemilikan
										,{p}AlamatAgunan
										,{p}KodeDati2
										,{p}KodeDati2OJKCode
										,{p}KodeDati2Desc
										,{p}NilaiAgunanNJOP
										,{p}NilaiAgunanLJK
										,{p}TanggalPenilaianLJK
										,{p}NilaiAgunanIndependen
										,{p}NamaPenilaiIndependen
										,{p}TanggalPenilaianIndependen
										,{p}StatusParipasu
										,{p}StatusParipasuOJKCode
										,{p}StatusParipasuDesc
										,{p}PresentaseParipasu
										,{p}StatusKreditJoin
										,{p}StatusKreditJoinOJKCode
										,{p}StatusKreditJoinDesc
										,{p}Diasuransikan
										,{p}DiasuransikanOJKCode
										,{p}DiasuransikanDesc
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
		public async Task<int> UpdateByID(IDbTransaction transaction, A01History model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								flag_detail	    							= {p}FlagDetail
								,kode_agunan								= {p}KodeAgunan	
								,nomor_rekening								= {p}NomorRekening	
								,cif										= {p}Cif	
								,kode_jenis_segmen_fasilitas				= {p}KodeJenisSegmenFasilitas	
								,kode_jenis_segmen_fasilitas_ojk_code		= {p}KodeJenisSegmenFasilitasOJKCode	
								,kode_jenis_segmen_fasilitas_desc			= {p}KodeJenisSegmenFasilitasDesc	
								,kode_status_agunan							= {p}KodeStatusAgunan	
								,kode_status_agunan_ojk_code				= {p}KodeStatusAgunanOJKCode	
								,kode_status_agunan_desc					= {p}KodeStatusAgunanDesc	
								,kode_jenis_agunan							= {p}KodeJenisAgunan	
								,kode_jenis_agunan_ojk_code					= {p}KodeJenisAgunanOJKCode	
								,kode_jenis_agunan_desc						= {p}KodeJenisAgunanDesc	
								,peringkat_agunan							= {p}PeringkatAgunan	
								,kode_lembaga_pemeringkat 					= {p}KodeLembagaPemeringkat	
								,kode_lembaga_pemeringkat_ojk_code 			= {p}KodeLembagaPemeringkatOJKCode	
								,kode_lembaga_pemeringkat_desc				= {p}KodeLembagaPemeringkatDesc	
								,kode_jenis_pengikatan						= {p}KodeJenisPengikatan	
								,kode_jenis_pengikatan_ojk_code				= {p}KodeJenisPengikatanOJKCode	
								,kode_jenis_pengikatan_desc					= {p}KodeJenisPengikatanDesc	
								,tanggal_pengikatan							= {p}TanggalPengikatan	
								,nama_pemilik								= {p}NamaPemilik	
								,bukti_kepemilikan							= {p}BuktiKepemilikan	
								,alamat_agunan								= {p}AlamatAgunan	
								,kode_dati_2								= {p}KodeDati2	
								,kode_dati_2_ojk_code						= {p}KodeDati2OJKCode	
								,kode_dati_2_desc							= {p}KodeDati2Desc	
								,nilai_agunan_njop							= {p}NilaiAgunanNJOP	
								,nilai_agunan_ljk							= {p}NilaiAgunanLJK	
								,tanggal_penilaian_ljk						= {p}TanggalPenilaianLJK	
								,nilai_agunan_independen					= {p}NilaiAgunanIndependen	
								,nama_penilai_independen					= {p}NamaPenilaiIndependen	
								,tanggal_penilaian_independen				= {p}TanggalPenilaianIndependen	
								,status_paripasu							= {p}StatusParipasu	
								,status_paripasu_ojk_code					= {p}StatusParipasuOJKCode	
								,status_paripasu_desc						= {p}StatusParipasuDesc	
								,presentase_paripasu						= {p}PresentaseParipasu	
								,status_kredit_join							= {p}StatusKreditJoin	
								,status_kredit_join_ojk_code				= {p}StatusKreditJoinOJKCode	
								,status_kredit_join_desc					= {p}StatusKreditJoinDesc	
								,diasuransikan								= {p}Diasuransikan	
								,diasuransikan_ojk_code						= {p}DiasuransikanOJKCode	
								,diasuransikan_desc							= {p}DiasuransikanDesc	
								,keterangan									= {p}Keterangan	
								,kode_kantor_cabang							= {p}KodeKantorCabang	
								,operasi_data								= {p}OperasiData	
								,operasi_data_ojk_code						= {p}OperasiDataOJKCode	
								,operasi_data_desc							= {p}OperasiDataDesc	
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

		public Task<List<A01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

