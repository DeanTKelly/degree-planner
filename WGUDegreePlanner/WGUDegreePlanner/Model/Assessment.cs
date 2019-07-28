using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WGUDegreePlanner.Model
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentID { get; set; }
        public string AssessmentName { get; set; } 
        public string AssessmentType { get; set; }
        public DateTime AssessmentStart { get; set; }
        public DateTime AssessmentEnd { get; set; }
        public bool AssessmentNotifications { get; set; }
        public int CourseID { get; set; }
    }
}
