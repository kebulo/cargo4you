using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargo4You.Models;

[Table("quotations")]
public partial class Quotation
{
	[Key]
	[Column("quotation_id")]
	[Required]
	public int QuotationId { get; set; }
	[Column("courier_id")]
	public int? CourierId { get; set; }
	[Column("client_name")]
	public string? ClientName { get; set; } = null!;
	[Column("weight")]
	public int? Weight { get; set; }
	[Column("width")]
	public int? Width { get; set; }
	[Column("height")]
	public int? Height { get; set; }
	[Column("depth")]
	public int? Depth { get; set; }
	[Column("price")]
	public double? Price { get; set; }
	[Column("shipment_confirm")]
	public bool ShipmentConfirm { get; set; }
	[Column("address")]
	public string? Address { get; set; }
	[Column("phone_number")]
	public string? PhoneNumber { get; set; }
	
	[ForeignKey("CourierId")]
	public virtual Courier? Courier { get; set; }
}
