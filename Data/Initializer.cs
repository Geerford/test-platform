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
            #region Roles
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
            #endregion
            #region Groups
            var group1 = new Group
            {
                Description = "401",
                Department = "Институт прокуратуры",
                University = "СГЮА",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(30)                
            };
            var group2 = new Group
            {
                Description = "402",
                Department = "Институт прокуратуры",
                University = "СГЮА",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(40)
            };
            #endregion
            #region Users
            var student1 = new User
            {
                Email = "belousova@gmail.com",
                Group = group1,
                Midname = "Александровна",
                Name = "Мария",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleStudent,
                Status = true,
                Surname = "Белоусова"
            };
            var student2 = new User
            {
                Email = "polancev@gmail.com",
                Group = group1,
                Midname = "Алексеевич",
                Name = "Александр",
                Password = "pass",
                Phone = "+7902963453",
                Role = roleStudent,
                Status = true,
                Surname = "Полянцев"
            };
            var student3 = new User
            {
                Email = "maslov@gmail.com",
                Group = group1,
                Midname = "Александрович",
                Name = "Кирилл",
                Password = "pass",
                Phone = "+7902535343",
                Role = roleStudent,
                Status = true,
                Surname = "Масленников"
            };
            var student4 = new User
            {
                Email = "antonov@gmail.com",
                Group = group1,
                Midname = "Романович",
                Name = "Данила",
                Password = "pass",
                Phone = "+7934538192",
                Role = roleStudent,
                Status = true,
                Surname = "Антонов"
            };
            var student5 = new User
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

            var student6 = new User
            {
                Email = "efirova@gmail.com",
                Group = group1,
                Midname = "Николаевна",
                Name = "Таисия",
                Password = "pass",
                Phone = "+79271425639",
                Role = roleStudent,
                Status = true,
                Surname = "Эфирова"
            };
            var student7 = new User
            {
                Email = "komzina@gmail.com",
                Group = group1,
                Midname = "Казимировна",
                Name = "Марфа",
                Password = "pass",
                Phone = "+79879512637",
                Role = roleStudent,
                Status = true,
                Surname = "Комзина"
            };
            var student8 = new User
            {
                Email = "anisimov@gmail.com",
                Group = group1,
                Midname = "Прохорович",
                Name = "Адриан",
                Password = "pass",
                Phone = "+79872587485",
                Role = roleStudent,
                Status = true,
                Surname = "Анисимов"
            };
            var student9 = new User
            {
                Email = "yasaveeva@gmail.com",
                Group = group1,
                Midname = "Андрияновна",
                Name = "Дарья",
                Password = "pass",
                Phone = "+79375596478",
                Role = roleStudent,
                Status = true,
                Surname = "Ясавеева"
            };
            var student10 = new User
            {
                Email = "ganichev@gmail.com",
                Group = group1,
                Midname = "Михаилович",
                Name = "Феликс",
                Password = "pass",
                Phone = "+79372254455",
                Role = roleStudent,
                Status = true,
                Surname = "Ганичев"
            };
            var student11 = new User
            {
                Email = "tarnovezzki@gmail.com",
                Group = group1,
                Midname = "Сократович",
                Name = "Ярослав",
                Password = "pass",
                Phone = "+79877415896",
                Role = roleStudent,
                Status = true,
                Surname = "Тарновецкий"
            };
            var student12 = new User
            {
                Email = "zimnakova@gmail.com",
                Group = group1,
                Midname = "Олеговна",
                Name = "Альбина",
                Password = "pass",
                Phone = "+79275696965",
                Role = roleStudent,
                Status = true,
                Surname = "Зимнякова"
            };
            var student13 = new User
            {
                Email = "alexandrova@gmail.com",
                Group = group1,
                Midname = "Василиевна",
                Name = "Наталья",
                Password = "pass",
                Phone = "+79874569878",
                Role = roleStudent,
                Status = true,
                Surname = "Александрова"
            };
            var student14 = new User
            {
                Email = "galigin@gmail.com",
                Group = group1,
                Midname = "Арсениевич",
                Name = "Константин",
                Password = "pass",
                Phone = "+79054569878",
                Role = roleStudent,
                Status = true,
                Surname = "Галыгин"
            };
            var student15 = new User
            {
                Email = "babanin@gmail.com",
                Group = group1,
                Midname = "Геннадиевич",
                Name = "Олег",
                Password = "pass",
                Phone = "+79051259856",
                Role = roleStudent,
                Status = true,
                Surname = "Бабанин"
            };
            var student16 = new User
            {
                Email = "rodikov@gmail.com",
                Group = group1,
                Midname = "Артемович",
                Name = "Ефрем",
                Password = "pass",
                Phone = "+79875526373",
                Role = roleStudent,
                Status = true,
                Surname = "Родиков"
            };
            var student17 = new User
            {
                Email = "kopeikina@gmail.com",
                Group = group1,
                Midname = "Владиленовна",
                Name = "Евдокия",
                Password = "pass",
                Phone = "+79025471494",
                Role = roleStudent,
                Status = true,
                Surname = "Копейкина"
            };
            var student18 = new User
            {
                Email = "bugaev@gmail.com",
                Group = group1,
                Midname = "Ираклиевич",
                Name = "Эмиль",
                Password = "pass",
                Phone = "+79379476854",
                Role = roleStudent,
                Status = true,
                Surname = "Бугаев"
            };
            var student19 = new User
            {
                Email = "gudkova@gmail.com",
                Group = group2,
                Midname = "Юлиевна",
                Name = "Рада",
                Password = "pass",
                Phone = "+79875478532",
                Role = roleStudent,
                Status = true,
                Surname = "Гудкова"
            };
            var student20 = new User
            {
                Email = "nabadchikov@gmail.com",
                Group = group2,
                Midname = "Сергеевич",
                Name = "Владилен",
                Password = "pass",
                Phone = "+79375698512",
                Role = roleStudent,
                Status = true,
                Surname = "Набадчиков"
            };
            var student21 = new User
            {
                Email = "oskin@gmail.com",
                Group = group2,
                Midname = "Платонович",
                Name = "Павел",
                Password = "pass",
                Phone = "+79025478511",
                Role = roleStudent,
                Status = true,
                Surname = "Оськин"
            };
            var student22 = new User
            {
                Email = "lobov@gmail.com",
                Group = group2,
                Midname = "Кондратович",
                Name = "Евсей",
                Password = "pass",
                Phone = "+79059113245",
                Role = roleStudent,
                Status = true,
                Surname = "Лобов"
            };
            var student23 = new User
            {
                Email = "kribzov@gmail.com",
                Group = group2,
                Midname = "Романович",
                Name = "Валентин",
                Password = "pass",
                Phone = "+79875421985",
                Role = roleStudent,
                Status = true,
                Surname = "Скребцов"
            };
            var student24 = new User
            {
                Email = "borshov@gmail.com",
                Group = group2,
                Midname = "Игоревич",
                Name = "Матвей",
                Password = "pass",
                Phone = "+79876659887",
                Role = roleStudent,
                Status = true,
                Surname = "Борщев"
            };
            var student25 = new User
            {
                Email = "sinizzin@gmail.com",
                Group = group2,
                Midname = "Захарович",
                Name = "Максим",
                Password = "pass",
                Phone = "+79056516567",
                Role = roleStudent,
                Status = true,
                Surname = "Синицын"
            };
            var student26 = new User
            {
                Email = "solovev@gmail.com",
                Group = group2,
                Midname = "Сидорович",
                Name = "Потап",
                Password = "pass",
                Phone = "+79370220547",
                Role = roleStudent,
                Status = true,
                Surname = "Соловьев"
            };
            var student27 = new User
            {
                Email = "habarov@gmail.com",
                Group = group2,
                Midname = "Маркович",
                Name = "Кирилл",
                Password = "pass",
                Phone = "+79873665469",
                Role = roleStudent,
                Status = true,
                Surname = "Хабаров"
            };
            var student28 = new User
            {
                Email = "rataev@gmail.com",
                Group = group2,
                Midname = "Георгиевич",
                Name = "Кирилл",
                Password = "pass",
                Phone = "+79875474421",
                Role = roleStudent,
                Status = true,
                Surname = "Ратаев"
            };
            var student29 = new User
            {
                Email = "shemakin@gmail.com",
                Group = group2,
                Midname = "Игоревич",
                Name = "Григорий",
                Password = "pass",
                Phone = "+79373563964",
                Role = roleStudent,
                Status = true,
                Surname = "Шемякин"
            };
            var student30 = new User
            {
                Email = "skvirzov@gmail.com",
                Group = group2,
                Midname = "Игоревич",
                Name = "Вадим",
                Password = "pass",
                Phone = "+79876542181",
                Role = roleStudent,
                Status = true,
                Surname = "Дворцов"
            };
            var student31 = new User
            {
                Email = "zimin@gmail.com",
                Group = group2,
                Midname = "Никитевич",
                Name = "Вячеслав",
                Password = "pass",
                Phone = "+79876595951",
                Role = roleStudent,
                Status = true,
                Surname = "Головко"
            };
            var curator1 = new User
            {
                Email = "zizirev@gmail.com",
                Group = group2,
                Midname = "Назарович",
                Name = "Арсений",
                Password = "pass",
                Phone = "+79098521456",
                Role = roleWorker,
                Status = true,
                Surname = "Цызырев"
            };
            var curator2 = new User
            {
                Email = "ivanov@gmail.com",
                Midname = "Иванович",
                Name = "Дмитрий",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleWorker,
                Status = true,
                Surname = "Михайлович"
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
            database.User.Add(student3);
            database.User.Add(student4);
            database.User.Add(student5);
            database.User.Add(student6);
            database.User.Add(student7);
            database.User.Add(student8);
            database.User.Add(student9);
            database.User.Add(student10);
            database.User.Add(student11);
            database.User.Add(student12);
            database.User.Add(student13);
            database.User.Add(student14);
            database.User.Add(student15);
            database.User.Add(student16);
            database.User.Add(student17);
            database.User.Add(student18);
            database.User.Add(student19);
            database.User.Add(student20);
            database.User.Add(student21);
            database.User.Add(student22);
            database.User.Add(student23);
            database.User.Add(student24);
            database.User.Add(student25);
            database.User.Add(student26);
            database.User.Add(student27);
            database.User.Add(student28);
            database.User.Add(student29);
            database.User.Add(student30);
            database.User.Add(student31);
            database.User.Add(curator1);
            database.User.Add(curator2);
            database.User.Add(admin);
            database.SaveChanges();
            #endregion
            #region Activity
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
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student12
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student3
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student4
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student5
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student7
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student8
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student10
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student27
            });
            database.Activity.Add(new Activity
            {
                Date = DateTime.Now,
                User = student31
            });
            database.SaveChanges();
            #endregion
            #region Template
            database.Template.Add(new Template
            {
                Description = "Уровень владения профессиональными навыками"
            });
            database.Template.Add(new Template
            {
                Description = "Способность студента обучаться и применять знания на практике"
            });
            #endregion
            #region Curator
            database.Curator.Add(new Curator
            {
                User = curator1,
                Students = new List<User>
                {
                    student1,
                    student2,
                    student3,
                    student4,
                    student5,
                    student6,
                    student7,
                    student8,
                    student9,
                    student10,
                    student11,
                    student12,
                    student13,
                    student14
                }
            });
            database.Curator.Add(new Curator
            {
                User = curator2,
                Students = new List<User>
                {
                    student15,
                    student16,
                    student17,
                    student18,
                    student19,
                    student20,
                    student21,
                    student22,
                    student23,
                    student24,
                    student25,
                    student26,
                    student27,
                    student28,
                    student29,
                    student30,
                    student31
                }
            });
            database.SaveChanges();
            #endregion
            #region Questions
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
            #endregion
            #region Sections
            var section1 = new Section
            {
                Description = "Судебная экспертиза"
            };
            var section2 = new Section
            {
                Description = "Уголовное право"
            };
            var section3 = new Section
            {
                Description = "Общий профиль"
            };
            #endregion
            #region Tests
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
                Title = "Судебная экспертиза"
            };
            database.Test.Add(test1);
            database.Test.Add(test2);
            #endregion
            #region Tasks
            var task1 = new Task
            {
                Description = @"<p>Тринадцатилетний Мохов, страдающий олигофренией в легкой степени, 
                            встретил семидесятилетнего Савина и, угрожая игрушечным пистолетом, 
                            потребовал у него деньги. Савин ответил, что у него денег с собой нет. 
                            Тогда Мохов обыскал Савина, и не найдя у него денег, отпустил его, 
                            сказав: «Если не найдешь для меня 200 руб. и не принесешь их сюда, 
                            заказывай себе могилу».</p>
                            <p>Имеются ли в действиях Мохова признаки состава преступления и можно 
                            ли привлечь его к уголовной ответственности? (См. УК РФ ст. 14, 15, 
                            19, 21, 25, 29, 87, 162).</p>",
                Right = @"<p>Обязательные признаки состава в уголовном праве группируются по 
                            элементам состава преступления, которых всего четыре: объект, 
                            объективная сторона, субъективная сторона и субъект преступления. 
                            При отсутствии хотя бы одного из этих элементов, нельзя говорить 
                            о деянии как о преступлении и о наступлении уголовной ответственности 
                            за него (см. ст. 14 Уголовного кодекса РФ – Понятие преступления).</p>
                            <p>В нашем случае Мохов является несовершеннолетним лицом, страдающим 
                            олигофренией легкой степени (вероятнее всего, имеется ввиду дебильность
                            - легкая степень умственной отсталости). Ст. 21 УК РФ говорит о том, 
                            что лицо, которое во время совершения общественно опасного деяния 
                            находилось в состоянии невменяемости, то есть не могло осознавать 
                            фактический характер и общественную опасность своих действий 
                            (бездействия) либо руководить ими вследствие хронического психического 
                            расстройства, временного психического расстройства, слабоумия либо иного 
                            болезненного состояния психики, не подлежит уголовной ответственности.</p>    
                            <p>Согласно ст. 19, уголовной ответственности подлежит только вменяемое 
                            физическое лицо, достигшее установленного УК РФ возраста – т.е. шестнадцати 
                            лет (ст. 20). Таким образом, в данной ситуации тринадцатилетний Мохов, 
                            страдающий умственной отсталостью, пусть даже и в легкой степени, уголовной 
                            ответственности не подлежит.</p>
                            <p>Под ст. 87 (Уголовная ответственность несовершеннолетних) Мохов не попадает, 
                            т.к. не достиг четырнадцатилетнего возраста.</p>
                            <p>Если предположить, что такие же действия совершило бы совершеннолетнее 
                            вменяемое лицо, то уголовная ответственность за это наступила бы по статье 162 
                            (Разбой).</p>",
                Title = "Задание по судебной экспертизе",
                Section = section1
            };
            var task2 = new Task
            {
                Description = @"<p>Семнадцатилетние Дронов и Перов поместили в большую спортивную сумку 
                                Сухова, застегнули ее и сдали в камеру хранения железнодорожного вокзала. 
                                Ночью Сухов вылез из сумки, осмотрелся и обыскал содержимое ряда чемоданов 
                                и сумок. Изъятые им вещи и деньги положил на дно сумки, в которую под утро 
                                залез обратно. Утром Дронов и Перов забрали сумку с Суховым. Такой прием они 
                                повторяли несколько раз, пока Сухова не обнаружили в сумке работники вокзала. 
                                Таким способом подростки завладели деньгами и вещами на сумму более пятидесяти 
                                тысяч рублей.</p>
                                <p>Дайте анализ уголовно - правовой ситуации.</p>
                                <p>Имеются ли в действиях указанных лиц признаки состава преступления ? (См.УК 
                                РФ ст. 14, 17, 20, 24, 32, 35, 87, 90 - 92, 150)",
                Right = @"<p>Дронову и Перову по семнадцать лет, а значит, согласно ст. 20 УК РФ, оба достигли 
                        возраста уголовной ответственности, хотя по-прежнему являются несовершеннолетними. 
                        Статья 87 поясняет, что несовершеннолетними признаются лица, которым ко времени совершения 
                        преступления исполнилось четырнадцать, но не исполнилось восемнадцати лет. Ч.1 вышеупомянутой 
                        статьи говорит о том, что только суд решает, назначать ли совершившему преступление 
                        несовершеннолетнему лицу наказание, применять ли принудительные меры воспитательного 
                        воздействия или же освободить от наказания и поместить в специальное учебно-воспитательное 
                        учреждение закрытого типа органа управления образованием.</p>
                        <p>Квалифицироваться деяние Дронова и Перова будет, скорее всего, по пунктам «а», «б» ч. 2 
                        ст. 158 УК РФ, а именно тайное хищение чужого имущества (кража) группой лиц по 
                        предварительному сговору, с проникновением в хранилище.</p>
                        <p>Про предварительный сговор смотрим ч.2 ст. 35 УК РФ: преступление признается совершенным
                        группой лиц по предварительному сговору, если в нем участвовали лица, заранее договорившиеся 
                        о совместном совершении преступления.</p>
                        <p>Также в данном случае образуется совокупность преступлений (ст. 17), и деяние Дронова и 
                        Перова будет квалифицироваться как вовлечение несовершеннолетнего в совершение кражи (ч. 1 
                        ст. 150, п. «а», «б» ч. 2 ст. 158 УК РФ).</p>
                        <p>Одиннадцатилетний Сухов уголовной ответственности не подлежит в силу своего возраста 
                        (см. главу 4 УК РФ).</p>",
                Title = "Задание по уголовному праву",
                Section = section2
            };
            var task3 = new Task
            {
                Description = @"<p>Пятнадцатилетние Шохин и Савельев надели черные маски и, желая над кем-нибудь 
                                подшутить, поздно вечером вышли на улицу. Увидев знакомого им Ковалева с девушкой, 
                                они приблизились к ним. Шохин сзади схватил за туловище Ковалева, а Савельев 
                                направил на него газовый пистолет и произнес: «Не трепыхайся, будет хуже». 
                                Ковалев вырвался и нанес удар ногой в живот Шохину и трижды Савельеву по голове. 
                                В результате последнему была причинена черепно-мозговая травма, от которой он, 
                                не приходя в сознание, скончался на следующий день. Ковалев и его спутница 
                                скрылись с места происшествия, однако в дальнейшем они были обнаружены и задержаны.</p>
                                <p>Дайте уголовно-правовую оценку изложенным обстоятельствам.</p>
                                <p>Имеется ли в действиях указанных лиц состав преступления?</p>
                                <p>Подлежит ли Ковалев уголовно-правовой ответственности? (См. УК РФ ст. 14, 15, 24, 
                                26, 37, 109).</p>",
                Right = @"<p>Действия Ковалева можно расценивать двояко. Рассмотрим оба варианта.</p>
                        <p>Согласно статье 14 УК РФ, преступление – это виновно совершенное общественно опасное деяние, 
                        запрещенное Уголовным кодексом под угрозой наказания. Форма вины, исходя из ст. 24, бывает двух 
                        видов: умысел и неосторожность. Суд может решить, что убийство Савельева Шохиным было совершено 
                        по небрежности, т.е. когда лицо не предвидело возможности наступления общественно опасных 
                        последствий своих действий, хотя при необходимой внимательности и предусмотрительности должно 
                        было и могло предвидеть эти последствия (ч.3 ст. 26 УК РФ).</p>
                        <p>В таком случае, Ковалев будет признан виновным по статье 109 Уголовного кодекса РФ, которая 
                        говорит о причинении смерти по неосторожности.</p>
                        <p>Другой вариант развития событий. Ковалев, будучи неосведомленным о том, что Шохин и Савельев 
                        решили просто подшутить над своим знакомым, воспринимал двух нападавших в масках, да еще с 
                        пистолетом в руках, как вполне серьезную угрозу для себя и своей спутницы. В таком случае его 
                        действия могут быть признаны необходимой обороной (см. ст. 37 УК РФ), которой признаются такие 
                        действия, которые совершаются при защите личности и прав обороняющегося от общественно опасного 
                        посягательства, если это посягательство было сопряжено с насилием, опасным для жизни 
                        обороняющегося или другого лица, либо с непосредственной угрозой применения такого насилия. 
                        Вопрос о соразмерности и о возможном превышении пределов необходимой обороны будет решаться 
                        на усмотрение суда. Полагаем, что решающим фактором будет наличие оружия в руках у Савельева и 
                        позднее время суток, ведь в темноте и будучи в состоянии переживания от внезапно возникшей 
                        опасности Ковалев мог принять газовый пистолет за огнестрельное оружие.</p>
                        <p>В таком случае, Ковалев уголовно-правовой ответственности подлежать не будет.</p>
                        <p>В действиях Савельева и Шохина состава преступления не усматривается, поскольку умысла на 
                        настоящее разбойное нападение у данных лиц не было. Исходя из условий, они хотели лишь 
                        подшутить над кем-нибудь.</p>",
                Title = "Задание общего профиля",
                Section = section3
            };
            var task4 = new Task
            {
                Description = @"<p>Багиров, проживая в гостинице, похитил из соседнего номера у Джамалова 20 г гашиша. 
                                На другой день в квартире Исаева Багиров предложил четырнадцатилетнему Волину выкурить 
                                сигарету, пообещав ему «необычайный кайф» от этого. Волин выкурил предложенную сигарету. 
                                Следствием установлено, что хозяин квартиры Исаев на протяжении года предоставлял свою 
                                квартиру различным лицам для употребления наркотиков, за что получал деньги и спиртные 
                                напитки.</p>
                                <p>Дайте уголовно-правовой анализ изложенных фактов.</p>
                                <p>Имеется ли в действиях Багирова и Исаева состав преступления?</p>
                                <p>Можно ли привлечь к уголовной ответственности Джамалова, Исаева, Багирова и Волина? 
                                (См. УК РФ ст. 14, 15, 17, 20, 228, 228.1, 229, 230, 232).</p>",
                Right = @"<p>Анализ уголовно-правовой ситуации в данной задаче не такой сложный, как это может 
                            показаться на первый взгляд. Разберемся подробно с характеристикой действий каждого из 
                            лиц, попавших в поле зрения составителя задачи.</p>
                            <p>Согласно Сводной таблице заключений Постоянного комитета по контролю наркотиков об 
                            отнесении к небольшим, крупным и особо крупным размерам количеств наркотических средств, 
                            психотропных и сильнодействующих веществ, обнаруженных в незаконном хранении или обороте, 
                            20 грамм гашиша – это крупный размер количеств наркотического средства.</p>
                            <p>Из-за недостаточности исходных данных, представленных в задаче, нельзя сделать точные 
                            выводы о том, по какой статье привлекать Джамалова к уголовной ответственности. Все 
                            зависит от целей, ради которых он хранил у себя такое количество (а может – и больше) 
                            гашиша. Если цель – сбыт, то квалификация будет проходить по п. «б» ч.2 ст. 228.1 
                            (незаконный сбыт наркотических средств в крупном размере). Если цели сбыта не было, то - 
                            статья 228, предусматривающая пять действий, которые могут повлечь привлечение к уголовной 
                            ответственности по этой статье. Нас же, в данном случае, интересует только хранение.</p>
                            <p>Следующее лицо в данной истории – Багиров. По совокупности преступлений (ст. 17 УК РФ) 
                            суд может квалифицировать действия Багирова по пункту «а» ч.3 ст. 230 и пункту «б» ч.3 ст. 
                            229 УК РФ, а именно - склонение несовершеннолетнего к потреблению наркотических средств и 
                            хищение наркотических средств в крупном размере.</p>
                            <p>Четырнадцатилетний Волин, вопреки своей фамилии, выкурил сигарету с гашишем, 
                            предложенную Багирову. Но, в силу своего несовершеннолетия, а так же уголовного закона, 
                            ответственности он подлежать не будет (ч.2 ст. 20).</p>
                            <p>Исаев - хозяин квартиры, которую он, по сути, превратил в притон - подлежит 
                            ответственности, в частности, по ч.1 статьи 232, за организацию и содержание притонов
                            для потребления наркотических средств. Притоном является жилое (в данном случае, квартира) 
                            помещение, систематически предоставляемое одному и тому же лицу либо разным лицам для 
                            потребления наркотических средств или психотропных веществ. Для наступления ответственности 
                            по ст. 232 достаточно двукратного предоставления помещения для указанных целей. В нашем 
                            случае квартира являлась притоном в течение года. Таким образом, преступление длящееся. 
                            Также нужна дополнительная квалификация по соответствующим частям статьи 228.1 УК РФ.</p>",
                Title = "Задание общего профиля",
                Section = section3
            };
            var task5 = new Task
            {
                Description = @"Тринадцатилетний Занин предложил Ерину, студенту первого курса сельскохозяйственного 
                                института, ночью похитить мотоцикл из гаража соседа. Через некоторое время они 
                                совершили задуманное, а мотоцикл продали. Суд осудил Ерина по ст. 151 УК РФ за 
                                вовлечение несовершеннолетнего в совершение антиобщественных действий.</p>
                                <p>Содержится ли в действиях Занина и Ерина состав преступления?</p>
                                <p>Правильно ли осужден Ерин за данное правонарушение? (См. УК РФ ст. 14, 15, 16, 
                                20, 151, 158).</p>",
                Right = @"<p>В действиях Ерина определенно содержится состав преступления – кражи (ст. 158 УК РФ), 
                        поскольку, если смотреть на условия задачи, он является студентом первого курса, а, значит, 
                        наверняка достиг возраста 18 лет. Впрочем, даже если еще нет, то ч.2 ст. 20 УК РФ говорит 
                        о том, что лицо, достигшее ко времени совершения преступления четырнадцатилетнего возраста, 
                        подлежит уголовной ответственности за такое преступление, как кража.</p>
                        <p>Статья 151 неприменима в данной ситуации, поскольку хищение не является антиобщественным 
                        действием (систематическое употребление спиртных напитков, одурманивающих веществ, занятие 
                        бродяжничеством или попрошайничеством), это – преступление. А значит, если предположить, 
                        что именно Ерин склонил несовершеннолетнего Занина к преступлению, то и отвечать он должен 
                        был по соответствующей статье – 150, вовлечение несовершеннолетнего в совершение преступления.</p>
                        <p>Но в нашем случае мы видим, что именно Занин предложил Ерину пойти на кражу. Следовательно,
                        если суд тщательно и независимо подойдет к рассмотрению этого дела, то к уголовной ответственности 
                        за ст. 150 с студент Ерина привлечен не будет. Только к ст. 158.</p>
                        <p>Занин, не достигший 14 лет, уголовной ответственности или принудительным мерам воспитательного 
                        воздействия не подлежит.</p>",
                Title = "Задание общего профиля",
                Section = section3
            };
            database.Task.Add(task1);
            database.Task.Add(task2);
            database.Task.Add(task3);
            database.Task.Add(task4);
            database.Task.Add(task5);
            #endregion
            #region Courses
            //database.Course.Add(new Course
            //{
            //    Description = "Уголовное право как отрасль права и как наука. Понятие, предмет и метод уголовного права",
            //    Link = "6a6f40db-981a-41f9-8c76-473f95351791.pdf",
            //    Section = section1
            //});
            //database.Course.Add(new Course
            //{
            //    Description = "Уголовное законодательство Российской Федерации, его задачи и принципы",
            //    Link = "d1682315-c188-4c98-a44f-4af8ba239c1c.pdf",
            //    Section = section1
            //});
            //database.Course.Add(new Course
            //{
            //    Description = "Теоретические, процессуальные и организационные основы судебной экспертизы",
            //    Link = "b953c69d-b793-46f4-8886-33cc45047cf8.pdf",
            //    Section = section2
            //});
            database.Course.Add(new Course
            {
                Description = "О разграничении компетенции прокуроров территориальных, военных и других специализированных прокуратур",
                Link = "b113c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "О совершенствовании прокурорского надзора за исполнением федерального законодательства органами государственной власти",
                Link = "b123c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об изменении организации прокурорского надзора за исполнением законов на транспорте и в таможенных органах и реорганизации транспортных прокуратур",
                Link = "b133c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об образовании при Генеральной прокуратуре РФ Научно-консультативного совета",
                Link = "b143c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за законностью нормативных правовых актов органов государственной власти субъектов РФ и местного самоуправления",
                Link = "b153c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов в жилищно-коммунальной сфере",
                Link = "b163c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов в сфере оборонно-промышленного комплекса",
                Link = "b173c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов в сфере противодействия легализации (отмыванию) доходов",
                Link = "b183c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов о несовершеннолетних и молодежи",
                Link = "b193c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов о противодействии терроризму",
                Link = "b203c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });            
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов о противодействии экстимистской деятельности",
                Link = "b213c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов об охране",
                Link = "b223c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов при осуществлении оперативно-розыскной деятельности",
                Link = "b233c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов при приеме, регистрации и разрешении сообщений о преступлениях",
                Link = "b243c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законов, соблюдением прав и свобод человека и гражданина",
                Link = "b253c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законодательства в сфере миграции",
                Link = "b263c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законодательства о корупции",
                Link = "b273c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законодательства о налогах и сборах",
                Link = "b283c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением законодательства о предупреждении и ликвидации чрезвычайных ситуаций природного характера и их последствий",
                Link = "b293c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за исполнением требований закона о соблюдении разумного срока на досудебных стадиях уголовного судопроизводства",
                Link = "b303c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за использованием законов судебными приставами",
                Link = "b313c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за оперативно-розыскной деятельностью Временной оперативной группировки органов внутренних дел",
                Link = "b323c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за оперативно-розыскной деятельностью",
                Link = "b333c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за процессуальной деятельностью органов дознания",
                Link = "b343c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за процессуальной деятельностью органов предварительного следствия",
                Link = "b353c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за соблюдением законодательства о выборах Президенита РФ",
                Link = "b363c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за соблюдением законодательства при содержании подозреваемых и обвиняемых",
                Link = "b373c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за соблюдением конституционных прав граждан в уголовном судопроизводстве",
                Link = "b383c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за соблюдением прав несовершенно летних на досудебных стадиях уголовного судопроизводства",
                Link = "b393c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об организации прокурорского надзора за соблюдением прав субъектов предпринимательской деятельности",
                Link = "b403c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Об усилении прокурорского надзора и ведомственного контроля за законностью процессуальных действий и принимаемых решений об отказе в возбуждении уголовного дела при разрешении сообщений о преступлениях",
                Link = "b413c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Следственный комитет Российской Федерации",
                Link = "b423c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Закон о прокуратуре",
                Link = "b433c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.Course.Add(new Course
            {
                Description = "Конституция 2019",
                Link = "b443c69d-b793-46f4-8886-33cc45047cf8.pdf",
                Section = section3
            });
            database.SaveChanges();
            #endregion
            #region Grades
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
                Value = 0.35
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student3,
                Value = 0.25
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student5,
                Value = 0.5
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student6,
                Value = 0.85
            });
            database.Grade.Add(new Grade
            {
                Test = test1,
                User = student7,
                Value = 1
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student8,
                Value = 1
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student12,
                Value = 0.25
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student22,
                Value = 0.5
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student31,
                Value = 0.85
            });
            database.Grade.Add(new Grade
            {
                Test = test1,
                User = student15,
                Value = 0.3
            });
            database.Grade.Add(new Grade
            {
                Test = test1,
                User = student16,
                Value = 0.5
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student13,
                Value = 0.1
            });
            database.Grade.Add(new Grade
            {
                Test = test1,
                User = student25,
                Value = 0.33
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student28,
                Value = 0.7
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student29,
                Value = 1
            });
            database.Grade.Add(new Grade
            {
                Test = test1,
                User = student24,
                Value = 1
            });
            database.Grade.Add(new Grade
            {
                Test = test2,
                User = student25,
                Value = 1
            });
            database.SaveChanges();
            #endregion
            #region Other
            database.Type.Add(type2);
            database.GroupSection.Add(new GroupSection
            {
                GroupID = 1,
                SectionID = 1
            });
            database.GroupSection.Add(new GroupSection
            {
                GroupID = 1,
                SectionID = 2
            });
            database.GroupSection.Add(new GroupSection
            {
                GroupID = 1,
                SectionID = 3
            });
            database.GroupSection.Add(new GroupSection
            {
                GroupID = 2,
                SectionID = 1
            });
            database.GroupSection.Add(new GroupSection
            {
                GroupID = 2,
                SectionID = 2
            });
            database.GroupSection.Add(new GroupSection
            {
                GroupID = 2,
                SectionID = 3
            });
            database.UserTask.Add(new UserTask {
                TaskID = 1,
                UserID = 9,
                Grade = "Хорошо",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 3,
                UserID = 9,
                Grade = "Удовлетворительно",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 4,
                UserID = 15,
                Grade = "Отлично",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 5,
                UserID = 15,
                Grade = "Хорошо",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 5,
                UserID = 16,
                Grade = "Хорошо",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 1,
                UserID = 25,
                Grade = "Отлично",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 1,
                UserID = 9,
                Grade = "Удовлетворительно",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 3,
                UserID = 29,
                Grade = "Отлично",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 4,
                UserID = 29,
                Grade = "Хорошо",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 5,
                UserID = 29,
                Grade = "Отлично",
                Answer = "Ответ на вопрос"
            });

            database.UserTask.Add(new UserTask
            {
                TaskID = 1,
                UserID = 3,
                Grade = "Хорошо",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 3,
                UserID = 3,
                Grade = "Удовлетворительно",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 4,
                UserID = 3,
                Grade = "Отлично",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 5,
                UserID = 3,
                Grade = "Отлично",
                Answer = "Ответ на вопрос"
            });

            database.UserTask.Add(new UserTask
            {
                TaskID = 1,
                UserID = 18,
                Grade = "Хорошо",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 3,
                UserID = 18,
                Grade = "Удовлетворительно",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 4,
                UserID = 18,
                Grade = "Отлично",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 5,
                UserID = 18,
                Grade = "Удовлетворительно",
                Answer = "Ответ на вопрос"
            });

            database.UserTask.Add(new UserTask
            {
                TaskID = 1,
                UserID = 7,
                Grade = "Отлично",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 3,
                UserID = 7,
                Grade = "Удовлетворительно",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 4,
                UserID = 7,
                Grade = "Хорошо",
                Answer = "Ответ на вопрос"
            });
            database.UserTask.Add(new UserTask
            {
                TaskID = 5,
                UserID = 7,
                Grade = "Неудовлетворительно",
                Answer = "Ответ на вопрос"
            });
            database.SaveChanges();
            #endregion
        }
    }
}