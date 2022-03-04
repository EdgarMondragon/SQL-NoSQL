using NoSqlAccess.Cqrs.Commands;

namespace NoSqlAccess.Domain.Commands.Command
{
	public class DeleteClientCommand : ICommand
	{
		public string Id { get; set; }
	}
}
