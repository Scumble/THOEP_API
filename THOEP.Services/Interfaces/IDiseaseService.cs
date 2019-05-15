using System;
using System.Collections.Generic;
using System.Text;
using THOEP.Services.DTO;

namespace THOEP.Services.Interfaces
{
    public interface IDiseaseService
    {
        List<DiseaseDto> GetDiseases();
        void AddDisease(DiseaseDto disease);
        void EditDisease(DiseaseDto disease);
        DiseaseDto DeleteDisease(int diseaseId);
        DiseaseDto GetDiseaseById(int diseaseId);
    }
}
