namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Selp.Common.Entities;
	using Selp.Interfaces;

	public interface ISemanticRepository<TEntity, in TKey> where TEntity : ISelpEntity<TKey>
	{
		TEntity GetById(TKey id);

		List<TEntity> GetByFilter(BaseFilter filter, out int total);

		RepositoryModifyResult<TEntity> Create(TEntity entity);

		RepositoryModifyResult<TEntity> Update(TKey id, TEntity entity);

		void Remove(TKey id);
	}
}