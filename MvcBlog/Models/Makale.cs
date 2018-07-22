namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Makale")]
    public partial class Makale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Makale()
        {
            Yorums = new HashSet<Yorum>();
            Yorums1 = new HashSet<Yorum>();
            Etikets = new HashSet<Etiket>();
        }

        public int MakaleId { get; set; }

        [Display(Name = "Baþlýk")]
        [StringLength(500)]
        [Required(ErrorMessage = "Email boþ býrakýlamaz")]
        public string Baslik { get; set; }

        [Display(Name = "Ýçerik")]
        [UIHint("tinymce_jquery_full"),AllowHtml]
        public string Icerik { get; set; }

        [Display(Name = "Fotoðraf")]
        [StringLength(3999)]
        [Required(ErrorMessage = "Email boþ býrakýlamaz")]
        public string Foto { get; set; }

        [Display(Name = "Tarih")]
        [Required(ErrorMessage = "Email boþ býrakýlamaz")]
        public DateTime? Tarih { get; set; }


        public int? KategoriId { get; set; }

        public int? UyeId { get; set; }

        public int? Okunma { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Uye Uye { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorums1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiket> Etikets { get; set; }
    }
}
