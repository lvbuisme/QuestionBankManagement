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
    public class ExaminationPaperBusiness
    {
        private MyDbContext _myDbContext;
        public ExaminationPaperBusiness()
        {
            _myDbContext = new MyDbContext();
        }
        public List<TestPaper> GetAllTestPaperTitle()
        {
            List<TestPaper> testPapers = _myDbContext.TestPaper.Where(c=>c.IsFinish).ToList();
            return testPapers;
        }
        public List<TestPaper> GetAllTestPaper()
        {
            List<TestPaper> testPapers = _myDbContext.TestPaper.ToList();
            return testPapers;
        }
        public int AddTestPaper(TestPaper testPaper)
        {
            try
            {
                if (testPaper.Id != 0)
                {
                    TestPaper dbTestPaper = _myDbContext.TestPaper.FirstOrDefault(c => c.Id == testPaper.Id);
                    if (dbTestPaper != null)
                    {
                        dbTestPaper.SubjectId = testPaper.SubjectId;
                        dbTestPaper.Subtitle = testPaper.Subtitle;
                        dbTestPaper.Title = testPaper.Title;
                        dbTestPaper.IsFinish = true;
                        _myDbContext.SaveChanges();
                        return testPaper.Id;
                    }
                }
                _myDbContext.TestPaper.Add(testPaper);
                _myDbContext.SaveChanges();
                return testPaper.Id;
            }
            catch
            {
                return -1;
            }
          

        }
        public bool SaveTestPaper(TestPaper testPaper)
        {
            TestPaper dbTestPaper = _myDbContext.TestPaper.FirstOrDefault(c => c.Id == testPaper.Id);
            if (dbTestPaper == null)
            {
                return false;
            }
            dbTestPaper.SubjectId = testPaper.SubjectId;
            dbTestPaper.Subtitle = testPaper.Subtitle;
            dbTestPaper.Title = testPaper.Title;
            dbTestPaper.PaperQuestionTypes = testPaper.PaperQuestionTypes;
            _myDbContext.SaveChanges();
            return true;
        }
        public bool DeleteTestPaper(int id)
        {
            TestPaper dbTestPaper = _myDbContext.TestPaper.FirstOrDefault(c => c.Id == id);
            if (dbTestPaper == null)
            {
                return false;
            }
            _myDbContext.TestPaper.Remove(dbTestPaper); 
            _myDbContext.SaveChanges();
            return true;
        }
        public int AddPaperquestionType(PaperQuestionType paperQuestionType)
        {
            _myDbContext.PaperQuestionType.Add(paperQuestionType);
             _myDbContext.SaveChanges();
            return paperQuestionType.Id;
        }
        public bool AddExaminationQuesionPaperTypes(ExaminationQuestionPaperType examinationQuestionPaperType)
        {
            _myDbContext.ExaminationQuestionPaperType.Add(examinationQuestionPaperType);
            _myDbContext.SaveChanges();
            return true;
        }
        public bool DeleteExaminationQuesionPaperTypes(ExaminationQuestionPaperType examinationQuestionPaperType)
        {
            ExaminationQuestionPaperType dbExaminationQuestion = _myDbContext.ExaminationQuestionPaperType.FirstOrDefault(c => c.ExaminationQuestionId == examinationQuestionPaperType.ExaminationQuestionId && c.PaperQuestionTypeId == examinationQuestionPaperType.PaperQuestionTypeId);
            if (dbExaminationQuestion == null) return true;
            _myDbContext.ExaminationQuestionPaperType.Remove(dbExaminationQuestion);
            _myDbContext.SaveChanges();
            return true;
        }
        public TestPaper GetTestPapperById(int id )
        {
            TestPaper testPaper = _myDbContext.TestPaper.Include(c=>c.Subject).Include(c=>c.PaperQuestionTypes).AsNoTracking().FirstOrDefault(c => c.Id == id);
            return testPaper;
        }
        public string AutoAddTestPaper(TestPaper testPaper,double percent)
        {
            using (DbContextTransaction transaction = _myDbContext.Database.BeginTransaction())
            {
                try
                {
                    testPaper.IsFinish = true;
                    _myDbContext.TestPaper.Add(testPaper);
                    _myDbContext.SaveChanges();
                    int QuestionTypenumber = (int)((percent * 10) / 2);
            
                    List<QuestionType> questionType = new List<QuestionType>();
                    foreach(var qi in _myDbContext.QuestionType.Include(c=>c.ExaminationQuestions).ToArray())
                    {
                        if (qi.ExaminationQuestions.Count() > 0)
                        {
                            questionType.Add(qi);
                        }

                    }
                    int percentNum = questionType.Count();
                    Random rd = new Random();
                    List<ExaminationQuestion> examinationQuestions = _myDbContext.ExaminationQuestion.Where(c => c.SubjectId == testPaper.SubjectId).ToList();
                    if (examinationQuestions.Count() == 0)
                    {

                        return "该科目题库没有题目";
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        int questionTypeNumber = rd.Next(0, 3);
                        PaperQuestionType paperQuestionType = new PaperQuestionType()
                        {
                            PaperQuestionTitle = questionType[questionTypeNumber].QuestionTypeName,
                            QuestionTypeId = questionType[questionTypeNumber].Id,
                            TestPaperId = testPaper.Id

                        };
                        _myDbContext.PaperQuestionType.Add(paperQuestionType);
                        _myDbContext.SaveChanges();
                        for (int j = 0; j < 5; j++)
                        {
                            ExaminationQuestion examinationQuestion = GetRandomExaminationQuestion(examinationQuestions, questionType[questionTypeNumber].Id, percentNum);
                            _myDbContext.ExaminationQuestionPaperType.Add(new ExaminationQuestionPaperType()
                            {
                                PaperQuestionTypeId = paperQuestionType.Id,
                                ExaminationQuestionId = examinationQuestion.Id
                            });
                        }
                    }
                    _myDbContext.SaveChanges();
                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                }
             
            }
            return "生成成功";
        }

        public string AutoAddTemplateTestPaper(TestPaper testPaper, double percent)
        {
            using (DbContextTransaction transaction = _myDbContext.Database.BeginTransaction())
            {
                try
                {
               
                    testPaper.IsFinish = true;
                    _myDbContext.TestPaper.Add(testPaper);
                    _myDbContext.SaveChanges();
                    int percentNum = (int)((percent * 10));
                    List<QuestionType> questionTypes = new List<QuestionType>();
                    foreach (var qt in _myDbContext.QuestionType.Include(c => c.ExaminationQuestions).ToList())
                    {
                        int count = qt.ExaminationQuestions.Where(c => c.SubjectId == testPaper.SubjectId).Count();
                        if (count >= 10)
                        {
                            questionTypes.Add(qt);
                        }
                    };


                    if (questionTypes.Count() < 3)
                    {
                        return "题目类型不足";
                    }
                   
                    List<ExaminationQuestion> examinationQuestions = _myDbContext.ExaminationQuestion.Where(c => c.SubjectId == testPaper.SubjectId).ToList();

                    #region 添加非选择题
                    QuestionType questionType2 = questionTypes.FirstOrDefault(c => !c.HasOption);
                    if (questionType2 == null)
                    {
                        transaction.Rollback();
                        return "题目类型不足";
                    }
                    else
                    {
                        questionTypes.Remove(questionType2);
                    }
                    PaperQuestionType paperQuestionType2 = new PaperQuestionType()
                    {
                        PaperQuestionTitle = questionType2.QuestionTypeName,
                        QuestionTypeId = questionType2.Id,
                        TestPaperId = testPaper.Id
                    };
                    _myDbContext.PaperQuestionType.Add(paperQuestionType2);
                    _myDbContext.SaveChanges();
                    for (int j = 0; j < 10; j++)
                    {
                        ExaminationQuestion examinationQuestion = GetRandomExaminationQuestion(examinationQuestions, questionType2.Id, percentNum);
                        if (examinationQuestion == null)
                        {
                            transaction.Rollback();
                            return "该科目题库题目数量不足";
                        }
                        examinationQuestions.Remove(examinationQuestion);
                        _myDbContext.ExaminationQuestionPaperType.Add(new ExaminationQuestionPaperType()
                        {
                            PaperQuestionTypeId = paperQuestionType2.Id,
                            ExaminationQuestionId = examinationQuestion.Id
                        });
                    }

                    #endregion
                    #region 添加非选择题
                    QuestionType questionType3 = questionTypes.FirstOrDefault(c => !c.HasOption);
                    if (questionType3 == null)
                    {
                        transaction.Rollback();
                        return "题目类型不足";
                    }
                    else
                    {
                        questionTypes.Remove(questionType3);
                    }
                    PaperQuestionType paperQuestionType3 = new PaperQuestionType()
                    {
                        PaperQuestionTitle = questionType3.QuestionTypeName,
                        QuestionTypeId = questionType3.Id,
                        TestPaperId = testPaper.Id
                    };
                    _myDbContext.PaperQuestionType.Add(paperQuestionType3);
                    _myDbContext.SaveChanges();
                    for (int j = 0; j < 5; j++)
                    {
                        ExaminationQuestion examinationQuestion = GetRandomExaminationQuestion(examinationQuestions, questionType3.Id, percentNum);
                        if (examinationQuestion == null)
                        {
                            transaction.Rollback();
                            return "该科目题库题目数量不足";
                        }
                        examinationQuestions.Remove(examinationQuestion);
                        _myDbContext.ExaminationQuestionPaperType.Add(new ExaminationQuestionPaperType()
                        {
                            PaperQuestionTypeId = paperQuestionType3.Id,
                            ExaminationQuestionId = examinationQuestion.Id
                        });
                    }

                    #endregion

                    #region 添加选择题
                    QuestionType questionType = questionTypes.FirstOrDefault(c => c.HasOption);
                    if (questionType == null)
                    {
                        return "题目类型不足";
                    }
                    else
                    {
                        questionTypes.Remove(questionType);
                    }
                    if (examinationQuestions.Count() == 0)
                    {

                        return "该科目题库没有题目";
                    }
                    if (examinationQuestions.Count() < 20)
                    {

                        return "该科目题库题目数量不足";
                    }
                    PaperQuestionType paperQuestionType = new PaperQuestionType()
                    {
                        PaperQuestionTitle = questionType.QuestionTypeName,
                        QuestionTypeId = questionType.Id,
                        TestPaperId = testPaper.Id
                    };
                    _myDbContext.PaperQuestionType.Add(paperQuestionType);
                    _myDbContext.SaveChanges();
                    for (int j = 0; j < 10; j++)
                    {
                        ExaminationQuestion examinationQuestion = GetRandomExaminationQuestion(examinationQuestions, questionType.Id, percentNum);
                        if (examinationQuestion == null)
                        {
                            transaction.Rollback();
                            return "该科目题库题目数量不足";
                        }
                        examinationQuestions.Remove(examinationQuestion);
                        _myDbContext.ExaminationQuestionPaperType.Add(new ExaminationQuestionPaperType()
                        {
                            PaperQuestionTypeId = paperQuestionType.Id,
                            ExaminationQuestionId = examinationQuestion.Id
                        });
                    }

                    #endregion
                    _myDbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return e.Message;
                }

            }
            return "生成成功";
        }
        public ExaminationQuestion GetRandomExaminationQuestion(List<ExaminationQuestion> examinationQuestions, int questionTypeId,int percentNum)
        {
            var list= examinationQuestions.Where(c =>  c.QuestionTypeId == questionTypeId && c.DifficultyGrade >= percentNum).ToList();
            int examinationQuestionsCountNum = list.Count();
            Random rd = new Random();
            int questionTypeNumber = rd.Next(0, examinationQuestionsCountNum);
            if (examinationQuestionsCountNum == 0)
            {
                return null;
            }
            return list[questionTypeNumber];
        }
        public List<TestPaper> GetAllTestPaper(string seach)
        {
            IQueryable<TestPaper> result = _myDbContext.TestPaper.Include(c=>c.Subject).Include(c=>c.PaperQuestionTypes).OrderByDescending(c=>c.Id).Where(c => c.IsFinish);
            if (!string.IsNullOrEmpty(seach))
            {
                result = result.Where(c => c.Title.Contains(seach));
            }
            List<TestPaper> testPapers = result.ToList();
            return testPapers;
        }
        public TestPaper GetTestPaperById(int id)
        {
            TestPaper result = _myDbContext.TestPaper.Include(c => c.Subject).Include(c => c.PaperQuestionTypes).First(c => c.Id == id);
            if (result.PaperQuestionTypes != null)
            {

                foreach (var paperQuestionType in result.PaperQuestionTypes)
                {
                    paperQuestionType.QuestionType = _myDbContext.QuestionType.FirstOrDefault(c => c.Id == paperQuestionType.QuestionTypeId);
                    paperQuestionType.ExaminationQuestionPaperTypes = _myDbContext.ExaminationQuestionPaperType.Include(c => c.ExaminationQuestion).Where(c => c.PaperQuestionTypeId == paperQuestionType.Id).ToArray();
                    foreach(var exa in paperQuestionType.ExaminationQuestionPaperTypes)
                    {
                        exa.ExaminationQuestion.OptionItems = _myDbContext.OptionItem.Where(c => c.ExaminationQuestionsId == exa.ExaminationQuestion.Id).ToArray();
                    }
                }
            }
          
            return result;
        }
    }
}
