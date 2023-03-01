namespace EmcureCERI.Business.Core
{
    using EmcureCERI.Business.Contract;
    using EmcureCERI.Data.DataAccess.Entities;
    using EmcureCERI.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FufquestionnaireService : IFufquestionnaireService
    {
        private readonly IEntityBaseRepository<Fufquestionnaire1> _fufquestionnaire1;
        private readonly IEntityBaseRepository<Fufquestionnaire2> _fufquestionnaire2;
        private readonly IEntityBaseRepository<Fufquestionnaire3> _fufquestionnaire3;
        private readonly IEntityBaseRepository<Fufquestionnaire4> _fufquestionnaire4;
        private readonly IEntityBaseRepository<Fufquestionnaire5> _fufquestionnaire5;
        private readonly IEntityBaseRepository<Fufquestionnaire6> _fufquestionnaire6;
        private readonly IEntityBaseRepository<Fufquestionnaire7> _fufquestionnaire7;

        #region Default Construtor

        public FufquestionnaireService(
            IEntityBaseRepository<Fufquestionnaire1> fufquestionnaire1,
            IEntityBaseRepository<Fufquestionnaire2> fufquestionnaire2,
            IEntityBaseRepository<Fufquestionnaire3> fufquestionnaire3,
            IEntityBaseRepository<Fufquestionnaire4> fufquestionnaire4,
            IEntityBaseRepository<Fufquestionnaire5> fufquestionnaire5,
            IEntityBaseRepository<Fufquestionnaire6> fufquestionnaire6,
            IEntityBaseRepository<Fufquestionnaire7> fufquestionnaire7
            )
        {
            _fufquestionnaire1 = fufquestionnaire1;
            _fufquestionnaire2 = fufquestionnaire2;
            _fufquestionnaire3 = fufquestionnaire3;
            _fufquestionnaire4 = fufquestionnaire4;
            _fufquestionnaire5 = fufquestionnaire5;
            _fufquestionnaire6 = fufquestionnaire6;
            _fufquestionnaire7 = fufquestionnaire7;
        }
        #endregion

        public Fufquestionnaire1 FindFufquestionnaires1Result(int? Id)
        {
            if (Id != null)
            {
                return _fufquestionnaire1.GetSingle(o => o.Id == Id); 
            }
            else {
                return new Fufquestionnaire1();
            }
        }

        public Fufquestionnaire2 FindFufquestionnaires2Result(int? Id)
        {
            if (Id != null)
            {
                return _fufquestionnaire2.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Fufquestionnaire2();
            }
        }

        public Fufquestionnaire3 FindFufquestionnaires3Result(int? Id)
        {
            if (Id != null)
            {
                return _fufquestionnaire3.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Fufquestionnaire3();
            }
        }

        public Fufquestionnaire4 FindFufquestionnaires4Result(int? Id)
        {
            if (Id != null)
            {
                return _fufquestionnaire4.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Fufquestionnaire4();
            }
        }

        public Fufquestionnaire5 FindFufquestionnaires5Result(int? Id)
        {
            if (Id != null)
            {
                return _fufquestionnaire5.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Fufquestionnaire5();
            }
        }

        public Fufquestionnaire6 FindFufquestionnaires6Result(int? Id)
        {
            if (Id != null)
            {
                return _fufquestionnaire6.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Fufquestionnaire6();
            }
        }

        public Fufquestionnaire7 FindFufquestionnaires7Result(int? Id)
        {
            if (Id != null)
            {
                return _fufquestionnaire7.GetSingle(o => o.Id == Id);
            }
            else
            {
                return new Fufquestionnaire7();
            }
        }

        public void AddFufquestionnaire1(Fufquestionnaire1 entity)
        {
            _fufquestionnaire1.Add(entity);
            _fufquestionnaire1.Commit();
        }
        public void AddFufquestionnaire2(Fufquestionnaire2 entity)
        {
            _fufquestionnaire2.Add(entity);
            _fufquestionnaire2.Commit();
        }
        public void AddFufquestionnaire3(Fufquestionnaire3 entity)
        {
            _fufquestionnaire3.Add(entity);
            _fufquestionnaire3.Commit();
        }
        public void AddFufquestionnaire4(Fufquestionnaire4 entity)
        {
            _fufquestionnaire4.Add(entity);
            _fufquestionnaire4.Commit();
        }
        public void AddFufquestionnaire5(Fufquestionnaire5 entity)
        {
            _fufquestionnaire5.Add(entity);
            _fufquestionnaire5.Commit();
        }
        public void AddFufquestionnaire6(Fufquestionnaire6 entity)
        {
            _fufquestionnaire6.Add(entity);
            _fufquestionnaire6.Commit();
        }
        public void AddFufquestionnaire7(Fufquestionnaire7 entity)
        {
            _fufquestionnaire7.Add(entity);
            _fufquestionnaire7.Commit();
        }


        public void UpdateFufquestionnaire1(Fufquestionnaire1 entity)
        {
            entity.IsFulFill = true;
            _fufquestionnaire1.Edit(entity);
            _fufquestionnaire1.Commit();
        }

        public void UpdateFufquestionnaire2(Fufquestionnaire2 entity)
        {   
            entity.IsFulFill = true;
            _fufquestionnaire2.Edit(entity);
            _fufquestionnaire2.Commit();
        }
        public void UpdateFufquestionnaire3(Fufquestionnaire3 entity)
        {
            entity.IsFulFill = true;
            _fufquestionnaire3.Edit(entity);
            _fufquestionnaire3.Commit();
        }
        public void UpdateFufquestionnaire4(Fufquestionnaire4 entity)
        {
            entity.IsFulFill = true;
            _fufquestionnaire4.Edit(entity);
            _fufquestionnaire4.Commit();
        }
        public void UpdateFufquestionnaire5(Fufquestionnaire5 entity)
        {
            entity.IsFulFill = true;
            _fufquestionnaire5.Edit(entity);
            _fufquestionnaire5.Commit();
        }

        public void UpdateFufquestionnaire6(Fufquestionnaire6 entity)
        {
            entity.IsFulFill = true;
            _fufquestionnaire6.Edit(entity);
            _fufquestionnaire6.Commit();
        }
        public void UpdateFufquestionnaire7(Fufquestionnaire7 entity)
        {
            entity.IsFulFill = true;
            _fufquestionnaire7.Edit(entity);
            _fufquestionnaire7.Commit();
        }
    }
}
