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
            var group1 = new Group
            {
                Description = "ИФСТ",
                Department = "ИнПИТ",
                University = "СГТУ",
                Start = DateTime.Now,
            };
            var student1 = new User
            {
                Email = "email@gmail.com",
                Group = group1,
                Midname = "Петрович",
                Name = "Петр",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleStudent,
                Status = true,
                Surname = "Петров",
                Username = "petrov_pp"
            };
            var student2 = new User
            {
                Email = "email@gmail.com",
                Group = group1,
                Midname = "Сидорович",
                Name = "Сидр",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleStudent,
                Status = true,
                Surname = "Сидров",
                Username = "sidorov_ss"
            };
            var curator1 = new User
            {
                Email = "email@gmail.com",
                Midname = "Иванович",
                Name = "Иван",
                Password = "pass",
                Phone = "+79022402402",
                Role = roleWorker,
                Status = true,
                Surname = "Иванов",
                Username = "ivanov_ii"
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
            #region Q-A
            var question1 = new Question
            {
                Description = "Сколько океанов на нашей планете?",
                Type = type1
            };
            var question2 = new Question
            {
                Description = "Единица изменения силы тока - это:",
                Type = type1
            };
            var question3 = new Question
            {
                Description = "Сатурн - это какая по счету планета от Солнца?",
                Type = type2
            };
            var question4 = new Question
            {
                Description = "Какой элемент периодической системы химических элементов обозначается как Ag?",
                Type = type1
            };
            var question5 = new Question
            {
                Description = "Сколько будет 0,2 км в дециметрах?",
                Type = type1
            };
            var question6 = new Question
            {
                Description = "Самая длинная в мире река - это:",
                Type = type2
            };
            var question7 = new Question
            {
                Description = "Какое число обозначается римскими цифрами LXXVII?",
                Type = type2
            };
            var question8 = new Question
            {
                Description = "В каком предложении не допущена ошибка?",
                Type = type1
            };
            var question9 = new Question
            {
                Description = "Зеленый пигмент, окрашивающий листья растений, называется:",
                Type = type1
            };
            var question10 = new Question
            {
                Description = "Все знают приставку <кило->. А как насчет <гекто->? Это сколько?",
                Type = type1
            };
            var question11 = new Question
            {
                Description = "Почему времена года сменяют друг друга?",
                Type = type1
            };
            var question12 = new Question
            {
                Description = "В какой строке все слова написаны правильно?",
                Type = type1
            };
            var question13 = new Question
            {
                Description = "Сколько хромосом в геноме человека?",
                Type = type2
            };

            var answer1 = new Answer
            {
                Desctiption = "4",
                Correct = false,
                Question = question1
            };
            var answer2 = new Answer
            {
                Desctiption = "5",
                Correct = true,
                Question = question1
            };
            var answer3 = new Answer
            {
                Desctiption = "6",
                Correct = false,
                Question = question1
            };
            var answer4 = new Answer
            {
                Desctiption = "Ампер",
                Correct = true,
                Question = question2
            };
            var answer5 = new Answer
            {
                Desctiption = "Ватт",
                Correct = false,
                Question = question2
            };
            var answer6 = new Answer
            {
                Desctiption = "Вольт",
                Correct = false,
                Question = question2
            };
            var answer7 = new Answer
            {
                Desctiption = "6",
                Correct = true,
                Question = question3
            };
            var answer8 = new Answer
            {
                Desctiption = "Золото",
                Correct = false,
                Question = question4
            };
            var answer9 = new Answer
            {
                Desctiption = "Серебро",
                Correct = true,
                Question = question4
            };
            var answer10 = new Answer
            {
                Desctiption = "Аргон",
                Correct = false,
                Question = question4
            };
            var answer11 = new Answer
            {
                Desctiption = "20000 дм",
                Correct = false,
                Question = question5
            };
            var answer12 = new Answer
            {
                Desctiption = "2000 дм",
                Correct = true,
                Question = question5
            };
            var answer13 = new Answer
            {
                Desctiption = "200 дм",
                Correct = false,
                Question = question5
            };
            var answer14 = new Answer
            {
                Desctiption = "Амазонка",
                Correct = true,
                Question = question6
            };
            var answer15 = new Answer
            {
                Desctiption = "77",
                Correct = true,
                Question = question7
            };
            var answer16 = new Answer
            {
                Desctiption = "На ней не было чулок",
                Correct = false,
                Question = question8
            };
            var answer17 = new Answer
            {
                Desctiption = "На полке лежала пачка макаронов",
                Correct = true,
                Question = question8
            };
            var answer18 = new Answer
            {
                Desctiption = "Эти кремы просрочены",
                Correct = false,
                Question = question8
            };
            var answer19 = new Answer
            {
                Desctiption = "Хлорофиллипт",
                Correct = false,
                Question = question9
            };
            var answer20 = new Answer
            {
                Desctiption = "Хлоропласт",
                Correct = false,
                Question = question9
            };
            var answer21 = new Answer
            {
                Desctiption = "Хлорофилл",
                Correct = true,
                Question = question9
            };
            var answer22 = new Answer
            {
                Desctiption = "100",
                Correct = true,
                Question = question10
            };
            var answer23 = new Answer
            {
                Desctiption = "1000",
                Correct = false,
                Question = question10
            };
            var answer24 = new Answer
            {
                Desctiption = "10000",
                Correct = false,
                Question = question10
            };
            var answer25 = new Answer
            {
                Desctiption = "100000",
                Correct = false,
                Question = question10
            };
            var answer26 = new Answer
            {
                Desctiption = "Из-за удаления и приближения Земли к Солнцу",
                Correct = false,
                Question = question11
            };
            var answer27 = new Answer
            {
                Desctiption = "Из-за наклона земной оси",
                Correct = true,
                Question = question11
            };
            var answer28 = new Answer
            {
                Desctiption = "Из-за вращения Земли вокруг своей оси",
                Correct = false,
                Question = question11
            };
            var answer29 = new Answer
            {
                Desctiption = "Серебряный подстаканник, ветренный день, румяный юноша",
                Correct = false,
                Question = question12
            };
            var answer30 = new Answer
            {
                Desctiption = "Кожаный ремень, соленый суп, избалованный ребенок",
                Correct = true,
                Question = question12
            };
            var answer31 = new Answer
            {
                Desctiption = "Истинный джентльмен, лебединая песня, песчанный пляж",
                Correct = false,
                Question = question12
            };
            var answer32 = new Answer
            {
                Desctiption = "46",
                Correct = true,
                Question = question13
            };
            #endregion
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