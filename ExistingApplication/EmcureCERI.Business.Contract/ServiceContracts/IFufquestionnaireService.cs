namespace EmcureCERI.Business.Contract
{
    using EmcureCERI.Data.DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IFufquestionnaireService
    {
        Fufquestionnaire1 FindFufquestionnaires1Result(int? Id);

        Fufquestionnaire2 FindFufquestionnaires2Result(int? Id);

        Fufquestionnaire3 FindFufquestionnaires3Result(int? Id);

        Fufquestionnaire4 FindFufquestionnaires4Result(int? Id);

        Fufquestionnaire5 FindFufquestionnaires5Result(int? Id);

        Fufquestionnaire6 FindFufquestionnaires6Result(int? Id);

        Fufquestionnaire7 FindFufquestionnaires7Result(int? Id);

        void AddFufquestionnaire1(Fufquestionnaire1 entity);
        void AddFufquestionnaire2(Fufquestionnaire2 entity);
        void AddFufquestionnaire3(Fufquestionnaire3 entity);
        void AddFufquestionnaire4(Fufquestionnaire4 entity);
        void AddFufquestionnaire5(Fufquestionnaire5 entity);
        void AddFufquestionnaire6(Fufquestionnaire6 entity);
        void AddFufquestionnaire7(Fufquestionnaire7 entity);

        void UpdateFufquestionnaire1(Fufquestionnaire1 entity);
        void UpdateFufquestionnaire2(Fufquestionnaire2 entity);
        void UpdateFufquestionnaire3(Fufquestionnaire3 entity);
        void UpdateFufquestionnaire4(Fufquestionnaire4 entity);
        void UpdateFufquestionnaire5(Fufquestionnaire5 entity);
        void UpdateFufquestionnaire6(Fufquestionnaire6 entity);
        void UpdateFufquestionnaire7(Fufquestionnaire7 entity);

    }
}

