using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOEP.DAL.DbContext;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;

namespace THOEP.DAL.Repositories
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly Context _context;
        public DiseaseRepository(Context context)
        {
            _context = context;
        }
        public List<Disease> GetDiseases(int healthInfoId)
        {
            return _context.Diseases.Where(x => x.Id == healthInfoId).ToList();
        }
        public Disease GetDiseaseById(int diseaseId)
        {
            var disease = _context.Diseases.Find(diseaseId);
            return disease;
        }

        public void AddDisease(Disease disease)
        {
            if (disease.Id == 0)
            {
                _context.Diseases.Add(disease);
            }
            else
            {
                var dbEntry = _context.Diseases.Find(disease.Id);
                if (dbEntry != null)
                {
                    dbEntry.DiseaseCode = disease.DiseaseCode;
                    dbEntry.DiseaseName = disease.DiseaseName;
                    dbEntry.DangerLevel = disease.DangerLevel;
                    dbEntry.Reccomendation = disease.Reccomendation;
                }
            }
            _context.SaveChanges();
        }

        public void EditDisease(Disease disease)
        {
            _context.Entry(disease).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public Disease DeleteDisease(int diseaseId)
        {
            var dbEntry = _context.Diseases.Find(diseaseId);
            if (dbEntry != null)
            {
                _context.Diseases.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
