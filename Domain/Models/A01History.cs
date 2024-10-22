
namespace Domain.Models
{
    public class A01History : BaseModel
    {
        public string? FormTransactionHistoryID { get; set; }
        public string? FlagDetail { get; set; }
        public string? KodeAgunan { get; set; }
        public string? NomorRekening { get; set; }
        public string? Cif { get; set; }
        public string? KodeJenisSegmenFasilitas { get; set; }
        public string? KodeJenisSegmenFasilitasOJKCode { get; set; }
        public string? KodeJenisSegmenFasilitasDesc { get; set; }
        public string? KodeStatusAgunan { get; set; }
        public string? KodeStatusAgunanOJKCode { get; set; }
        public string? KodeStatusAgunanDesc { get; set; }
        public string? KodeJenisAgunan { get; set; }
        public string? KodeJenisAgunanOJKCode { get; set; }
        public string? KodeJenisAgunanDesc { get; set; }
        public string? PeringkatAgunan { get; set; }
        public string? KodeLembagaPemeringkat { get; set; }
        public string? KodeLembagaPemeringkatOJKCode { get; set; }
        public string? KodeLembagaPemeringkatDesc { get; set; }
        public string? KodeJenisPengikatan { get; set; }
        public string? KodeJenisPengikatanOJKCode { get; set; }
        public string? KodeJenisPengikatanDesc { get; set; }
        public DateTime? TanggalPengikatan { get; set; }
        public string? NamaPemilik { get; set; }
        public string? BuktiKepemilikan { get; set; }
        public string? AlamatAgunan { get; set; }
        public string? KodeDati2 { get; set; }
        public string? KodeDati2OJKCode { get; set; }
        public string? KodeDati2Desc { get; set; }
        public int? NilaiAgunanNJOP { get; set; }
        public int? NilaiAgunanLJK { get; set; }
        public DateTime? TanggalPenilaianLJK { get; set; }
        public int? NilaiAgunanIndependen { get; set; }
        public string? NamaPenilaiIndependen { get; set; }
        public DateTime? TanggalPenilaianIndependen { get; set; }
        public string? StatusParipasu { get; set; }
        public string? StatusParipasuOJKCode { get; set; }
        public string? StatusParipasuDesc { get; set; }
        public int? PresentaseParipasu { get; set; }
        public string? StatusKreditJoin { get; set; }
        public string? StatusKreditJoinOJKCode { get; set; }
        public string? StatusKreditJoinDesc { get; set; }
        public string? Diasuransikan { get; set; }
        public string? DiasuransikanOJKCode { get; set; }
        public string? DiasuransikanDesc { get; set; }
        public string? Keterangan { get; set; }
        public string? KodeKantorCabang { get; set; }
        public string? OperasiData { get; set; }
        public string? OperasiDataOJKCode { get; set; }
        public string? OperasiDataDesc { get; set; }
        public string? Period { get; set; }

        #region SysCompany    
        public string? SysCompanyKantorCabang { get; set; }
        #endregion

    }
}