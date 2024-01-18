using Test_task.ViewModels;

namespace Test_task
{
	internal class Program
	{
		static void Main(string[] args)
		{
            Console.Write("Enter begin date in format(yyyy/mm/dd): ");
			string stringDate = Console.ReadLine();
			bool isCorrectDate = DateTime.TryParse(stringDate, out DateTime result);
			if(isCorrectDate && stringDate.Contains("/"))
			{
				DateTime beginDate = DateTime.Parse(stringDate);
				Console.Write("Enter the sum: ");
				string sumAmount = Console.ReadLine();
				bool isCorrectSumAmount = Decimal.TryParse(sumAmount, out decimal sum);
				if (isCorrectSumAmount)
				{
					decimal sumForFunction = Decimal.Parse(sumAmount);
					List<CustomerViewModel> customerViewModels = GetCustomer(beginDate, sumForFunction);
					foreach (var item in customerViewModels)
					{
                        Console.WriteLine("Manager is: " + item.ManagerName + " customer is: " + item.CustomerName + " amount is: " + item.Amount);
                    }
				}
			}
		}

		public static List<CustomerViewModel> GetCustomer(DateTime beginDate,
			decimal sumAmount)
		{
			DateTime currentDate = DateTime.Now;
			List<CustomerViewModel> customers = new List<CustomerViewModel>();

			using (ApplicationContext context = new ApplicationContext())

			{

				customers = context.Orders

					.Join(

					context.Customers, order => order.ID, customer => customer.ID,

					(order, customer) => new { Orders = order, Customers = customer })

					.Join(context.Managers, customerTM => customerTM.Customers.ManagerID, manager => manager.ID,

					(customerTM, manager) => new { Customers = customerTM, Managers = manager })

					.Where(order => order.Customers.Orders.Date >= beginDate && order.Customers.Orders.Date <= currentDate && order.Customers.Orders.Amount >= sumAmount)

					.Select(value => new CustomerViewModel

					{

						CustomerName = value.Customers.Customers.Name,

						ManagerName = value.Managers.Name,

						Amount = value.Customers.Orders.Amount

					}).ToList();

			}
			return customers;
		}
	}
}