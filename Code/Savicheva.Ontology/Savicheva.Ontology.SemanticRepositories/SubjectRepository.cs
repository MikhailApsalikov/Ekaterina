namespace Savicheva.Ontology.SemanticRepositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using Entities;
	using Selp.Common.Entities;
	using Selp.Interfaces;

	public class SubjectRepository : ISelpRepository<Subject, int>
	{
		private readonly List<Subject> subjects = new List<Subject>
		{
			new Subject
			{
				Id = 1,
				Title = "Дисциплина 1",
				FormsOfControl = new List<FormOfControl>
				{
					new FormOfControl
					{
						Id = 25,
						Title = "Зачет"
					},
					new FormOfControl
					{
						Id = 228,
						Title = "Экзамен"
					}
				},
				HasHourForInd = 10,
				HasHourForKoll = 20,
				HasHourForLab = 30,
				HasHourForLecture = 40,
				HasHourForPract = 50
			},
			new Subject
			{
				Id = 2,
				Title = "Дисциплина 2",
				FormsOfControl = new List<FormOfControl>
				{
					new FormOfControl
					{
						Id = 25,
						Title = "Зачет"
					}
				},
				HasHourForInd = 110,
				HasHourForLecture = 410,
				HasHourForPract = 510
			},
			new Subject
			{
				Id = 3,
				Title = "Дисциплина 31",
				FormsOfControl = new List<FormOfControl>
				{
					new FormOfControl
					{
						Id = 228,
						Title = "Экзамен"
					}
				},
				HasHourForKoll = 21,
				HasHourForLab = 31
			},
			new Subject
			{
				Id = 4,
				Title = "Дисциплина 4",
				FormsOfControl = new List<FormOfControl>(),
				HasHourForInd = 22,
				HasHourForKoll = 33,
				HasHourForLab = 11,
				HasHourForLecture = 22,
				HasHourForPract = 33
			}
		};

		public List<Subject> GetAll()
		{
			return subjects;
		}

		public Subject GetById(int id)
		{
			return subjects.FirstOrDefault(i => i.Id == id);
		}

		public void Remove(int id)
		{
			subjects.RemoveAt(id);
		}

		public List<Subject> GetByCustomExpression(Expression<Func<Subject, bool>> customExpression)
		{
			throw new NotSupportedException();
		}

		public RepositoryModifyResult<Subject> Create(Subject entity)
		{
			subjects.Add(entity);
			return new RepositoryModifyResult<Subject>(entity);
		}

		public List<Subject> GetByFilter(BaseFilter filter, out int total)
		{
			var subjectFilter = filter as SubjectFilter;
			total = subjects.Count;
			return subjects;
		}

		public RepositoryModifyResult<Subject> Update(int id, Subject entity)
		{
			int index = subjects.FindIndex(s => s.Id == id);
			subjects[index] = entity;
			return new RepositoryModifyResult<Subject>(entity);
		}
	}
}