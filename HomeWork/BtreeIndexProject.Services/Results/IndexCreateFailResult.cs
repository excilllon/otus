using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BtreeIndexProject.Abstractions;

namespace BtreeIndexProject.Services.Results
{
	public class IndexCreateFailResult: QueryResult
	{
		public IndexCreateFailResult(string message)
		{
			ErrorMessage = message;
		}
	}
}
