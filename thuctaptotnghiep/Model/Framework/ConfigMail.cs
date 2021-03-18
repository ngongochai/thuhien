namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConfigMail")]
    public partial class ConfigMail
    {
        public int id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string smtUserName { get; set; }

        [StringLength(200)]
        public string smtpPassword { get; set; }

        [StringLength(200)]
        public string smtpHost { get; set; }

        public int? smtpPort { get; set; }

        public string smtpSubject { get; set; }

        public int? smtpType { get; set; }

        public bool? EnableSSL { get; set; }
    }
}
