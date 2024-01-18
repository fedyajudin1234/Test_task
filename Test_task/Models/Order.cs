using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_task.Models
{
	public class Order
	{
		public int ID { get; set; }
		public DateTime Date { get; set; }
		[Precision(18, 2)]
		public decimal Amount { get; set; }
		public virtual Customer Customer { get; set; }
		public int CustomerID { get; set; }
	}
}
