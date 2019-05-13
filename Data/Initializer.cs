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
                Value = "Администратор"
            };
            var group1 = new Group
            {
                Description = "ИФСТ",
                Department = "ИнПИТ",
                University = "СГТУ",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(30)
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
            var admin = new User
            {
                Email = "email@gmail.com",
                Midname = "Сергеевич",
                Name = "Денис",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleAdmin,
                Status = true,
                Surname = "Еременко"
            };
            database.User.Add(student1);
            database.User.Add(student2);
            database.User.Add(curator1);
            database.User.Add(admin);

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
            database.Template.Add(new Template
            {
                Description = "Уровень владения профессиональными навыками"
            });
            database.Template.Add(new Template
            {
                Description = "Способность студента обучаться и применять знания на практике"
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
                Description = "Данный тип предназначен для выбора одного из правильных ответов",
                Status = "Выбор ответа"
            };
            var type2 = new Core.Type
            {
                Description = "Данный тип предназначен для ввода ответа",
                Status = "Ввод ответа"
            };

            var question1 = new Question
            {
                Description = "Понятие преступления определяется действующим УК РФ как:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Умышленное деяние, запрещенное нормативно- правовыми актами РФ под угрозой наказания",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Уголовно-наказуемое, умышленное, противоправное действие субъекта, запрещенное УК РФ",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Виновно совершенное общественно- опасное деяние, запрещенное УК РФ под угрозой наказания",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Противоправное общественно- опасное действие субъекта уголовной ответственности",
                        Correct = false
                    }
                }
            };
            var question2 = new Question
            {
                Description = "Категории преступлений, установленные в Уголовном кодексе:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Легкие, средние, тяжкие и особо тяжкие",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Небольшой тяжести, средней тяжести, тяжкие и особо тяжкие",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Легкие, средние, тяжкие",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Небольшой тяжести, и тяжкие",
                        Correct = false
                    }
                }
            };
            var question3 = new Question
            {
                Description = "Что понимается под временем совершения преступления?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Это промежуток времени, в течение которого совершается преступление",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "это время совершения общественно опасного деяния и время наступления последствий",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Это время, когда о совершенном преступлении стало известно правоохранительным органам",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "То время совершения общественно опасного действия (бездействия) независимо от наступления последствий",
                        Correct = false
                    }
                }
            };
            var question4 = new Question
            {
                Description = "Какой уголовный закон имеет обратную силу (укажите наиболее полный ответ)?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Который отменяет или изменяет действующий уголовный закон",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Который устраняет преступность деяния, смягчает наказание или иным способом улучшает положение лица, совершившего преступление",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Который устраняет преступность деяния, усиливает наказание или иным образом ухудшает положение лица, совершившего преступление",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Который сокращает сроки наказания, предусмотренные за совершение этого преступления",
                        Correct = false
                    }
                }
            };
            var question5 = new Question
            {
                Description = "В чем заключается территориальный принцип действия уголовного закона?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Применяется уголовный закон места совершения преступления",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Граждане РФ подчиняются российским законам, где бы они ни находились",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Применяется закон места пресечения преступной деятельности",
                        Correct = false
                    }
                }
            };
            var question6 = new Question
            {
                Description = "Подлежат ли выдаче иностранному государству граждане РФ, совершившие преступление на территории этого государства?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Подлежат",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Могут быть выданы для привлечения к уголовной ответственности или отбытия наказания в соответствии с международным договором",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Не подлежат",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Могут быть объявлены персонами «нон грата» и высланы из страны",
                        Correct = false
                    }
                }
            };
            var question7 = new Question
            {
                Description = "В основу классификации преступлений положено:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Степень вины",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Размер причиненного ущерба",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Характер и степень общественной опасности",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Размер наказания",
                        Correct = false
                    }
                }
            };
            var question8 = new Question
            {
                Description = "Какого вида преступления не существует?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Легкого",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Небольшой тяжести",
                        Correct = false
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
                Description = "Уголовное наказание не может быть целью:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Исправления осужденного",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Восстановления справедливости",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Устрашения",
                        Correct = true
                    }
                }
            };
            var question10 = new Question
            {
                Description = "Какие из перечисленных ниже пунктов являются признаками преступления:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Общественная опасность деяния",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Наличие вины",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Запрещенность деяния законом",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Наказуемость деяния",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Все перечисленные",
                        Correct = true
                    }
                }
            };
            var question11 = new Question
            {
                Description = "Без кого из указанных ниже лиц преступление не может быть осуществлено?",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Исполнитель",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Организатор",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Подстрекатель",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Пособник",
                        Correct = false
                    }
                }
            };
            var question12 = new Question
            {
                Description = "Смертная казнь в РФ:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Существует в УК РФ, применяется судами и приводится в исполнение",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Существует в УК РФ, применяется судами, но не приводится в исполнение",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Существует в УК РФ, не применяется судами и не приводится в исполнение",
                        Correct = true
                    }
                }
            };
            var question13 = new Question
            {
                Description = "Умышленные и неосторожные деяния, за совершение которых максимальное наказание, предусмотренное УК РФ, не превышает 10 лет лишения свободы, являются:",
                Type = type1,
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Desctiption = "Особо тяжкими преступлениями",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Тяжкими преступлениями",
                        Correct = true
                    },
                    new Answer
                    {
                        Desctiption = "Преступлениями средней тяжести",
                        Correct = false
                    },
                    new Answer
                    {
                        Desctiption = "Легкими преступлениями",
                        Correct = false
                    }
                }
            };

            var section1 = new Section
            {
                Description = "Судебная экспертиза"
            };
            var section2 = new Section
            {
                Description = "Уголовное право"
            };
            var test1 = new Test
            {
                Description = "Тест по уголовному праву",
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
                Title = "Уголовное право"
            };
            var test2 = new Test
            {
                Description = "Тест по судебной экспертизе",
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
                Title = "Судебное экспертиза"
            };
            database.Test.Add(test1);
            database.Test.Add(test2);
            database.Course.Add(new Course
            {
                Description = "Лекционные материалы по уголовному праву",
                Link = "6a6f40db-981a-41f9-8c76-473f95351791.pdf",
                Section = section1
            });
            database.Course.Add(new Course
            {
                Description = "Лекционные материалы по уголовному праву",
                Link = "d1682315-c188-4c98-a44f-4af8ba239c1c.pdf",
                Section = section1
            });
            database.Course.Add(new Course
            {
                Description = "Лекционные материалы по судебной экспертизе",
                Link = "b953c69d-b793-46f4-8886-33cc45047cf8.pdf",
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
            database.Type.Add(type2);
            database.SaveChanges();
        }
    }
}