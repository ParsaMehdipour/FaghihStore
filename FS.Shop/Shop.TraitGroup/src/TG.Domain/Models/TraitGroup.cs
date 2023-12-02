using SH.Domain;
using SH.Domain.Interfaces;

namespace TG.Domain.Models;

public class TraitGroup : AuditableEntity, IAggregateRoot
{
	private TraitGroup()
	{
	}

	public string Title { get; private set; }
	public int OrderNumber { get; private set; }

	public ICollection<Trait> Traits { get; private set; }

	internal TraitGroup(string title, int orderNumber)
	{
		SetTitle(title);
		SetOrderNumber(orderNumber);
		CreatedDate = DateTime.Now;
	}

	public void SetTitle(string title)
	{
		if (Title == title)
			return;

		Title = title;
	}

	public void SetOrderNumber(int orderNumber)
	{
		if (OrderNumber == orderNumber)
			return;
		OrderNumber = orderNumber;
	}

	public void SetModifiedDate(DateTime modifiedDate)
	{
		if (ModifiedDate != modifiedDate)
			ModifiedDate = modifiedDate;
	}
}