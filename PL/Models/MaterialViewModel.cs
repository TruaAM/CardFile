using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Models
{
	public class MaterialViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Please enter the name of the product")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be from 3 to 255 characters long")]
		public string Name { get; set; }

		[StringLength(50, MinimumLength = 3, ErrorMessage = "The category must be from 3 to 255 characters long")]
		public string Content { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime DateCreate { get; set; }

	}
}
