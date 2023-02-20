using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargo4You.Models;

[Table("courier")]
public partial class Courier
{
	[System.ComponentModel.DataAnnotations.Key]
	[Column("courier_id")]
	[Required]
	public int CourierId { get; set; }
    [Column("Name")]
    public string Name { get; set; } = null!;
	public virtual ICollection<Quotation> Quotations { get; } = new List<Quotation>();

    public virtual ICollection<Rule> Rules { get; } = new List<Rule>();
}
