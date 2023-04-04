﻿using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AcademicManagementSystem.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(TblUser user)
		{
			DbAcademicMsContext db = new DbAcademicMsContext();
			var getUser = db.TblUsers.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
			
			if(getUser!=null) //kullanıcı bulundu
			{
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Name,user.Name.ToString()) 
				};
				ClaimsIdentity userIdentity = new ClaimsIdentity(claims); //kullanıcı kimliği oluşturuldu
				ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
				await HttpContext.SignInAsync(principal);

                if (user.Authority == "Teacher")
                {
                    return RedirectToAction("Teacher", "Home");
                }
                else if (user.Authority == "Student")
                {
                    return RedirectToAction("Student", "Home");
                }
            }
            return View();
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
		public static string MD5Sifrele(string sifrelenecekMetin) //kullanıcı parolarları şifrelendiğinde kullanılacak
		{
			//MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			//Parametre olarak gelen veriyi byte dizisine dönüştürdük.
			byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
			//dizinin hash'ini hesaplattık.
			dizi = md5.ComputeHash(dizi);
			//Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
			StringBuilder sb = new StringBuilder();
			//Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

			foreach (byte ba in dizi)
			{
				sb.Append(ba.ToString("x2").ToLower());
			}

			//hexadecimal(onaltılık) stringi geri döndürdük.
			return sb.ToString();
		}
	}
}