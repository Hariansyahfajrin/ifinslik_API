using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;
using System.Web.Mvc.Html;


namespace DAL
{
	public class D01Repository : BaseRepository, ID01Repository
	{

		private readonly string tableBase = "d01";
		private readonly string tableCompany = "sys_company";

		public async Task<List<D01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
	 				
					select

						        id		        								as	ID		        							
								,cif											as	Cif									
								,nik											as	Nik								
								,nama											as	Nama										
								,jenis_kelamin_desc								as	JenisKelaminDesc							
								,tanggal_lahir									as	TanggalLahir															

					from
						{tableBase}
					where
                                {tableBase}.form_transaction_id          = {p}FormTransactionID
                    AND
						(
							lower(cif)													like	lower({p}Keyword)	
							or	lower(nik)												like	lower({p}Keyword)	
							or	lower(nama)												like	lower({p}Keyword)		
							or	lower(jenis_kelamin_desc)									like	lower({p}Keyword)		
							or	cast(tanggal_lahir as varchar)							like	lower({p}Keyword)	

						)
					order by
						mod_date DESC
					"
			);

			return await _command.GetRows<D01>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				FormTransactionID = formTransactionID
			});
		}

		public async Task<D01> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

									{tableBase}.id		        								as	ID		        						
									,{tableBase}.form_transaction_id							as	FormTransactionID			
									,{tableBase}.flag_detail									as	FlagDetail					
									,{tableBase}.cif											as	Cif							
									,{tableBase}.jenis_identitas								as	JenisIdentitas				
									,{tableBase}.jenis_identitas_ojk_code						as	JenisIdentitasOJKCode		
									,{tableBase}.jenis_identitas_desc							as	JenisIdentitasDesc				
									,{tableBase}.nik											as	Nik								
									,{tableBase}.nama											as	Nama								
									,{tableBase}.nama_lengkap									as	NamaLengkap					
									,{tableBase}.kode_status_pendidikan							as	KodeStatusPendidikan				
									,{tableBase}.kode_status_pendidikan_ojk_code				as	KodeStatusPendidikanOJKCode	
									,{tableBase}.kode_status_pendidikan_desc					as	KodeStatusPendidikanDesc		
									,{tableBase}.jenis_kelamin									as	JenisKelamin						
									,{tableBase}.jenis_kelamin_ojk_code							as	JenisKelaminOJKCode				
									,{tableBase}.jenis_kelamin_desc								as	JenisKelaminDesc					
									,{tableBase}.tempat_lahir 									as	TempatLahir 		
									,{tableBase}.tanggal_lahir									as	TanggalLahir	
									,{tableBase}.npwp											as	Npwp	
									,{tableBase}.alamat											as	Alamat								
									,{tableBase}.kelurahan										as	Kelurahan													
									,{tableBase}.kecamatan										as	Kecamatan												
									,{tableBase}.kode_dati_2									as	KodeDati2	
									,{tableBase}.kode_dati_2_ojk_code							as	KodeDati2OJKCode	
									,{tableBase}.kode_dati_2_desc								as	KodeDati2Desc	
									,{tableBase}.kode_pos										as	KodePos				
									,{tableBase}.nomor_telepon									as	NomorTelepon			
									,{tableBase}.nomor_handphone								as	NomorHandphone	
									,{tableBase}.email											as	Email					
									,{tableBase}.kode_negara_domisili							as	KodeNegaraDomisili	
									,{tableBase}.kode_negara_domisili_ojk_code					as	KodeNegaraDomisiliOJKCode	
									,{tableBase}.kode_negara_domisili_desc						as	KodeNegaraDomisiliDesc	
									,{tableBase}.kode_pekerjaan									as	KodePekerjaan	
									,{tableBase}.kode_pekerjaan_ojk_code						as	KodePekerjaanOJKCode	
									,{tableBase}.kode_pekerjaan_desc							as	KodePekerjaanDesc	
									,{tableBase}.tempat_bekerja									as	TempatBekerja	
									,{tableBase}.kode_bidang_usaha								as	KodeBidangUsaha	
									,{tableBase}.kode_bidang_usaha_ojk_code						as	KodeBidangUsahaOJKCode	
									,{tableBase}.kode_bidang_usaha_desc							as	KodeBidangUsahaDesc	
									,{tableBase}.alamat_tempat_kerja							as	AlamatTempatKerja	
									,{tableBase}.penghasilan_kotor_per_tahun					as	PenghasilanKotorPerTahun		
									,{tableBase}.kode_sumber_penghasilan						as	KodeSumberPenghasilan	
									,{tableBase}.kode_sumber_penghasilan_ojk_code				as	KodeSumberPenghasilanOJKCode		
									,{tableBase}.kode_sumber_penghasilan_desc					as	KodeSumberPenghasilanDesc	
									,{tableBase}.jumlah_tanggungan								as	JumlahTanggungan	
									,{tableBase}.kode_hubungan_dengan_pelapor					as	KodeHubunganDenganPelapor		
									,{tableBase}.kode_hubungan_dengan_pelapor_ojk_code			as	KodeHubunganDenganPelaporOJKCode					
									,{tableBase}.kode_hubungan_dengan_pelapor_desc				as	KodeHubunganDenganPelaporDesc		
									,{tableBase}.kode_golongan_debitur							as	KodeGolonganDebitur	
									,{tableBase}.kode_golongan_debitur_ojk_code					as	KodeGolonganDebiturOJKCode		
									,{tableBase}.kode_golongan_debitur_desc						as	KodeGolonganDebiturDesc	
									,{tableBase}.status_perkawinan_debitur						as	StatusPerkawinanDebitur			
									,{tableBase}.status_perkawinan_debitur_ojk_code				as	StatusPerkawinanDebiturOJKCode				
									,{tableBase}.status_perkawinan_debitur_desc					as	StatusPerkawinanDebiturDesc	
									,{tableBase}.nomor_identitas_pasangan						as	NomorIdentitasPasangan	
									,{tableBase}.nama_pasangan									as	NamaPasangan		
									,{tableBase}.tanggal_lahir_pasangan							as	TanggalLahirPasangan			
									,{tableBase}.perjanjian_pisah_harta							as	PerjanjianPisahHarta			
									,{tableBase}.perjanjian_pisah_harta_ojk_code				as	PerjanjianPisahHartaOJKCode			
									,{tableBase}.perjanjian_pisah_harta_desc					as	PerjanjianPisahHartaDesc			
									,{tableBase}.melanggar_bmpk									as	MelanggarBmpk	
									,{tableBase}.melanggar_bmpk_ojk_code						as	MelanggarBmpkOJKCode	
									,{tableBase}.melanggar_bmpk_desc							as	MelanggarBmpkDesc	
									,{tableBase}.melampaui_bmpk									as  MelampauiBmpk	
									,{tableBase}.melampaui_bmpk_ojk_code						as	MelampauiBmpkOJKCode		
									,{tableBase}.melampaui_bmpk_desc							as	MelampauiBmpkDesc		
									,{tableBase}.nama_gadis_ibu_kandung							as	NamaGadisIbuKandung		
									,{tableBase}.kode_kantor_cabang								as	KodeKantorCabang		
									,{tableBase}.operasi_data									as	OperasiData	
									,{tableBase}.operasi_data_ojk_code							as	OperasiDataOJKCode			
									,{tableBase}.operasi_data_desc								as	OperasiDataDesc		
									,{tableBase}.period											as	Period										
									,sckc.name													as  SysCompanyKantorCabang
							from
								{tableBase}
							left join
							{tableCompany} as sckc on sckc.code = {tableBase}.kode_kantor_cabang	
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<D01>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}

		public async Task<int> Insert(IDbTransaction transaction, D01 model)
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
										,form_transaction_id							
										,flag_detail									
										,cif											
										,jenis_identitas								
										,jenis_identitas_ojk_code						
										,jenis_identitas_desc							
										,nik											
										,nama											
										,nama_lengkap									
										,kode_status_pendidikan							
										,kode_status_pendidikan_ojk_code				
										,kode_status_pendidikan_desc					
										,jenis_kelamin									
										,jenis_kelamin_ojk_code							
										,jenis_kelamin_desc								
										,tempat_lahir 									
										,tanggal_lahir									
										,npwp											
										,alamat											
										,kelurahan										
										,kecamatan										
										,kode_dati_2									
										,kode_dati_2_ojk_code							
										,kode_dati_2_desc								
										,kode_pos										
										,nomor_telepon									
										,nomor_handphone								
										,email											
										,kode_negara_domisili							
										,kode_negara_domisili_ojk_code					
										,kode_negara_domisili_desc						
										,kode_pekerjaan									
										,kode_pekerjaan_ojk_code						
										,kode_pekerjaan_desc							
										,tempat_bekerja									
										,kode_bidang_usaha								
										,kode_bidang_usaha_ojk_code						
										,kode_bidang_usaha_desc							
										,alamat_tempat_kerja							
										,penghasilan_kotor_per_tahun					
										,kode_sumber_penghasilan						
										,kode_sumber_penghasilan_ojk_code				
										,kode_sumber_penghasilan_desc					
										,jumlah_tanggungan								
										,kode_hubungan_dengan_pelapor					
										,kode_hubungan_dengan_pelapor_ojk_code			
										,kode_hubungan_dengan_pelapor_desc				
										,kode_golongan_debitur							
										,kode_golongan_debitur_ojk_code					
										,kode_golongan_debitur_desc						
										,status_perkawinan_debitur						
										,status_perkawinan_debitur_ojk_code				
										,status_perkawinan_debitur_desc					
										,nomor_identitas_pasangan						
										,nama_pasangan									
										,tanggal_lahir_pasangan							
										,perjanjian_pisah_harta							
										,perjanjian_pisah_harta_ojk_code				
										,perjanjian_pisah_harta_desc					
										,melanggar_bmpk									
										,melanggar_bmpk_ojk_code						
										,melanggar_bmpk_desc							
										,melampaui_bmpk									
										,melampaui_bmpk_ojk_code						
										,melampaui_bmpk_desc							
										,nama_gadis_ibu_kandung							
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
										,{p}FormTransactionID			
										,{p}FlagDetail					
										,{p}Cif							
										,{p}JenisIdentitas				
										,{p}JenisIdentitasOJKCode		
										,{p}JenisIdentitasDesc				
										,{p}Nik								
										,{p}Nama								
										,{p}NamaLengkap					
										,{p}KodeStatusPendidikan				
										,{p}KodeStatusPendidikanOJKCode	
										,{p}KodeStatusPendidikanDesc		
										,{p}JenisKelamin						
										,{p}JenisKelaminOJKCode				
										,{p}JenisKelaminDesc					
										,{p}TempatLahir 		
										,{p}TanggalLahir	
										,{p}Npwp	
										,{p}Alamat								
										,{p}Kelurahan								
										,{p}Kecamatan								
										,{p}KodeDati2	
										,{p}KodeDati2OJKCode	
										,{p}KodeDati2Desc	
										,{p}KodePos				
										,{p}NomorTelepon			
										,{p}NomorHandphone	
										,{p}Email					
										,{p}KodeNegaraDomisili	
										,{p}KodeNegaraDomisiliOJKCode	
										,{p}KodeNegaraDomisiliDesc	
										,{p}KodePekerjaan	
										,{p}KodePekerjaanOJKCode	
										,{p}KodePekerjaanDesc	
										,{p}TempatBekerja	
										,{p}KodeBidangUsaha	
										,{p}KodeBidangUsahaOJKCode	
										,{p}KodeBidangUsahaDesc	
										,{p}AlamatTempatKerja	
										,{p}PenghasilanKotorPerTahun		
										,{p}KodeSumberPenghasilan	
										,{p}KodeSumberPenghasilanOJKCode		
										,{p}KodeSumberPenghasilanDesc	
										,{p}JumlahTanggungan	
										,{p}KodeHubunganDenganPelapor		
										,{p}KodeHubunganDenganPelaporOJKCode		
										,{p}KodeHubunganDenganPelaporDesc		
										,{p}KodeGolonganDebitur	
										,{p}KodeGolonganDebiturOJKCode		
										,{p}KodeGolonganDebiturDesc	
										,{p}StatusPerkawinanDebitur			
										,{p}StatusPerkawinanDebiturOJKCode			
										,{p}StatusPerkawinanDebiturDesc	
										,{p}NomorIdentitasPasangan	
										,{p}NamaPasangan		
										,{p}TanggalLahirPasangan			
										,{p}PerjanjianPisahHarta			
										,{p}PerjanjianPisahHartaOJKCode			
										,{p}PerjanjianPisahHartaDesc			
										,{p}MelanggarBmpk	
										,{p}MelanggarBmpkOJKCode	
										,{p}MelanggarBmpkDesc	
										,{p}MelampauiBmpk	
										,{p}MelampauiBmpkOJKCode		
										,{p}MelampauiBmpkDesc		
										,{p}NamaGadisIbuKandung		
										,{p}KodeKantorCabang		
										,{p}OperasiData	
										,{p}OperasiDataOJKCode			
										,{p}OperasiDataDesc		
										,{p}Period									
									
									)
								";

			return await _command.Insert(transaction, query, model);
		}
		public async Task<int> UpdateByID(IDbTransaction transaction, D01 model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								flag_detail									= {p}FlagDetail					
								,cif										= {p}Cif								
								,jenis_identitas							= {p}JenisIdentitas					
								,jenis_identitas_ojk_code					= {p}JenisIdentitasOJKCode			
								,jenis_identitas_desc						= {p}JenisIdentitasDesc					
								,nik										= {p}Nik									
								,nama										= {p}Nama									
								,nama_lengkap								= {p}NamaLengkap						
								,kode_status_pendidikan						= {p}KodeStatusPendidikan					
								,kode_status_pendidikan_ojk_code			= {p}KodeStatusPendidikanOJKCode		
								,kode_status_pendidikan_desc				= {p}KodeStatusPendidikanDesc			
								,jenis_kelamin								= {p}JenisKelamin							
								,jenis_kelamin_ojk_code						= {p}JenisKelaminOJKCode					
								,jenis_kelamin_desc							= {p}JenisKelaminDesc						
								,tempat_lahir 								= {p}TempatLahir 			
								,tanggal_lahir								= {p}TanggalLahir		
								,npwp										= {p}Npwp		
								,alamat										= {p}Alamat									
								,kelurahan									= {p}Kelurahan									
								,kecamatan									= {p}Kecamatan									
								,kode_dati_2								= {p}KodeDati2		
								,kode_dati_2_ojk_code						= {p}KodeDati2OJKCode		
								,kode_dati_2_desc							= {p}KodeDati2Desc		
								,kode_pos									= {p}KodePos					
								,nomor_telepon								= {p}NomorTelepon				
								,nomor_handphone							= {p}NomorHandphone		
								,email										= {p}Email						
								,kode_negara_domisili						= {p}KodeNegaraDomisili		
								,kode_negara_domisili_ojk_code				= {p}KodeNegaraDomisiliOJKCode		
								,kode_negara_domisili_desc					= {p}KodeNegaraDomisiliDesc		
								,kode_pekerjaan								= {p}KodePekerjaan		
								,kode_pekerjaan_ojk_code					= {p}KodePekerjaanOJKCode		
								,kode_pekerjaan_desc						= {p}KodePekerjaanDesc		
								,tempat_bekerja								= {p}TempatBekerja		
								,kode_bidang_usaha							= {p}KodeBidangUsaha		
								,kode_bidang_usaha_ojk_code					= {p}KodeBidangUsahaOJKCode		
								,kode_bidang_usaha_desc						= {p}KodeBidangUsahaDesc		
								,alamat_tempat_kerja						= {p}AlamatTempatKerja		
								,penghasilan_kotor_per_tahun				= {p}PenghasilanKotorPerTahun			
								,kode_sumber_penghasilan					= {p}KodeSumberPenghasilan		
								,kode_sumber_penghasilan_ojk_code			= {p}KodeSumberPenghasilanOJKCode			
								,kode_sumber_penghasilan_desc				= {p}KodeSumberPenghasilanDesc		
								,jumlah_tanggungan							= {p}JumlahTanggungan		
								,kode_hubungan_dengan_pelapor				= {p}KodeHubunganDenganPelapor			
								,kode_hubungan_dengan_pelapor_ojk_code		= {p}KodeHubunganDenganPelaporOJKCode			
								,kode_hubungan_dengan_pelapor_desc			= {p}KodeHubunganDenganPelaporDesc			
								,kode_golongan_debitur						= {p}KodeGolonganDebitur		
								,kode_golongan_debitur_ojk_code				= {p}KodeGolonganDebiturOJKCode			
								,kode_golongan_debitur_desc					= {p}KodeGolonganDebiturDesc		
								,status_perkawinan_debitur					= {p}StatusPerkawinanDebitur				
								,status_perkawinan_debitur_ojk_code			= {p}StatusPerkawinanDebiturOJKCode				
								,status_perkawinan_debitur_desc				= {p}StatusPerkawinanDebiturDesc		
								,nomor_identitas_pasangan					= {p}NomorIdentitasPasangan		
								,nama_pasangan								= {p}NamaPasangan			
								,tanggal_lahir_pasangan						= {p}TanggalLahirPasangan				
								,perjanjian_pisah_harta						= {p}PerjanjianPisahHarta				
								,perjanjian_pisah_harta_ojk_code			= {p}PerjanjianPisahHartaOJKCode				
								,perjanjian_pisah_harta_desc				= {p}PerjanjianPisahHartaDesc				
								,melanggar_bmpk								= {p}MelanggarBmpk		
								,melanggar_bmpk_ojk_code					= {p}MelanggarBmpkOJKCode		
								,melanggar_bmpk_desc						= {p}MelanggarBmpkDesc		
								,melampaui_bmpk								= {p}MelampauiBmpk		
								,melampaui_bmpk_ojk_code					= {p}MelampauiBmpkOJKCode			
								,melampaui_bmpk_desc						= {p}MelampauiBmpkDesc			
								,nama_gadis_ibu_kandung						= {p}NamaGadisIbuKandung			
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
		public async Task<List<D01>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? financeCompanyType)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
				select
          additional.cif           as cif
          ,additional.nama_lengkap as namalengkap
        from
          (
            select
              d01.cif
              ,d01.nama_lengkap
              ,d01.period
              ,d01.mod_date
            from
              d01
              inner join form_transaction ftr on ftr.id = d01.form_transaction_id
            where
              ftr.finance_company_type = {p}FinanceCompanyType
            
            union all

            select
              d02.cif
              ,d02.nama_badan_usaha
              ,d02.period
              ,d02.mod_date
            from
              d02
              inner join form_transaction ftr on ftr.id = d02.form_transaction_id
          ) as additional
        where
          additional.period = {p}Period
          and 
          (
            lower(additional.cif)             like lower({p}Keyword) 
            or lower(additional.nama_lengkap) like lower({p}Keyword) 
            or lower(additional.period)       like lower({p}Keyword) 
          )
				order by
					additional.mod_date DESC
			    "
			);

			return await _command.GetRows<D01>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				Period = period,
				FinanceCompanyType = financeCompanyType,
			});
		}

		public Task<List<D01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

