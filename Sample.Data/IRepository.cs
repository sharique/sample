using System;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Sample.Data
{
	public interface IRepository<T>
	{
		void Add (T item);
		
		void Delete (T item);
		
		void Update (T item);
		
		void SaveOrUpdate (T item);
		
		T Get<U> (U id);
		
		/// <summary>
		/// Defers the detached crteria execution which will return a result set that is an  <see cref="IEnumerable{T}"/>.
		/// </summary>
		/// <param name="detachedCriteria">The detached criteria.</param>
		/// <returns></returns>
		IEnumerable<T> FutureList (DetachedCriteria detachedCriteria);

		/// <summary>
		/// Executes the detached crteria immediately returning a single item <see cref="T"/>
		/// This will also cause the execution of any defered queries to be executed.
		/// </summary>
		/// <param name="detachedCriteria">The detached criteria.</param>
		/// <returns></returns>
		T FutureSingle (DetachedCriteria detachedCriteria);
		
		/// <summary>
		/// Executes the detached crteria immediately returning a result set that is an  <see cref="IList{T}"/>.
		/// This will also cause the execution of any defered queries to be executed.
		/// </summary>
		/// <param name="detachedCriteria">The detached criteria.</param>
		/// <returns></returns>
		IList<T> QueryList (DetachedCriteria detachedCriteria);

		/// <summary>
		/// Executes the detached crteria immediately returning a single item <see cref="T"/>
		/// </summary>
		/// <param name="detachedCriteria">The detached criteria.</param>
		/// <returns></returns>
		T QuerySingle (DetachedCriteria detachedCriteria);
	}
}

