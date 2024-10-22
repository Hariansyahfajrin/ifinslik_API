
namespace Domain.Models
{
    public class F01History : BaseModel
    {
        public string? FormTransactionHistoryID { get; set; }
        public string? FlagDetail { get; set; }
        public string? NomorRekening { get; set; }
        public string? Cif { get; set; }
        public string? NamaLengkap { get; set; }
        public string? KodeSifatKredit { get; set; }
        public string? KodeSifatKreditOJKCode { get; set; }
        public string? KodeSifatKreditDesc { get; set; }
        public string? KodeJenisKredit { get; set; }
        public string? KodeJenisKreditOJKCode { get; set; }
        public string? KodeJenisKreditDesc { get; set; }
        public string? KodeSkim { get; set; }
        public string? KodeSkimOJKCode { get; set; }
        public string? KodeSkimDesc { get; set; }
        public string? NomorAkadAwal { get; set; }
        public DateTime? TanggalAkadAwal { get; set; }
        public string? NomorAkadAkhir { get; set; }
        public DateTime? TanggalAkadAkhir { get; set; }
        public string? BaruPerpanjangan { get; set; }
        public string? BaruPerpanjanganOJKCode { get; set; }
        public string? BaruPerpanjanganDesc { get; set; }
        public DateTime? TanggalAwalKredit { get; set; }
        public DateTime? TanggalMulai { get; set; }
        public DateTime? TanggalJatuhTempo { get; set; }
        public string? KodeKategoriDebitur { get; set; }
        public string? KodeKategoriDebiturOJKCode { get; set; }
        public string? KodeKategoriDebiturDesc { get; set; }
        public string? KodeJenisPenggunaan { get; set; }
        public string? KodeJenisPenggunaanOJKCode { get; set; }
        public string? KodeJenisPenggunaanDesc { get; set; }
        public string? KodeOrientasiPenggunaan { get; set; }
        public string? KodeOrientasiPenggunaanOJKCode { get; set; }
        public string? KodeOrientasiPenggunaanDesc { get; set; }
        public string? KodeSektorEkonomi { get; set; }
        public string? KodeSektorEkonomiOJKCode { get; set; }
        public string? KodeSektorEkonomiDesc { get; set; }
        public string? KodeDati2 { get; set; }
        public string? KodeDati2OJKCode { get; set; }
        public string? KodeDati2Desc { get; set; }
        public int? NilaiProyek { get; set; }
        public string? KodeValuta { get; set; }
        public string? KodeValutaOJKCode { get; set; }
        public string? KodeValutaDesc { get; set; }
        public int? PresentaseSukuBunga { get; set; }
        public string? JenisSukuBunga { get; set; }
        public string? JenisSukuBungaOJKCode { get; set; }
        public string? JenisSukuBungaDesc { get; set; }
        public string? KreditProgramPemerintah { get; set; }
        public string? KreditProgramPemerintahOJKCode { get; set; }
        public string? KreditProgramPemerintahDesc { get; set; }
        public string? TakeoverDari { get; set; }
        public string? TakeoverDariOJKCode { get; set; }
        public string? TakeoverDariDesc { get; set; }
        public string? SumberDana { get; set; }
        public string? SumberDanaOJKCode { get; set; }
        public string? SumberDanaDesc { get; set; }
        public int? PlafonAwal { get; set; }
        public int? Plafon { get; set; }
        public int? RealisasiPencairan { get; set; }
        public int? Denda { get; set; }
        public int? BakiDebet { get; set; }
        public int? NilaiDalamMataUangAsal { get; set; }
        public string? KodeKolektibilitas { get; set; }
        public string? KodeKolektibilitasOJKCode { get; set; }
        public string? KodeKolektibilitasDesc { get; set; }
        public DateTime? TanggalMacet { get; set; }
        public string? KodeSebabMacet { get; set; }
        public string? KodeSebabMacetOJKCode { get; set; }
        public string? KodeSebabMacetDesc { get; set; }
        public int? TunggakanPokok { get; set; }
        public int? TunggakanBunga { get; set; }
        public int? JumlahHariTunggakan { get; set; }
        public int? FrekuensiTunggakan { get; set; }
        public int? FrekuensiRestrukturisasi { get; set; }
        public DateTime? TanggalRestrukturisasiAwal { get; set; }
        public DateTime? TanggalRestrukturisasiAkhir { get; set; }
        public string? KodeCaraRestrukturisasi { get; set; }
        public string? KodeCaraRestrukturisasiOJKCode { get; set; }
        public string? KodeCaraRestrukturisasiDesc { get; set; }
        public string? KodeKondisi { get; set; }
        public string? KodeKondisiOJKCode { get; set; }
        public string? KodeKondisiDesc { get; set; }
        public DateTime? TanggalKondisi { get; set; }
        public string? Keterangan { get; set; }
        public string? KodeKantorCabang { get; set; }
        public string? OperasiData { get; set; }
        public string? OperasiDataOJKCode { get; set; }
        public string? OperasiDataDesc { get; set; }
        public string? SlikGroup { get; set; }
        public string? Period { get; set; }
        public int? IsSyariah { get; set; }

        #region SysCompany    
        public string? SysCompanyKantorCabang { get; set; }
        #endregion

        #region SysGeneralSubCode
        public string? SysGeneralSubCodeSlikGroup { get; set; }
        #endregion

    }
}