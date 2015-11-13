namespace HomeWork1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶總覽MetaData))]
    public partial class 客戶總覽
    {
    }
    
    public partial class 客戶總覽MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 客戶銀行人數 { get; set; }
        public Nullable<int> 客戶聯絡人數 { get; set; }
    }
}
