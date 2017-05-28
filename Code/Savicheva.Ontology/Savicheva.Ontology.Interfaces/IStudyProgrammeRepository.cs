namespace Savicheva.Ontology.Interfaces
{
    using System.Collections.Generic;
    using Entities;

    public interface IStudyProgrammeRepository
    {
        List<StudyProgramme> GetAll(StudyProgrammeFilter filter);
        StudyProgramme GetById(string id);
        void Remove(string id);
        string Create(StudyProgramme entity);
        void Update(string id, StudyProgramme entity);
    }
}