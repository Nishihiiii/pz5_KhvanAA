using pz5_KhvanAA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityServiceLib;

namespace pz5_KhvanAA
{
	internal class Program
	{
		static void Main(string[] args)
		{
			RegisterNewSystemAccount();
		}

		private static void RegisterNewSystemAccount()
		{
			Console.WriteLine("Создание новой учетной записи для пользователя\n");

			Console.Write("Введите имя пользователя: ");
			string userGivenName = Console.ReadLine();

			Console.Write("Введите фамилию пользователя: ");
			string userFamilyName = Console.ReadLine();

			Console.Write("Введите логин пользователя: ");
			string userAccountLogin = Console.ReadLine();

			Console.Write("Введите пароль пользователя: ");
			string userSecretInput = Console.ReadLine();

			string encryptedPasswordValue = EncryptionManager.GenerateSha256SecurityKey(userSecretInput);
			Console.WriteLine($"Хешированный пароль пользователя:{encryptedPasswordValue}");

			using (TelecomServicesDBEntities databaseInstance = new TelecomServicesDBEntities())
			{
				Users registeredClientAccount = new Users
				{

					Login = userAccountLogin,
					PasswordHash = encryptedPasswordValue,
					RoleID = 1,
					RegistrationDate = DateTime.Now, 
					IsActive = true
				};

				databaseInstance.Users.Add(registeredClientAccount);
				databaseInstance.SaveChanges();
			}

			Console.WriteLine("Учетная запись добавлена");
			Console.ReadLine();
		}
	}
}

