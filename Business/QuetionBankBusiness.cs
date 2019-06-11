using QuestionBankManagement;
using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Business
{
    public class QuetionBankBusiness
    {
        private MyDbContext _myDbContext;
        public QuetionBankBusiness()
        {
            _myDbContext = new MyDbContext();
        }
        public List<Subject> GetAllSubject()
        {
            List<Subject> subjects = _myDbContext.Subject.AsNoTracking().ToList();
            return subjects;
        }
        public List<QuestionType> GetAllQuestionType()
        {
            List<QuestionType> list = _myDbContext.QuestionType.AsNoTracking().ToList();
            return list;
        }
        public List<KnowledgePoint> GetAllKnowledgePoint()
        {
            List<KnowledgePoint> list = _myDbContext.KnowledgePoint.AsNoTracking().ToList();
            return list;
        }
        public bool AddExaminationQuestion(ExaminationQuestion examinationQuestion)
        {
            _myDbContext.ExaminationQuestion.Add(examinationQuestion);
            _myDbContext.SaveChanges();
            return true;
        }
        public List<ExaminationQuestion> GetExaminationQuestionList(string search, int questionTypesId, int subjectId,int pageIndex)
        {
            try
            {

                IQueryable<ExaminationQuestion> result = _myDbContext.ExaminationQuestion.Include(c => c.QuestionType).Include(c => c.Subject).Include(c => c.KnowledgePoint);
                if (!string.IsNullOrEmpty(search))
                {
                    result = result.Where(c => c.Content.Contains(search));
                }
                if (subjectId != 0)
                {
                    result = result.Where(c => c.SubjectId == subjectId);
                }
                if (questionTypesId != 0)
                {
                    result = result.Where(c => c.QuestionTypeId == questionTypesId);
                }
                var list = result.OrderBy(c=>c.Id).Skip((pageIndex - 1) * 8).Take(8).AsNoTracking().ToList();
                return list;
            }
            catch(Exception e)
            {
                return new List<ExaminationQuestion>();
            }
        }
        public ExaminationQuestion GetExaminationQuestionById(int id)
        {
            return _myDbContext.ExaminationQuestion.FirstOrDefault(c => c.Id == id);
        }

        public bool DeleteExaminationQuestion(int id)
        {
            ExaminationQuestion dbExaminationQuestion = _myDbContext.ExaminationQuestion.FirstOrDefault(c => c.Id == id);
            if (dbExaminationQuestion == null) return false;
            _myDbContext.ExaminationQuestion.Remove(dbExaminationQuestion);
            _myDbContext.SaveChangesAsync();
            return true;
        }
        public bool SaveExaminationQuestion(ExaminationQuestion examinationQuestion)
        {

            ExaminationQuestion dbExaminationQuestion = _myDbContext.ExaminationQuestion.FirstOrDefault(c => c.Id == examinationQuestion.Id);
            if (dbExaminationQuestion == null) return false;
            dbExaminationQuestion.KnowledgePointId = examinationQuestion.Id;
            dbExaminationQuestion.SubjectId = examinationQuestion.SubjectId;
            dbExaminationQuestion.Content = examinationQuestion.Content;
            dbExaminationQuestion.Score = examinationQuestion.Score;
            dbExaminationQuestion.QuestionTypeId = examinationQuestion.QuestionTypeId;
            _myDbContext.SaveChangesAsync();
            return true;
        }
        public bool AddKnowledgePoint(string KnowledgePointName)
        {
            _myDbContext.KnowledgePoint.Add(new KnowledgePoint() { KnowledgePointName = KnowledgePointName });
            _myDbContext.SaveChanges();
            return true;
        }
        public bool DeleteKnowledgePoint(int id)
        {
            KnowledgePoint knowledgePoint = _myDbContext.KnowledgePoint.FirstOrDefault(c => c.Id == id);
            if (knowledgePoint == null)
            {
                return false;
            }
            _myDbContext.KnowledgePoint.Remove(knowledgePoint);
            _myDbContext.SaveChanges();
            return true;
        }
        public bool SaveKnowledgePoint(KnowledgePoint knowledge)
        {
            KnowledgePoint knowledgePoint = _myDbContext.KnowledgePoint.FirstOrDefault(c => c.Id == knowledge.Id);
            if (knowledgePoint == null)
            {
                return false;
            }
            knowledgePoint.KnowledgePointName = knowledge.KnowledgePointName;
            _myDbContext.SaveChanges();
            return true;
        }
        public bool AddSubject(string subjectName)
        {
            _myDbContext.Subject.Add(new Subject() { SubjectName = subjectName });
            _myDbContext.SaveChanges();
            return true;
        }
        public bool DeleteSubject(int id)
        {
            Subject subject = _myDbContext.Subject.FirstOrDefault(c => c.Id == id);
            if (subject == null)
            {
                return false;
            }
            _myDbContext.Subject.Remove(subject);
            _myDbContext.SaveChanges();
            return true;
        }
        public bool SaveSubject(Subject subject)
        {
            Subject dbSubject = _myDbContext.Subject.FirstOrDefault(c => c.Id == subject.Id);
            if (dbSubject == null)
            {
                return false;
            }
            dbSubject.SubjectName = subject.SubjectName;
            _myDbContext.SaveChanges();
            return true;
        }

        public bool AddQuestionType(QuestionType questionType)
        {
            _myDbContext.QuestionType.Add(questionType);
            _myDbContext.SaveChanges();
            return true;
        }
        public bool DeleteQuestionType(int id)
        {
            QuestionType questionType = _myDbContext.QuestionType.FirstOrDefault(c => c.Id == id);
            if (questionType == null)
            {
                return false;
            }
            _myDbContext.QuestionType.Remove(questionType);
            _myDbContext.SaveChanges();
            return true;
        }
        public bool SaveQuestionType(QuestionType questionType)
        {
            QuestionType dbQuestionType = _myDbContext.QuestionType.FirstOrDefault(c => c.Id == questionType.Id);
            if (dbQuestionType == null)
            {
                return false;
            }
            dbQuestionType.QuestionTypeName = questionType.QuestionTypeName;
            _myDbContext.SaveChanges();
            return true;
        }
        public List<ExaminationQuestion> GetExaminationQuestionsByPaperTypeId(int paperTypeId)
        {
            List<ExaminationQuestion> result =(from rqp in   _myDbContext.ExaminationQuestionPaperType.Where(c => c.PaperQuestionTypeId == paperTypeId)
                                              join eq in _myDbContext.ExaminationQuestion.Include(e=>e.KnowledgePoint).Include(c=>c.QuestionType).Include(c=>c.Subject) on rqp.ExaminationQuestionId equals eq.Id
                                              select eq).ToList();

            return result;
        }
        public PaperQuestionType GetPaperQuestionTypeById(int id)
        {
            PaperQuestionType result = _myDbContext.PaperQuestionType.AsNoTracking().FirstOrDefault(c => c.Id == id);
            return result;
        }
        public int GetExaminationQuestionTotalPage(string search, int questionTypesId, int subjectId)
        {
            IQueryable<ExaminationQuestion> result = _myDbContext.ExaminationQuestion.Include(c => c.QuestionType).Include(c => c.Subject).Include(c => c.KnowledgePoint);
            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(c => c.Content.Contains(search));
            }
            if (subjectId != 0)
            {
                result = result.Where(c => c.SubjectId == subjectId);
            }
            if (questionTypesId != 0)
            {
                result = result.Where(c => c.QuestionTypeId == questionTypesId);
            }
            int count = result.Count();
            int pageCount = count % 8 == 0 ? count / 8 : (count / 8) + 1;
            return pageCount;
        }

    }
}
