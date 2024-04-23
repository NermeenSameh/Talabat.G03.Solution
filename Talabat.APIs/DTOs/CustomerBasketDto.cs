using Route.Talabat.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; } = null!;

		public List<BasketItemDto> Items { get; set; }
	}
}
