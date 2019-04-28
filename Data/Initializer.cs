using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Infrastructure.Data
{
    internal class Initializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context database)
        {
            var roleStudent = new Role
            {
                Description = "Проходящий практику",
                Value = "Студент"
            };
            var roleWorker = new Role
            {
                Description = "Дополнительное описание",
                Value = "Старший сержант"
            };
            var roleAdmin = new Role
            {
                Description = "Администратор системы",
                Value = "Admin"
            };
            var group1 = new Group
            {
                Description = "ИФСТ",
                Department = "ИнПИТ",
                University = "СГТУ",
                Start = DateTime.Now,
                End = DateTime.Now
            };
            var student1 = new User
            {
                Email = "petrov@gmail.com",
                Group = group1,
                Midname = "Петрович",
                Name = "Петр",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleStudent,
                Status = true,
                Surname = "Петров"
            };
            var student2 = new User
            {
                Email = "sidorov@gmail.com",
                Group = group1,
                Midname = "Сидорович",
                Name = "Сидр",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleStudent,
                Status = true,
                Surname = "Сидров"
            };
            var curator1 = new User
            {
                Email = "ivanov@gmail.com",
                Midname = "Иванович",
                Name = "Иван",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleWorker,
                Status = true,
                Surname = "Иванов"
            };
            database.User.Add(student1);
            database.User.Add(student2);
            database.User.Add(curator1);

            database.SaveChanges();

            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student1
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student2
            });
            database.SaveChanges();
            var report1 = new Report
            {
                Link = "Hyperlink#1",
                User = student1
            };
            var report2 = new Report
            {
                Link = "Hyperlink#2",
                User = student2
            };
            database.Template.Add(new Template
            {
                Description = "Description#1",
                Reports = new List<Report>
                {
                    report1, report2
                }
            });

            database.Curator.Add(new Curator
            {
                User = curator1,
                Students = new List<User>
                {
                    student1,
                    student2
                }
            });
            database.SaveChanges();
            var type1 = new Core.Type
            {
                Desription = "Данный тип предназначен для выбора одного из правильных ответов",
                Status = "Выбор ответа"
            };
            var type2 = new Core.Type
            {
                Desription = "Данный тип предназначен для ввода ответа",
                Status = "Ввод ответа"
            };

            var question1 = new Question
            {
                Description = "Сколько океанов на нашей планете?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "4",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "5",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "6",
                        Correct = false
                    }
                }
            };
            var question2 = new Question
            {
                Description = "Единица изменения силы тока - это:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Ампер",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Ватт",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Вольт",
                        Correct = false
                    }
                }
            };
            var question3 = new Question
            {
                Description = "Сатурн - это какая по счету планета от Солнца?",
                Type = type2,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "6",
                        Correct = true
                    }
                }
            };
            var question4 = new Question
            {
                Description = "Какой элемент периодической системы химических элементов обозначается как Ag?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Золото",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Серебро",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Аргон",
                        Correct = false
                    }
                }
            };
            var question5 = new Question
            {
                Description = "Сколько будет 0,2 км в дециметрах?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "20000 дм",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "2000 дм",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "200 дм",
                        Correct = false
                    }
                }
            };
            var question6 = new Question
            {
                Description = "Самая длинная в мире река - это:",
                Type = type2,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Амазонка",
                        Correct = true
                    }
                }
            };
            var question7 = new Question
            {
                Description = "Какое число обозначается римскими цифрами LXXVII?",
                Type = type2,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "77",
                        Correct = true
                    }
                }
            };
            var question8 = new Question
            {
                Description = "В каком предложении не допущена ошибка?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "На ней не было чулок",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "На полке лежала пачка макаронов",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Эти кремы просрочены",
                        Correct = false
                    }
                }
            };
            var question9 = new Question
            {
                Description = "Зеленый пигмент, окрашивающий листья растений, называется:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Хлорофиллипт",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Хлоропласт",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Хлорофилл",
                        Correct = true
                    }
                }
            };
            var question10 = new Question
            {
                Description = "Все знают приставку <кило->. А как насчет <гекто->? Это сколько?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "100",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "1000",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "10000",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "100000",
                        Correct = false
                    }
                }
            };
            var question11 = new Question
            {
                Description = "Почему времена года сменяют друг друга?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Из-за удаления и приближения Земли к Солнцу",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Из-за наклона земной оси",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Из-за вращения Земли вокруг своей оси",
                        Correct = false
                    }
                }
            };
            var question12 = new Question
            {
                Description = "В какой строке все слова написаны правильно?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Серебряный подстаканник, ветренный день, румяный юноша",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Кожаный ремень, соленый суп, избалованный ребенок",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Истинный джентльмен, лебединая песня, песчанный пляж",
                        Correct = false
                    }
                }
            };
            var question13 = new Question
            {
                Description = "Сколько хромосом в геноме человека?",
                Type = type2,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "46",
                        Correct = true
                    }
                }
            };

            var section1 = new Section
            {
                Description = "Школьная программа"
            };
            var section2 = new Section
            {
                Description = "Математический анализ"
            };
            var test1 = new Test
            {
                Description = "Тест первый о ...",
                Questions = new List<Question>
                {
                    question1,
                    question2,
                    question3,
                    question4,
                    question5,
                    question6,
                    question7
                },
                Section = section1,
                Title = "Тест обо всем"
            };
            var test2 = new Test
            {
                Description = "Тест первый о ...",
                Questions = new List<Question>
                {
                    question8,
                    question9,
                    question10,
                    question11,
                    question12,
                    question13
                },
                Section = section2,
                Title = "Тест почти всем"
            };
            database.Test.Add(test1);
            database.Test.Add(test2);
            database.Course.Add(new Course
            {
                Description = "Лекционные материалы для ...",
                Link = "Ссылка на материал",
                Section = section1
            });
            database.Course.Add(new Course
            {
                Description = "Лекционные материалы для ...",
                Link = "Ссылка на материал",
                Section = section2
            });
            database.SaveChanges();
            database.Grade.Add(new Grade
            {
                Test = test1,
                User = student1,
                Value = 1
            });
            database.Grade.Add(new Grade
            {
                Test = test1,
                User = student2,
                Value = 0.33
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student2,
                Value = 0.85
            });
            database.SaveChanges();
        }
    }
}