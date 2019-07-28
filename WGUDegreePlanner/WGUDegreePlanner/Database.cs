using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;

namespace WGUDegreePlanner
{
    public class Database
    {
        public static SQLiteAsyncConnection connection;
        public Database(string databasePath)
        {
            connection = new SQLiteAsyncConnection(databasePath);
            connection.CreateTablesAsync<Assessment, Course, Term>().Wait();
        }
        public void EvaluationData()
        {
            Term DummyTerm = new Term();
            Course DummyCourse = new Course();
            Assessment DummyAssessment1 = new Assessment();
            Assessment DummyAssessment2 = new Assessment();
            
            var checkTerm = connection.Table<Term>().Where(x => x.TermName == "Term 4").ToListAsync();

            if(checkTerm.Result.Count == 0)
            {
                DummyTerm.TermName = "Term 4";
                DummyTerm.TermStart = DateTime.Today.AddDays(7);
                DummyTerm.TermEnd = DateTime.Today.AddDays(21);
                SaveTerm(DummyTerm);

                var relatedTerm = connection.Table<Term>().Where(x => x.TermName == "Term 4").ToListAsync();
               if(relatedTerm.Result.Count == 1)
                {
                    DummyCourse.CourseName = "Mobile Application Development";
                    DummyCourse.CourseStatus = "Upcoming";
                    DummyCourse.CourseStart = DateTime.Today.AddDays(7);
                    DummyCourse.CourseEnd = DateTime.Today.AddDays(21);
                    DummyCourse.CourseNotifications = true;
                    DummyCourse.InstructorName = "Dean Kelly";
                    DummyCourse.InstructorPhone = "(423)715-8160";
                    DummyCourse.InstructorEmail = "dkell80@wgu.edu";
                    DummyCourse.CourseNotes = "I will learn mobile application development using C#.";
                    DummyCourse.TermID = DummyTerm.TermID;
                    SaveCourse(DummyCourse);

                    var relatedCourse = connection.Table<Course>().Where(x => x.CourseName == "Mobile Application Development").ToListAsync();
                    if(relatedCourse.Result.Count > 0)
                    {
                        DummyAssessment1.AssessmentName = "Microsoft Exam 70-483";
                        DummyAssessment1.AssessmentType = "Objective Assessment";
                        DummyAssessment1.AssessmentStart = DateTime.Today.AddDays(7);
                        DummyAssessment1.AssessmentEnd = DateTime.Today.AddDays(21);
                        DummyAssessment1.CourseID = DummyCourse.CourseID;
                        DummyAssessment2.AssessmentName = "LAP1 - Mobile App Development";
                        DummyAssessment2.AssessmentType = "Performance Assessment";
                        DummyAssessment2.AssessmentStart = DateTime.Today.AddDays(7);
                        DummyAssessment2.AssessmentEnd = DateTime.Today.AddDays(21);
                        DummyAssessment2.CourseID = DummyCourse.CourseID;
                        SaveAssessment(DummyAssessment1);
                        SaveAssessment(DummyAssessment2);
                    }
                }
            }
        }
        //Assessment
        public Task<int> SaveAssessment(Assessment assessment)
        {
            if(assessment.AssessmentID == 0)
            {
                return connection.InsertAsync(assessment);
            }
            else
            {
                return connection.UpdateAsync(assessment);
            }
        }
        public Task<int> DeleteAssessment(Assessment assessment)
        {
            return connection.DeleteAsync(assessment);
        }
        public Task<Assessment> ShowAssessments(int CourseID)
        {
            return connection.Table<Assessment>()
                .Where(a => a.CourseID == CourseID).FirstOrDefaultAsync();
        }
        //Course
        public Task<List<Course>> ShowCourses(Term term)
        {
            return connection.Table<Course>().Where(a => a.TermID == term.TermID).ToListAsync();
        }
        public Task<int> SaveCourse(Course course)
        {
            if(course.CourseID == 0)
            {
                return connection.InsertAsync(course);
            }
            else
            {
                return connection.UpdateAsync(course);
            }
        }
        public Task<int> DeleteCourse(Course course)
        {
            return connection.DeleteAsync(course);
        }
        //Term
        public Task<List<Term>> ShowTerms()
        {
            return connection.Table<Term>().ToListAsync();
        }
        public Task<Term> ShowTerms(int id)
        {
            return connection.Table<Term>().Where(i => i.TermID == id).FirstOrDefaultAsync();
        }
        public Task<List<Assessment>> ShowAssessments(Course course)
        {
            return connection.Table<Assessment>().Where(a => a.CourseID == course.CourseID).ToListAsync();
        }
        public Task<int> SaveTerm(Term term)
        {
            if(term.TermID == 0)
            {
                return connection.InsertAsync(term);
            }
            else
            {
                return connection.UpdateAsync(term);
            }
        }
        public Task<int> DeleteTerm(Term term)
        {
            return connection.DeleteAsync(term);
        }
    }
}
