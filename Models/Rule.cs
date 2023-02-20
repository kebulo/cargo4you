using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargo4You.Models;

[Table("rules")]
public partial class Rule
{
	[System.ComponentModel.DataAnnotations.Key]
	[Column("price_id")]
	public int PriceId { get; set; }
	[Column("courier_id")]
	public int? CourierId { get; set; }
	[Column("pice")]
	public double? Price { get; set; }
	[Column("is_dimension")]
	public bool? IsDimension { get; set; }
	[Column("min")]
	public short? Min { get; set; }
	[Column("extra_kg")]
	public short? ExtraKg { get; set; }
	[Column("max")]
	public string? Max { get; set; }
	[Column("extra_value")]
	public double? ExtraValue { get; set; }

	public virtual Courier? Courier { get; set; }
	public string CourierName { get; internal set; }
}
