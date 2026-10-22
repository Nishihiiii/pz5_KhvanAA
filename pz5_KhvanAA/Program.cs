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

			bool isAppRunning = true;


			while (isAppRunning)
			{
				Console.Clear();
				Console.WriteLine("1. Зарегистрировать нового пользователя");
				Console.WriteLine("0. Выход");
				Console.Write("\nВыберите пункт меню: ");

				string menuChoice = Console.ReadLine();

				switch (menuChoice)
				{
					case "1":
						Console.Clear();

						RegisterNewSystemAccount();
						break;


					case "0":
						isAppRunning = false;
						Console.WriteLine("Завершение работы программы...");
						break;

					default:
						Console.WriteLine("\nНеверный ввод! Пожалуйста, выберите 1, 2 или 0.");
						Console.WriteLine("Нажмите Enter, чтобы попробовать снова...");
						Console.ReadLine();
						break;
				}
			}
		}

		private static void RegisterNewSystemAccount()
		{
			Console.WriteLine("Новая учетная записьn");

			Console.Write("Введите имя пользователя: ");
			string userGivenName = Console.ReadLine();

			Console.Write("Введите фамилию пользователя: ");
			string userFamilyName = Console.ReadLine();

			Console.Write("Введите номер телефона: ");
			string userPhoneInput = Console.ReadLine();

			Console.Write("Введите Email пользователя: ");
			string userEmailInput = Console.ReadLine();

			Console.Write("Введите паспортные данные (серия и номер): ");
			string userPassportInput = Console.ReadLine();

			Console.Write("Введите адрес проживания: ");
			string userAddressInput = Console.ReadLine();

			Console.Write("Введите логин пользователя: ");
			string userAccountLogin = Console.ReadLine();

			Console.Write("Введите пароль пользователя: ");
			string userSecretInput = Console.ReadLine();

			Console.Write("Введите ID роли (например: 1 - Клиент): ");
			string roleInput = Console.ReadLine();

			int selectedRoleId;
			if (!int.TryParse(roleInput, out selectedRoleId))
			{
				Console.WriteLine("Ошибка ввода роли. Назначена роль по умолчанию: 1");
				selectedRoleId = 1;
			}

			string encryptedPasswordValue = EncryptionManager.GenerateSha256SecurityKey(userSecretInput);

			try
			{
				using (TelecomServicesDBEntities databaseInstance = new TelecomServicesDBEntities())
				{
					Users registeredAccount = new Users
					{
						Login = userAccountLogin,
						PasswordHash = encryptedPasswordValue,
						RoleID = selectedRoleId,
						RegistrationDate = DateTime.Now,
						IsActive = true
					};

					Clients newClientProfile = new Clients
					{
						FirstName = userGivenName,
						LastName = userFamilyName,
						PhoneNumber = userPhoneInput,
						Email = userEmailInput,
						PassportData = userPassportInput,
						Address = userAddressInput,
						Users = registeredAccount
					};

					databaseInstance.Clients.Add(newClientProfile);
					databaseInstance.SaveChanges();
				}

				Console.WriteLine("\nУчетная запись была успешно добавлена в базу данных!");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"\nПроизошла ошибка при сохранении: {ex.Message}");
				if (ex.InnerException != null)
				{
					Console.WriteLine($"Детали: {ex.InnerException.InnerException?.Message ?? ex.InnerException.Message}");
				}
			}

			Console.WriteLine("\nНажмите Enter для возврата в главное меню...");
			Console.ReadLine();
		}
	}
}


