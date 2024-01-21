using BtreeIndexProject.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtreeIndexProject.Services.Results
{
	internal class ColumnNotFoundResult : QueryResult
	{
		public ColumnNotFoundResult()
		{
			ErrorMessage = "Колонка не найдена";
		}
	}
}
