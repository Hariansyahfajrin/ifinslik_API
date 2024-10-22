using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class F01Repository : BaseRepository, IF01Repository
	{

		private readonly string tableBase = "f01";
		private readonly string tableD01 = "d01";
		private readonly string tableD02 = "d02";
		private readonly string tableCompany = "sys_company";
		private readonly string tableForm = "form_transaction";
		private readonly string tableGeneralSubCode = "sys_general_subcode";

		public async Task<List<F01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
					$@"
	 				
				select
						         {tableBase}.id		        						as	ID
								,{tableBase}.form_transaction_id					as  FormTransactionID																										
								,{tableBase}.nomor_rekening							as  NomorRekening													
								,{tableBase}.cif									as  Cif																													
								,{tableBase}.kode_sifat_kredit_desc					as  KodeSifatKreditDesc																																										
								,{tableBase}.kode_jenis_kredit_desc					as  KodeJenisKreditDesc		
								,sgsc.description									as  SysGeneralSubCodeSlikGroup																									
								,case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end as NamaLengkap
					from
						{tableBase}
					left join {tableD01} as d1 on d1.cif = {tableBase}.cif
           			left join {tableD02} as d2 on d2.cif = {tableBase}.cif
					inner join
					{tableGeneralSubCode} as sgsc on sgsc.code = {tableBase}.slik_group
					where
                                {tableBase}.form_transaction_id          = {p}FormTransactionID
                    AND
						(
							lower    ({tableBase}.nomor_rekening)							like	lower({p}Keyword)	
							or	lower({tableBase}.cif)										like	lower({p}Keyword)	
							or	lower({tableBase}.kode_sifat_kredit_desc)					like	lower({p}Keyword)		
							or	lower({tableBase}.kode_jenis_kredit_desc)					like	lower({p}Keyword)		
							or	lower(sgsc.description	)									like	lower({p}Keyword)	
							or lower(case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end) like lower({p}Keyword)		
						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<F01>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				FormTransactionID = formTransactionID
			});
		}

		public async Task<F01> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

									{tableBase}.id		        							as	ID
									,{tableBase}.form_transaction_id						as  FormTransactionID																
									,{tableBase}.flag_detail								as  FlagDetail									
									,{tableBase}.nomor_rekening								as  NomorRekening												
									,{tableBase}.cif										as  Cif																														
									,{tableBase}.kode_sifat_kredit							as  KodeSifatKredit													
									,{tableBase}.kode_sifat_kredit_ojk_code					as  KodeSifatKreditOJKCode																	
									,{tableBase}.kode_sifat_kredit_desc						as  KodeSifatKreditDesc																												
									,{tableBase}.kode_jenis_kredit							as  KodeJenisKredit									
									,{tableBase}.kode_jenis_kredit_ojk_code					as  KodeJenisKreditOJKCode													
									,{tableBase}.kode_jenis_kredit_desc						as  KodeJenisKreditDesc									
									,{tableBase}.kode_skim									as  KodeSkim													
									,{tableBase}.kode_skim_ojk_code							as  KodeSkimOJKCode												
									,{tableBase}.kode_skim_desc								as  KodeSkimDesc											
									,{tableBase}.nomor_akad_awal 							as  NomorAkadAwal 										
									,{tableBase}.tanggal_akad_awal 							as  TanggalAkadAwal 										
									,{tableBase}.nomor_akad_akhir 							as  NomorAkadAkhir 														
									,{tableBase}.tanggal_akad_akhir 						as  TanggalAkadAkhir 											
									,{tableBase}.baru_perpanjangan							as  BaruPerpanjangan											
									,{tableBase}.baru_perpanjangan_ojk_code					as  BaruPerpanjanganOJKCode												
									,{tableBase}.baru_perpanjangan_desc						as  BaruPerpanjanganDesc												
									,{tableBase}.tanggal_awal_kredit						as  TanggalAwalKredit										
									,{tableBase}.tanggal_mulai								as  TanggalMulai							
									,{tableBase}.tanggal_jatuh_tempo						as  TanggalJatuhTempo										
									,{tableBase}.kode_kategori_debitur						as  KodeKategoriDebitur									
									,{tableBase}.kode_kategori_debitur_ojk_code				as  KodeKategoriDebiturOJKCode												
									,{tableBase}.kode_kategori_debitur_desc					as  KodeKategoriDebiturDesc										
									,{tableBase}.kode_jenis_penggunaan						as  KodeJenisPenggunaan								
									,{tableBase}.kode_jenis_penggunaan_ojk_code				as  KodeJenisPenggunaanOJKCode									
									,{tableBase}.kode_jenis_penggunaan_desc					as  KodeJenisPenggunaanDesc											
									,{tableBase}.kode_orientasi_penggunaan					as  KodeOrientasiPenggunaan							
									,{tableBase}.kode_orientasi_penggunaan_ojk_code			as  KodeOrientasiPenggunaanOJKCode											
									,{tableBase}.kode_orientasi_penggunaan_desc				as  KodeOrientasiPenggunaanDesc															
									,{tableBase}.kode_sektor_ekonomi						as  KodeSektorEkonomi												
									,{tableBase}.kode_sektor_ekonomi_ojk_code				as  KodeSektorEkonomiOJKCode																
									,{tableBase}.kode_sektor_ekonomi_desc					as  KodeSektorEkonomiDesc						
									,{tableBase}.kode_dati_2								as  KodeDati2																							
									,{tableBase}.kode_dati_2_ojk_code						as  KodeDati2OJKCode												
									,{tableBase}.kode_dati_2_desc							as  KodeDati2Desc																																											
									,{tableBase}.nilai_proyek								as  NilaiProyek																		
									,{tableBase}.kode_valuta								as  KodeValuta							
									,{tableBase}.kode_valuta_ojk_code						as  KodeValutaOJKCode														
									,{tableBase}.kode_valuta_desc							as  KodeValutaDesc												
									,{tableBase}.presentase_suku_bunga						as  PresentaseSukuBunga												
									,{tableBase}.jenis_suku_bunga							as  JenisSukuBunga												
									,{tableBase}.jenis_suku_bunga_ojk_code					as  JenisSukuBungaOJKCode																
									,{tableBase}.jenis_suku_bunga_desc						as  JenisSukuBungaDesc																	
									,{tableBase}.kredit_program_pemerintah					as  KreditProgramPemerintah																		
									,{tableBase}.kredit_program_pemerintah_ojk_code			as  KreditProgramPemerintahOJKCode																			
									,{tableBase}.kredit_program_pemerintah_desc				as  KreditProgramPemerintahDesc																
									,{tableBase}.takeover_dari								as  TakeoverDari														
									,{tableBase}.takeover_dari_ojk_code						as  TakeoverDariOJKCode																			
									,{tableBase}.takeover_dari_desc							as  TakeoverDariDesc																			
									,{tableBase}.sumber_dana								as  SumberDana																			
									,{tableBase}.sumber_dana_ojk_code						as  SumberDanaOJKCode																				
									,{tableBase}.sumber_dana_desc							as  SumberDanaDesc																	
									,{tableBase}.plafon_awal								as  PlafonAwal																																	
									,{tableBase}.plafon										as  Plafon																		
									,{tableBase}.realisasi_pencairan						as  RealisasiPencairan																	
									,{tableBase}.denda										as  Denda											
									,{tableBase}.baki_debet									as  BakiDebet															
									,{tableBase}.nilai_dalam_mata_uang_asal					as  NilaiDalamMataUangAsal																	
									,{tableBase}.kode_kolektibilitas						as  KodeKolektibilitas												
									,{tableBase}.kode_kolektibilitas_ojk_code				as  KodeKolektibilitasOJKCode																
									,{tableBase}.kode_kolektibilitas_desc					as  KodeKolektibilitasDesc																		
									,{tableBase}.tanggal_macet								as  TanggalMacet														
									,{tableBase}.kode_sebab_macet							as  KodeSebabMacet																																
									,{tableBase}.kode_sebab_macet_ojk_code					as  KodeSebabMacetOJKCode																
									,{tableBase}.kode_sebab_macet_desc						as  KodeSebabMacetDesc												
									,{tableBase}.tunggakan_pokok							as  TunggakanPokok																	
									,{tableBase}.tunggakan_bunga							as  TunggakanBunga																	
									,{tableBase}.jumlah_hari_tunggakan						as  JumlahHariTunggakan														
									,{tableBase}.frekuensi_tunggakan						as  FrekuensiTunggakan																															
									,{tableBase}.frekuensi_restrukturisasi					as  FrekuensiRestrukturisasi														
									,{tableBase}.tanggal_restrukturisasi_awal				as  TanggalRestrukturisasiAwal 																			 
									,{tableBase}.tanggal_restrukturisasi_akhir 				as  TanggalRestrukturisasiAkhir 											
									,{tableBase}.kode_cara_restrukturisasi					as  KodeCaraRestrukturisasi										
									,{tableBase}.kode_cara_restrukturisasi_ojk_code			as  KodeCaraRestrukturisasiOJKCode											
									,{tableBase}.kode_cara_restrukturisasi_desc				as  KodeCaraRestrukturisasiDesc										
									,{tableBase}.kode_kondisi								as  KodeKondisi						
									,{tableBase}.kode_kondisi_ojk_code						as  KodeKondisiOJKCode											
									,{tableBase}.kode_kondisi_desc							as  KodeKondisiDesc							
									,{tableBase}.tanggal_kondisi							as  TanggalKondisi							
									,{tableBase}.keterangan									as  Keterangan						
									,{tableBase}.kode_kantor_cabang							as  KodeKantorCabang											
									,{tableBase}.operasi_data								as  OperasiData																				
									,{tableBase}.operasi_data_ojk_code						as  OperasiDataOJKCode						
									,{tableBase}.operasi_data_desc							as  OperasiDataDesc									
									,{tableBase}.period										as  Period		
									,{tableBase}.slik_group									as  SlikGroup	
									,{tableBase}.is_syariah									as  IsSyariah						
									,sckc.name												as  SysCompanyKantorCabang
									,sgsc.description										as  SysGeneralSubCodeSlikGroup
							from
								{tableBase}
							left join
							{tableCompany} as sckc on sckc.code = {tableBase}.kode_kantor_cabang	
							inner join
							{tableGeneralSubCode} as sgsc on sgsc.code = {tableBase}.slik_group
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<F01>(
			transaction, query, new { ID = id });

			return result;
			//  return await _command.GetRow<MasterTemplate>(transaction, query, new { ID = id });
		}


		public async Task<int> Insert(IDbTransaction transaction, F01 model)
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
										,nomor_rekening						
										,cif												
										,kode_sifat_kredit					
										,kode_sifat_kredit_ojk_code			
										,kode_sifat_kredit_desc				
										,kode_jenis_kredit					
										,kode_jenis_kredit_ojk_code			
										,kode_jenis_kredit_desc				
										,kode_skim							
										,kode_skim_ojk_code					
										,kode_skim_desc						
										,nomor_akad_awal 					
										,tanggal_akad_awal 					
										,nomor_akad_akhir 					
										,tanggal_akad_akhir 				
										,baru_perpanjangan					
										,baru_perpanjangan_ojk_code			
										,baru_perpanjangan_desc				
										,tanggal_awal_kredit				
										,tanggal_mulai						
										,tanggal_jatuh_tempo				
										,kode_kategori_debitur				
										,kode_kategori_debitur_ojk_code		
										,kode_kategori_debitur_desc			
										,kode_jenis_penggunaan				
										,kode_jenis_penggunaan_ojk_code		
										,kode_jenis_penggunaan_desc			
										,kode_orientasi_penggunaan			
										,kode_orientasi_penggunaan_ojk_code	
										,kode_orientasi_penggunaan_desc		
										,kode_sektor_ekonomi				
										,kode_sektor_ekonomi_ojk_code		
										,kode_sektor_ekonomi_desc			
										,kode_dati_2						
										,kode_dati_2_ojk_code				
										,kode_dati_2_desc					
										,nilai_proyek						
										,kode_valuta						
										,kode_valuta_ojk_code				
										,kode_valuta_desc					
										,presentase_suku_bunga				
										,jenis_suku_bunga					
										,jenis_suku_bunga_ojk_code			
										,jenis_suku_bunga_desc				
										,kredit_program_pemerintah			
										,kredit_program_pemerintah_ojk_code	
										,kredit_program_pemerintah_desc		
										,takeover_dari						
										,takeover_dari_ojk_code				
										,takeover_dari_desc					
										,sumber_dana						
										,sumber_dana_ojk_code				
										,sumber_dana_desc					
										,plafon_awal						
										,plafon								
										,realisasi_pencairan				
										,denda								
										,baki_debet							
										,nilai_dalam_mata_uang_asal			
										,kode_kolektibilitas				
										,kode_kolektibilitas_ojk_code		
										,kode_kolektibilitas_desc			
										,tanggal_macet						
										,kode_sebab_macet					
										,kode_sebab_macet_ojk_code			
										,kode_sebab_macet_desc				
										,tunggakan_pokok					
										,tunggakan_bunga					
										,jumlah_hari_tunggakan				
										,frekuensi_tunggakan				
										,frekuensi_restrukturisasi			
										,tanggal_restrukturisasi_awal		
										,tanggal_restrukturisasi_akhir 		
										,kode_cara_restrukturisasi			
										,kode_cara_restrukturisasi_ojk_code	
										,kode_cara_restrukturisasi_desc		
										,kode_kondisi						
										,kode_kondisi_ojk_code				
										,kode_kondisi_desc					
										,tanggal_kondisi					
										,keterangan							
										,kode_kantor_cabang					
										,operasi_data						
										,operasi_data_ojk_code				
										,operasi_data_desc
										,slik_group					
										,period	
										,is_syariah							
																					
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
										,{p}NomorRekening							
										,{p}Cif																
										,{p}KodeSifatKredit							
										,{p}KodeSifatKreditOJKCode					
										,{p}KodeSifatKreditDesc						
										,{p}KodeJenisKredit						
										,{p}KodeJenisKreditOJKCode					
										,{p}KodeJenisKreditDesc					
										,{p}KodeSkim								
										,{p}KodeSkimOJKCode							
										,{p}KodeSkimDesc							
										,{p}NomorAkadAwal 							
										,{p}TanggalAkadAwal 						
										,{p}NomorAkadAkhir 							
										,{p}TanggalAkadAkhir 						
										,{p}BaruPerpanjangan						
										,{p}BaruPerpanjanganOJKCode					
										,{p}BaruPerpanjanganDesc					
										,{p}TanggalAwalKredit						
										,{p}TanggalMulai							
										,{p}TanggalJatuhTempo						
										,{p}KodeKategoriDebitur						
										,{p}KodeKategoriDebiturOJKCode				
										,{p}KodeKategoriDebiturDesc					
										,{p}KodeJenisPenggunaan					
										,{p}KodeJenisPenggunaanOJKCode				
										,{p}KodeJenisPenggunaanDesc					
										,{p}KodeOrientasiPenggunaan					
										,{p}KodeOrientasiPenggunaanOJKCode			
										,{p}KodeOrientasiPenggunaanDesc				
										,{p}KodeSektorEkonomi						
										,{p}KodeSektorEkonomiOJKCode				
										,{p}KodeSektorEkonomiDesc					
										,{p}KodeDati2								
										,{p}KodeDati2OJKCode						
										,{p}KodeDati2Desc							
										,{p}NilaiProyek								
										,{p}KodeValuta							
										,{p}KodeValutaOJKCode						
										,{p}KodeValutaDesc							
										,{p}PresentaseSukuBunga						
										,{p}JenisSukuBunga							
										,{p}JenisSukuBungaOJKCode					
										,{p}JenisSukuBungaDesc						
										,{p}KreditProgramPemerintah					
										,{p}KreditProgramPemerintahOJKCode			
										,{p}KreditProgramPemerintahDesc				
										,{p}TakeoverDari							
										,{p}TakeoverDariOJKCode						
										,{p}TakeoverDariDesc						
										,{p}SumberDana								
										,{p}SumberDanaOJKCode						
										,{p}SumberDanaDesc							
										,{p}PlafonAwal								
										,{p}Plafon									
										,{p}RealisasiPencairan						
										,{p}Denda									
										,{p}BakiDebet								
										,{p}NilaiDalamMataUangAsal					
										,{p}KodeKolektibilitas						
										,{p}KodeKolektibilitasOJKCode				
										,{p}KodeKolektibilitasDesc					
										,{p}TanggalMacet							
										,{p}KodeSebabMacet							
										,{p}KodeSebabMacetOJKCode					
										,{p}KodeSebabMacetDesc						
										,{p}TunggakanPokok							
										,{p}TunggakanBunga							
										,{p}JumlahHariTunggakan						
										,{p}FrekuensiTunggakan						
										,{p}FrekuensiRestrukturisasi				
										,{p}TanggalRestrukturisasiAwal 				
										,{p}TanggalRestrukturisasiAkhir 			
										,{p}KodeCaraRestrukturisasi					
										,{p}KodeCaraRestrukturisasiOJKCode			
										,{p}KodeCaraRestrukturisasiDesc				
										,{p}KodeKondisi						
										,{p}KodeKondisiOJKCode						
										,{p}KodeKondisiDesc							
										,{p}TanggalKondisi							
										,{p}Keterangan						
										,{p}KodeKantorCabang						
										,{p}OperasiData								
										,{p}OperasiDataOJKCode						
										,{p}OperasiDataDesc		
										,{p}SlikGroup					
										,{p}Period			
										,{p}IsSyariah						
									)
								";

			return await _command.Insert(transaction, query, model);
		}
		public async Task<int> UpdateByID(IDbTransaction transaction, F01 model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								flag_detail								= {p}FlagDetail								
								,nomor_rekening							= {p}NomorRekening							
								,cif									= {p}Cif																	
								,kode_sifat_kredit						= {p}KodeSifatKredit								
								,kode_sifat_kredit_ojk_code				= {p}KodeSifatKreditOJKCode					
								,kode_sifat_kredit_desc					= {p}KodeSifatKreditDesc							
								,kode_jenis_kredit						= {p}KodeJenisKredit						
								,kode_jenis_kredit_ojk_code				= {p}KodeJenisKreditOJKCode					
								,kode_jenis_kredit_desc					= {p}KodeJenisKreditDesc					
								,kode_skim								= {p}KodeSkim								
								,kode_skim_ojk_code						= {p}KodeSkimOJKCode								
								,kode_skim_desc							= {p}KodeSkimDesc							
								,nomor_akad_awal 						= {p}NomorAkadAwal 							
								,tanggal_akad_awal 						= {p}TanggalAkadAwal 						
								,nomor_akad_akhir 						= {p}NomorAkadAkhir 								
								,tanggal_akad_akhir 					= {p}TanggalAkadAkhir 						
								,baru_perpanjangan						= {p}BaruPerpanjangan						
								,baru_perpanjangan_ojk_code				= {p}BaruPerpanjanganOJKCode						
								,baru_perpanjangan_desc					= {p}BaruPerpanjanganDesc					
								,tanggal_awal_kredit					= {p}TanggalAwalKredit						
								,tanggal_mulai							= {p}TanggalMulai							
								,tanggal_jatuh_tempo					= {p}TanggalJatuhTempo						
								,kode_kategori_debitur					= {p}KodeKategoriDebitur							
								,kode_kategori_debitur_ojk_code			= {p}KodeKategoriDebiturOJKCode				
								,kode_kategori_debitur_desc				= {p}KodeKategoriDebiturDesc						
								,kode_jenis_penggunaan					= {p}KodeJenisPenggunaan					
								,kode_jenis_penggunaan_ojk_code			= {p}KodeJenisPenggunaanOJKCode				
								,kode_jenis_penggunaan_desc				= {p}KodeJenisPenggunaanDesc						
								,kode_orientasi_penggunaan				= {p}KodeOrientasiPenggunaan						
								,kode_orientasi_penggunaan_ojk_code		= {p}KodeOrientasiPenggunaanOJKCode			
								,kode_orientasi_penggunaan_desc			= {p}KodeOrientasiPenggunaanDesc					
								,kode_sektor_ekonomi					= {p}KodeSektorEkonomi						
								,kode_sektor_ekonomi_ojk_code			= {p}KodeSektorEkonomiOJKCode				
								,kode_sektor_ekonomi_desc				= {p}KodeSektorEkonomiDesc					
								,kode_dati_2							= {p}KodeDati2								
								,kode_dati_2_ojk_code					= {p}KodeDati2OJKCode						
								,kode_dati_2_desc						= {p}KodeDati2Desc							
								,nilai_proyek							= {p}NilaiProyek									
								,kode_valuta							= {p}KodeValuta							
								,kode_valuta_ojk_code					= {p}KodeValutaOJKCode						
								,kode_valuta_desc						= {p}KodeValutaDesc							
								,presentase_suku_bunga					= {p}PresentaseSukuBunga							
								,jenis_suku_bunga						= {p}JenisSukuBunga							
								,jenis_suku_bunga_ojk_code				= {p}JenisSukuBungaOJKCode					
								,jenis_suku_bunga_desc					= {p}JenisSukuBungaDesc						
								,kredit_program_pemerintah				= {p}KreditProgramPemerintah						
								,kredit_program_pemerintah_ojk_code		= {p}KreditProgramPemerintahOJKCode			
								,kredit_program_pemerintah_desc			= {p}KreditProgramPemerintahDesc					
								,takeover_dari							= {p}TakeoverDari							
								,takeover_dari_ojk_code					= {p}TakeoverDariOJKCode							
								,takeover_dari_desc						= {p}TakeoverDariDesc						
								,sumber_dana							= {p}SumberDana								
								,sumber_dana_ojk_code					= {p}SumberDanaOJKCode						
								,sumber_dana_desc						= {p}SumberDanaDesc							
								,plafon_awal							= {p}PlafonAwal								
								,plafon									= {p}Plafon									
								,realisasi_pencairan					= {p}RealisasiPencairan						
								,denda									= {p}Denda									
								,baki_debet								= {p}BakiDebet								
								,nilai_dalam_mata_uang_asal				= {p}NilaiDalamMataUangAsal					
								,kode_kolektibilitas					= {p}KodeKolektibilitas						
								,kode_kolektibilitas_ojk_code			= {p}KodeKolektibilitasOJKCode				
								,kode_kolektibilitas_desc				= {p}KodeKolektibilitasDesc					
								,tanggal_macet							= {p}TanggalMacet							
								,kode_sebab_macet						= {p}KodeSebabMacet							
								,kode_sebab_macet_ojk_code				= {p}KodeSebabMacetOJKCode					
								,kode_sebab_macet_desc					= {p}KodeSebabMacetDesc						
								,tunggakan_pokok						= {p}TunggakanPokok							
								,tunggakan_bunga						= {p}TunggakanBunga							
								,jumlah_hari_tunggakan					= {p}JumlahHariTunggakan							
								,frekuensi_tunggakan					= {p}FrekuensiTunggakan						
								,frekuensi_restrukturisasi				= {p}FrekuensiRestrukturisasi				
								,tanggal_restrukturisasi_awal			= {p}TanggalRestrukturisasiAwal 					
								,tanggal_restrukturisasi_akhir 			= {p}TanggalRestrukturisasiAkhir 			
								,kode_cara_restrukturisasi				= {p}KodeCaraRestrukturisasi						
								,kode_cara_restrukturisasi_ojk_code		= {p}KodeCaraRestrukturisasiOJKCode			
								,kode_cara_restrukturisasi_desc			= {p}KodeCaraRestrukturisasiDesc					
								,kode_kondisi							= {p}KodeKondisi						
								,kode_kondisi_ojk_code					= {p}KodeKondisiOJKCode						
								,kode_kondisi_desc						= {p}KodeKondisiDesc								
								,tanggal_kondisi						= {p}TanggalKondisi							
								,keterangan								= {p}Keterangan						
								,kode_kantor_cabang						= {p}KodeKantorCabang						
								,operasi_data							= {p}OperasiData									
								,operasi_data_ojk_code					= {p}OperasiDataOJKCode						
								,operasi_data_desc						= {p}OperasiDataDesc
								,slik_group								= {p}SlikGroup									
								,period									= {p}Period			
								,is_syariah								= {p}IsSyariah							
								,mod_date       				 		= {p}ModDate
								,mod_by							 		= {p}ModBy
								,mod_ip_address 				 		= {p}ModIPAddress		
								
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
		public async Task<List<F01>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? companyCode, string? financeCompanyType)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
            select
                {tableBase}.id as ID,
                {tableBase}.nomor_rekening as NomorRekening,
                {tableBase}.cif as Cif,
				case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end as NamaLengkap
            from
                {tableBase}
            left join {tableD01} as d1 on d1.cif = {tableBase}.cif
            left join {tableD02} as d2 on d2.cif = {tableBase}.cif
            inner join {tableForm} as ftr on ftr.id = {tableBase}.form_transaction_id
            where
                {tableBase}.period = {p}Period
                and ftr.company_code = {p}CompanyCode
                and ftr.finance_company_type = {p}FinanceCompanyType
                and 
				(
                    lower({tableBase}.nomor_rekening) like lower({p}Keyword) 
                    or lower({tableBase}.cif) like lower({p}Keyword) 
                    or lower(case when d1.nama_lengkap is null then d2.nama_badan_usaha else d1.nama_lengkap end) like lower({p}Keyword)
                )
            order by
                {tableBase}.mod_date DESC
       "
					);
			return await _command.GetRows<F01>(transaction, query, new
			{
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset,
				Period = period,
				CompanyCode = companyCode,
				FinanceCompanyType = financeCompanyType,

			});
		}

		public Task<List<F01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

