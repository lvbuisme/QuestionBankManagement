using QuestionBankManagement;
using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UserBusiness
    {
        private MyDbContext _myDbContext;
        public UserBusiness()
        {
            _myDbContext = new MyDbContext();
        }

        public User login(string account, string password)
        {
            //User Testuser = _myDbContext.User.FirstOrDefault(c => c.Account.Equals("lvbu") && c.Password.Equals("123456"));
            //if (Testuser == null)
            //{
            //    _myDbContext.User.Add(new User() { Id = 2, Account = "lvbu", MobilePhone = "123456", Password = "123456", Role=new Role() { RoleName="系统管理员"}, UserName = "吕布" });
            //    _myDbContext.SaveChanges();
            //}
            Init();
            User user = _myDbContext.User.FirstOrDefault(c => c.Account.Equals(account) && c.Password.Equals(password));
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }

        }
        public bool VerifyAccountExists(string account)
        {
            User user = _myDbContext.User.FirstOrDefault(c => c.Account.Equals(account));
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool AddUser(User user)
        {

            _myDbContext.User.Add(user);
            _myDbContext.SaveChanges();
            return true;

        }
        public List<User> GetUserList()
        {
            List<User> users = _myDbContext.User.ToList();
            return users;

        }
        public bool DeleteUser(int userId)
        {
            if (userId == 1) return false;
            User user = _myDbContext.User.FirstOrDefault(c => c.Id == userId);
            if (user != null)
            {
                _myDbContext.User.Remove(user);
                _myDbContext.SaveChanges();
            }
            return true;
        }
        public bool UpdateUser(User user)
        {
            if (user.Id == 1) return false;
            User dbUser = _myDbContext.User.FirstOrDefault(c => c.Id == user.Id);
            if (dbUser != null)
            {
                dbUser.MobilePhone = user.MobilePhone;
                dbUser.Password = user.Password;
                dbUser.RoleId = user.RoleId;
                dbUser.UserName = user.UserName;
                dbUser.Account = user.Account;
                _myDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public User GetUserById(int userId)
        {
            User user = _myDbContext.User.FirstOrDefault(c => c.Id == userId);

            return user;
        }
        public List<RoleRight> GetRoleRightByRoleId(int roleId)
        {
            List<RoleRight> roleRights = _myDbContext.RoleRight.Where(c => c.RoleId == roleId).ToList();
            return roleRights;
        }
        public bool UpdateRoleRight(bool isRight, string rightName, int roleId)
        {
            if (isRight)
            {
                RoleRight roleRight = _myDbContext.RoleRight.FirstOrDefault(c => c.RoleId == roleId && c.RightName.Equals(rightName));
                if (roleRight == null)
                {
                    _myDbContext.RoleRight.Add(new RoleRight() { RightName = rightName, RoleId = roleId });
                }
            }
            else
            {
                RoleRight roleRight = _myDbContext.RoleRight.FirstOrDefault(c => c.RoleId == roleId && c.RightName.Equals(rightName));
                if (roleRight != null)
                {
                    _myDbContext.RoleRight.Remove(roleRight);
                }
            }
            _myDbContext.SaveChanges();
            return true;
        }
        public void Init()
        {
            User Testuser = _myDbContext.User.FirstOrDefault(c => c.Account.Equals("lvbu"));
            if (Testuser != null)
            {
                return;
            }
            _myDbContext.Role.Add(new Role()
            {
                Id = 1,
                RoleName = "系统管理员",
                RoleRights = new List<RoleRight>() {
                    new RoleRight() { RightName = "UserManager" },
                     new RoleRight() { RightName = "QuestionBankManager" },
                      new RoleRight() { RightName = "ExaminationPaperManager" },
                }
            });
            _myDbContext.Role.Add(new Role() { Id = 2, RoleName = "老师" });
            _myDbContext.Role.Add(new Role() { Id = 3, RoleName = "学生" });
            _myDbContext.User.Add(new User() { Id = 2, Account = "lvbu", MobilePhone = "123456", Password = "123456", RoleId = 1, UserName = "吕布" });
            _myDbContext.QuestionType.Add(new QuestionType() { Id = 2, QuestionTypeName = "应用题", IsFixedScore = false, HasOption = false });
            _myDbContext.QuestionType.Add(new QuestionType() { Id = 3, QuestionTypeName = "填空题", IsFixedScore = false, HasOption = false });
        
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "字符的ASCII编码在计算机中的表示方法准确的描述应是()",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="使用8位二进制代码，最右边一位为1" },
                    new OptionItem() { OptionContent = "使用8位二进制代码，最左边一位为0" },
                    new OptionItem() { OptionContent = "使用8位二进制代码，最右边一位为0" },
                    new OptionItem() { OptionContent = "使用8位二进制代码，最左边一位为" }
                },
                KnowledgePoint = new KnowledgePoint() { Id = 1, KnowledgePointName = "数据结构" },
                QuestionType = new QuestionType() { Id=1, QuestionTypeName = "选择题", IsFixedScore = false, HasOption = true },
                Subject = new Subject() { Id = 1, SubjectName = "计算机" },
            });
            _myDbContext.SaveChanges();
            #region 选择题
            #endregion

            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "在16×16点阵字库中，存储一个汉字的字模信息需用的字节数是()",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="8 " },
                    new OptionItem() { OptionContent = "16 " },
                    new OptionItem() { OptionContent = "32 " },
                    new OptionItem() { OptionContent = "64 " }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "下列字符中，ASCII码值最大的是",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="Y " },
                    new OptionItem() { OptionContent = "y " },
                    new OptionItem() { OptionContent = "A " },
                    new OptionItem() { OptionContent = "a " }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "计算机的微处理芯片上集成有( )部件",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="CPU和运算器" },
                    new OptionItem() { OptionContent = "运算器和I/O接口 " },
                    new OptionItem() { OptionContent = "控制器和运算器" },
                    new OptionItem() { OptionContent = "控制器和存储器 " }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "计算机硬件系统中的运算器又称为( )",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="算术逻辑单元" },
                    new OptionItem() { OptionContent = "算术运算单元 " },
                    new OptionItem() { OptionContent = "逻辑运算单元" },
                    new OptionItem() { OptionContent = "加法器" }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "计算机硬件系统基本包括( )",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="CPU、存储器和I/O " },
                    new OptionItem() { OptionContent = "主机和存储器 " },
                    new OptionItem() { OptionContent = "外设和CPU  " },
                    new OptionItem() { OptionContent = "控制器和运算器 " }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "在微机的性能指标中，内存条的容量通常是指( )",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="RAM的容量" },
                    new OptionItem() { OptionContent = "ROM的容量 " },
                    new OptionItem() { OptionContent = "RAM和ROM的容量之和 " },
                    new OptionItem() { OptionContent = "CD-ROM的容量" }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "微机中，基本输入输出系统BIOS是( )",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="硬件" },
                    new OptionItem() { OptionContent = "软件" },
                    new OptionItem() { OptionContent = "总线" },
                    new OptionItem() { OptionContent = "外部设备" }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "动态RAM存储器的特点是( )",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="信息在断电后仍能保持" },
                    new OptionItem() { OptionContent = "不断电的情况下，信息不会自动消失" },
                    new OptionItem() { OptionContent = "信息不能随意更改" },
                    new OptionItem() { OptionContent = "信息必须在不断刷新的支持下才能保持" }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "在微机中运行某一程序时，若存储容量不够，可采用下列( )办法解决",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="虚拟内存" },
                    new OptionItem() { OptionContent = "更新CPU " },
                    new OptionItem() { OptionContent = "采用光盘" },
                    new OptionItem() { OptionContent = "采用高密度软盘 " }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "某个双面高密软盘格式化后，若每面有80个磁道，每个磁道有18个扇区，每个扇区有512个字节，则该软盘的容量是( )",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="720KB" },
                    new OptionItem() { OptionContent = "360KB" },
                    new OptionItem() { OptionContent = "1.44MB" },
                    new OptionItem() { OptionContent = "1.2MB " }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "在微型机中，I/O接口位于( )",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="总线和I/O设备之间" },
                    new OptionItem() { OptionContent = "CPU和I/O设备之间 " },
                    new OptionItem() { OptionContent = "主机和总线之间" },
                    new OptionItem() { OptionContent = "CPU和主存储器之间 " }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "在微型机使用中，下面正确的说法是( )",
                DifficultyGrade = 10,
                Score = 2,
                OptionItems = new OptionItem[] {
                    new OptionItem() { OptionContent="不可带电插拔各类接口卡" },
                    new OptionItem() { OptionContent = "可以带电插拔接口卡 " },
                    new OptionItem() { OptionContent = "可以带电接入外设" },
                    new OptionItem() { OptionContent = "可以带电卸下外设 " }
                },
                KnowledgePointId = 1,
                QuestionTypeId = 1,
                SubjectId = 1,
            });
            #region 填空题
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "折半查找失败时的最大次数",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId =1,
                QuestionTypeId = 2,
                SubjectId =1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "在软件工程各个阶段中有几个阶段可以软件复用",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 2,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "编译原理的几个环节",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 2,
                SubjectId = 1,

            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "浅谈大数据对生活带来的变革",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 2,
                SubjectId = 1,

            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "数字序列找规律",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 2,
                SubjectId = 1,

            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "浅谈对“互联网+”的理解",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 2,
                SubjectId = 1,

            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "么是系统调用，和过程调用有什么联系和区别",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 2,
                SubjectId = 1,

            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "描述一个自己的项目",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 2,
                SubjectId = 1,

            });
            #endregion
            #region 应用题
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "是否可以在static环境中访问非static变量？",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 3,
                SubjectId=1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "ava支持的数据类型有哪些？什么是自动拆装箱？",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 3,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "Java中的方法覆盖(Overriding)和方法重载(Overloading)是什么意思？",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 3,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "在监视器(Monitor)内部，是如何做线程同步的？程序应该做哪种级别的同步？",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 3,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "创建线程有几种不同的方式？你喜欢哪一种？为什么？",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 3,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "Java支持的数据类型有哪些？什么是自动拆装箱？",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 3,
                SubjectId = 1,
            });
            _myDbContext.ExaminationQuestion.Add(new ExaminationQuestion()
            {
                Content = "概括的解释下线程的几种可用状态。",
                DifficultyGrade = 10,
                Score = 2,
                KnowledgePointId = 1,
                QuestionTypeId = 3,
                SubjectId = 1,
            });
            #endregion

            _myDbContext.SaveChanges();
        }
    }
}
