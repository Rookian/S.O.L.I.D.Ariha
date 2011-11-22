using System;

//using NHibernate;

namespace CommandProcessor.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		void Invalidate();
	}
}