using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;

namespace THOEP.DAL.Interfaces
{
    public interface IDiseaseRepository
    {
        List<Disease> GetDiseases(int healthInfoId);
        void AddDisease(Disease disease);
        void EditDisease(Disease disease);
        Disease DeleteDisease(int diseaseId);
        Disease GetDiseaseById(int diseaseId);
    }
}
