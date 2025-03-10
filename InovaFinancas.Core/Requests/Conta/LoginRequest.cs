﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovaFinancas.Core.Requests.Conta
{
	public class LoginRequest:Request
	{
		[Required(ErrorMessage = "E-mail")]
		[EmailAddress(ErrorMessage = "E-mail inválido")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Senha Inválida")]
		public string Password { get; set; }
	}
}
