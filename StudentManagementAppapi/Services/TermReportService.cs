using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Repository;

namespace StudentManagementAppapi.Services
{
    public class TermReportService : ITermReportService
    {
        private readonly ITermReportRepository _termReportRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicTermRepository _termRepository;
        private readonly IResultRepository _resultRepository;

        public TermReportService(
            ITermReportRepository termReportRepository,
            IStudentRepository studentRepository,
            IAcademicTermRepository termRepository,
            IResultRepository resultRepository)
        {
            _termReportRepository = termReportRepository;
            _studentRepository = studentRepository;
            _termRepository = termRepository;
            _resultRepository = resultRepository;
        }

        //public async Task<ResponseWrapper<TermReportDto>> CreateTermReportAsync(CreateTermReportDto dto)
        //{
        //    try
        //    {
        //        var student = await _studentRepository.GetByIdAsync(dto.StudentId);
        //        if (student == null)
        //            return ResponseWrapper<TermReportDto>.Failure("Student not found");

        //        var term = await _termRepository.GetByIdAsync(dto.TermId);
        //        if (term == null)
        //            return ResponseWrapper<TermReportDto>.Failure("Academic term not found");

        //        var existingReport = await _termReportRepository.GetByStudentAndTermAsync(dto.Student.AdmissionNumber, dto.TermId);
        //        if (existingReport != null)
        //            return ResponseWrapper<TermReportDto>.Failure("Report already exists for this student and term");

        //        var termReport = new TermReport
        //        {
        //            StudentId = dto.StudentId,
        //            TermId = dto.TermId,
        //            TotalScore = dto.TotalScore,
        //            AverageScore = dto.AverageScore,
        //            PositionInClass = dto.PositionInClass,
        //            ClassTeacherComment = dto.ClassTeacherComment,
        //            PrincipalComment = dto.PrincipalComment,
        //            NextTermBegins = dto.NextTermBegins,
        //            GeneratedDate = dto.GeneratedDate,
        //            CreatedAt = DateTime.UtcNow
        //        };

        //        await _termReportRepository.AddAsync(termReport);
        //        return ResponseWrapper<TermReportDto>.Success(MapToDto(termReport), "Term report created successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseWrapper<TermReportDto>.Failure($"Error creating term report: {ex.Message}");
        //    }
        //}

        public async Task<ResponseWrapper<TermReportDto>> CreateTermReportAsync(CreateTermReportDto dto)
        {
            try
            {
                // Validate student exists
                var student = await _studentRepository.GetByIdAsync(dto.StudentId);
                if (student == null)
                    return ResponseWrapper<TermReportDto>.Failure("Student not found");

                // Validate term exists
                var term = await _termRepository.GetByIdAsync(dto.TermId);
                if (term == null)
                    return ResponseWrapper<TermReportDto>.Failure("Academic term not found");

                
                var existingReport = await _termReportRepository.GetByStudentAndTermAsync(student.AdmissionNumber, dto.TermId);
                if (existingReport != null)
                    return ResponseWrapper<TermReportDto>.Failure("Report already exists for this student and term");

                
                var studentResults = await _resultRepository.GetByStudentAndTermAsync(dto.admissionNo, dto.TermId);
                if (!studentResults.Any())
                    return ResponseWrapper<TermReportDto>.Failure("No assessment results found for this student in the specified term");

              
                var totalScore = studentResults.Sum(r => r.Score);
                var averageScore = studentResults.Average(r => r.Score);

               
                var classStudents = await _studentRepository.GetByClassIdAsync(student.ClassId);
                var classResults = new List<(Guid StudentId, decimal AverageScore)>();

                foreach (var classStudent in classStudents)
                {
                    var results = await _resultRepository.GetByStudentAndTermAsync(classStudent.AdmissionNumber, dto.TermId);
                    if (results.Any())
                    {
                        var avgScore = results.Average(r => r.Score);
                        classResults.Add((classStudent.Id, avgScore));
                    }
                }

                
                var sortedResults = classResults.OrderByDescending(r => r.AverageScore).ToList();
                var positionInClass = sortedResults.FindIndex(r => r.StudentId == dto.StudentId) + 1;
                var totalStudentsInClass = sortedResults.Count;

                
                var grade = CalculateGrade(averageScore);

               
                var termReport = new TermReport
                {
                    StudentId = dto.StudentId,
                    TermId = dto.TermId,
                    ClassId = student.ClassId,
                    TotalScore = totalScore,
                    AverageScore = Math.Round(averageScore, 2),
                    PositionInClass = positionInClass,
                    TotalStudentsInClass = totalStudentsInClass,
                    Grade = grade,
                    ClassTeacherComment = dto.ClassTeacherComment ?? GenerateClassTeacherComment(averageScore),
                    PrincipalComment = dto.PrincipalComment ?? GeneratePrincipalComment(averageScore),
                    NextTermBegins = dto.NextTermBegins ?? GetNextTermStartDate(term),
                    GeneratedDate = DateTime.UtcNow,
                    IsPublished = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _termReportRepository.AddAsync(termReport);

                var reportDto = MapToDto(termReport);
                return ResponseWrapper<TermReportDto>.Success(reportDto, "Term report created successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<TermReportDto>.Failure($"Error creating term report: {ex.Message}");
            }
        }

        
        private string CalculateGrade(decimal averageScore)
        {
            return averageScore switch
            {
                >= 90 => "A+",
                >= 80 => "A",
                >= 70 => "B+",
                >= 60 => "B",
                >= 50 => "C",
                >= 40 => "D",
                _ => "F"
            };
        }

        
        private string GenerateClassTeacherComment(decimal averageScore)
        {
            return averageScore switch
            {
                >= 90 => "Excellent performance. Keep up the outstanding work!",
                >= 80 => "Very good performance. Well done!",
                >= 70 => "Good performance. Continue working hard.",
                >= 60 => "Satisfactory performance. There's room for improvement.",
                >= 50 => "Fair performance. Please put in more effort.",
                >= 40 => "Below average performance. Needs significant improvement.",
                _ => "Poor performance. Requires immediate attention and support."
            };
        }

        
        private string GeneratePrincipalComment(decimal averageScore)
        {
            return averageScore switch
            {
                >= 90 => "Outstanding academic achievement. Congratulations!",
                >= 80 => "Commendable performance. Keep striving for excellence.",
                >= 70 => "Good work. Continue to build on this foundation.",
                >= 60 => "Adequate progress. Focus on consistent improvement.",
                _ => "Requires additional support and guidance to reach potential."
            };
        }

       
        private DateTime GetNextTermStartDate(AcademicTerm currentTerm)
        {
            
            return currentTerm.EndDate.AddDays(30); 
        }

       
       
        public async Task<ResponseWrapper<TermReportDto>> GetTermReportByIdAsync(Guid id)
        {
            try
            {
                var termReport = await _termReportRepository.GetByIdAsync(id);
                if (termReport == null)
                    return ResponseWrapper<TermReportDto>.Failure("Term report not found");

                return ResponseWrapper<TermReportDto>.Success(MapToDto(termReport));
            }
            catch (Exception ex)
            {
                return ResponseWrapper<TermReportDto>.Failure($"Error retrieving term report: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<TermReportDto>>> GetAllTermReportsAsync()
        {
            try
            {
                var reports = await _termReportRepository.GetAllAsync();
                return ResponseWrapper<IEnumerable<TermReportDto>>.Success(reports.Select(MapToDto));
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<TermReportDto>>.Failure($"Error retrieving term reports: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<TermReportDto>> UpdateTermReportAsync(Guid id, CreateTermReportDto dto)
        {
            try
            {
                var termReport = await _termReportRepository.GetByIdAsync(id);
                if (termReport == null)
                    return ResponseWrapper<TermReportDto>.Failure("Term report not found");

               
                termReport.ClassTeacherComment = dto.ClassTeacherComment;
                termReport.PrincipalComment = dto.PrincipalComment;
                termReport.NextTermBegins = dto.NextTermBegins;
                termReport.GeneratedDate = DateTime.Now;
                termReport.UpdatedAt = DateTime.UtcNow;


                await _termReportRepository.UpdateAsync(termReport);
                return ResponseWrapper<TermReportDto>.Success(MapToDto(termReport), "Term report updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<TermReportDto>.Failure($"Error updating term report: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteTermReportAsync(Guid id)
        {
            try
            {
                var report = await _termReportRepository.GetByIdAsync(id);
                if (report == null)
                    return ResponseWrapper<bool>.Failure("Term report not found");

                await _termReportRepository.DeleteAsync(id);
                return ResponseWrapper<bool>.Success(true, "Term report deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting term report: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<TermReportDto>> GetTermReportByStudentAndTermAsync(string admissionNo, Guid termId)
        {
            try
            {
                var report = await _termReportRepository.GetByStudentAndTermAsync(admissionNo, termId);
                if (report == null)
                    return ResponseWrapper<TermReportDto>.Failure("Term report not found");

                return ResponseWrapper<TermReportDto>.Success(MapToDto(report));
            }
            catch (Exception ex)
            {
                return ResponseWrapper<TermReportDto>.Failure($"Error retrieving term report: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<TermReportDto>>> GetTermReportsByStudentAsync(string admissionNo)
        {
            try
            {
                var reports = await _termReportRepository.GetByStudentAsync(admissionNo);
                return ResponseWrapper<IEnumerable<TermReportDto>>.Success(reports.Select(MapToDto));
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<TermReportDto>>.Failure($"Error retrieving student term reports: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<TermReportDto>>> GetTermReportsByTermAsync(Guid termId)
        {
            try
            {
                var reports = await _termReportRepository.GetByTermAsync(termId);
                return ResponseWrapper<IEnumerable<TermReportDto>>.Success(reports.Select(MapToDto));
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<TermReportDto>>.Failure($"Error retrieving term reports by term: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<TermReportDto>>> GetTermReportsByClassAsync(Guid classId, Guid termId)
        {
            try
            {
                var reports = await _termReportRepository.GetByClassAndTermAsync(classId, termId);
                return ResponseWrapper<IEnumerable<TermReportDto>>.Success(reports.Select(MapToDto));
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<TermReportDto>>.Failure($"Error retrieving term reports by class: {ex.Message}");
            }
        }

        private TermReportDto MapToDto(TermReport report)
        {
            return new TermReportDto
            {
                Id = report.Id,
                StudentId = report.StudentId,
                StudentName = report.Student?.User?.FirstName + " " + report.Student?.User?.LastName ?? string.Empty,
                TermId = report.TermId,
                TermName = report.Term.Name,
                TotalScore = report.TotalScore,
                AverageScore = report.AverageScore,
                PositionInClass = report.PositionInClass,
                ClassTeacherComment = report.ClassTeacherComment,
                PrincipalComment = report.PrincipalComment,
                NextTermBegins = report.NextTermBegins ?? DateTime.MinValue,
                GeneratedDate = report.GeneratedDate
            };
        }

    }
}



