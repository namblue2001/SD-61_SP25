using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class AnhMinhChung
    {
        [Key]
        public Guid ID_AnhMinhChung { get; set; }
        public Guid ID_YeuCauDoiTra { get; set; }
        public string url { get; set; }
        public virtual YeuCauDoiTra YeuCauDoiTra { get;set; }
    }
}
