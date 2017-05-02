﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table(name: "Oda")]
    class Oda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int OdaId { get; set; }

    }
}
