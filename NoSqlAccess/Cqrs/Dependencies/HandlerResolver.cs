using DotNetStarter.Abstractions;
using NoSqlAccess.Cqrs.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Cqrs.Dependencies
{
	public class HandlerResolver : IHandlerResolver
	{
		private readonly IResolver resolver;

		public HandlerResolver(IResolver resolver)
		{
			this.resolver = resolver;
		}

		public THandler ResolveHandler<THandler>()
		{
			var handler = resolver.Resolve<THandler>();

			if (handler == null)
			{
				throw new HandlerNotFoundException(typeof(THandler));
			}

			return handler;
		}
	}
}
