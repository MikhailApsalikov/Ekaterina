﻿namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;

	// Выпускающая кафедра
	public class Department : ISelpEntity<string>
	{
		public string Title { get; set; }
		public string Id { get; set; }
	}
}