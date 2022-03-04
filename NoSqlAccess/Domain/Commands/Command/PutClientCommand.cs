﻿using NoSqlAccess.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Domain.Commands.Command
{
	public class PutClientCommand : ICommand
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string State { get; set; }
	}
}
