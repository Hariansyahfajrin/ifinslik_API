
namespace Domain.Models
{
    public class K01 : BaseModel
    {
        public string? FormTransactionID { get; set; }
        public string? FlagDetail { get; set; }
        public string? Cif { get; set; }
        public string? NamaLengkap { get; set; }
        public string? TanggalLaporan { get; set; }
        public int? Aset { get; set; }
        public int? AsetLancar { get; set; }
        public int? Kas { get; set; }
        public int? PiutangUsahaLancar { get; set; }
        public int? InvestasiLainLancar { get; set; }
        public int? AsetLancarLain { get; set; }
        public int? AsetTidakLancar { get; set; }
        public int? PiutangUsahaTidakLancar { get; set; }
        public int? InvestasiLainTidakLancar { get; set; }
        public int? AsetTidakLancarLain { get; set; }
        public int? Liabilitas { get; set; }
        public int? LiabilitasJangkaPendek { get; set; }
        public int? PinjamanJangkaPendek { get; set; }
        public int? UtangUsahaJangkaPendek { get; set; }
        public int? LiabilitasJangkaPendekLain { get; set; }
        public int? LiabilitasJangkaPanjang { get; set; }
        public int? PinjamanJangkaPanjang { get; set; }
        public int? UtangUsahajangkaPanjang { get; set; }
        public int? LiabilitasJangkaPanjangLain { get; set; }
        public int? Ekuitas { get; set; }
        public int? PendapatanUsaha { get; set; }
        public int? BebanOperasional { get; set; }
        public int? LabaRugiBruto { get; set; }
        public int? PendapatanLain { get; set; }
        public int? BebanLain { get; set; }
        public int? LabaRugiPreTax { get; set; }
        public int? LabaRugiTahunBerjalan { get; set; }
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