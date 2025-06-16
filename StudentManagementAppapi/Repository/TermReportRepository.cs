using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class TermReportRepository : ITermReportRepository
    {
        private readonly StudentManagementDbContext _context;

        public TermReportRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<TermReport?> GetByIdAsync(Guid id)
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Term)
                .Include(tr => tr.Class)
                .FirstOrDefaultAsync(tr => tr.Id == id);
        }

        public async Task<IEnumerable<TermReport>> GetAllAsync()
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Term)
                .Include(tr => tr.Class)
                .OrderByDescending(tr => tr.GeneratedDate)
                .ToListAsync();
        }

        public async Task<TermReport> AddAsync(TermReport termReport)
        {
            
            if (termReport.GeneratedDate == default)
            {
                termReport.GeneratedDate = DateTime.UtcNow;
            }

            _context.TermReports.Add(termReport);
            await _context.SaveChangesAsync();
            return termReport;
        }

        public async Task<TermReport> UpdateAsync(TermReport termReport)
        {
            _context.TermReports.Update(termReport);
            await _context.SaveChangesAsync();
            return termReport;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var termReport = await _context.TermReports.FindAsync(id);
            if (termReport == null) return false;

            _context.TermReports.Remove(termReport);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.TermReports.AnyAsync(tr => tr.Id == id);
        }

        public async Task<TermReport?> GetByStudentAndTermAsync(string admissionNo, Guid termId)
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Term)
                .Include(tr => tr.Class)
                .FirstOrDefaultAsync(tr => tr.Student.AdmissionNumber == admissionNo && tr.TermId == termId);
        }

        public async Task<IEnumerable<TermReport>> GetByTermAsync(Guid termId)
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Class)
                .Where(tr => tr.TermId == termId)
                .OrderBy(tr => tr.Class.Name)
                .ThenBy(tr => tr.PositionInClass)
                .ToListAsync();
        }

        public async Task<IEnumerable<TermReport>> GetByStudentAsync(string admissionNo)
        {
            return await _context.TermReports
                .Include(tr => tr.Term)
                .Include(tr => tr.Class)
                .Where(tr => tr.Student.AdmissionNumber == admissionNo)
                .OrderByDescending(tr => tr.Term.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TermReport>> GetByClassAndTermAsync(Guid classId, Guid termId)
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Term)
                .Where(tr => tr.ClassId == classId && tr.TermId == termId)
                .OrderBy(tr => tr.PositionInClass)
                .ToListAsync();
        }

        public async Task<IEnumerable<TermReport>> GetByClassAsync(Guid classId)
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Term)
                .Where(tr => tr.ClassId == classId)
                .OrderByDescending(tr => tr.Term.StartDate)
                .ThenBy(tr => tr.PositionInClass)
                .ToListAsync();
        }

        public async Task<IEnumerable<TermReport>> GetTopPerformersAsync(Guid termId, int count = 10)
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Class)
                .Where(tr => tr.TermId == termId)
                .OrderBy(tr => tr.PositionInClass)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<TermReport>> GetTopPerformersByClassAsync(Guid classId, Guid termId, int count = 5)
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Term)
                .Where(tr => tr.ClassId == classId && tr.TermId == termId)
                .OrderBy(tr => tr.PositionInClass)
                .Take(count)
                .ToListAsync();
        }

        public async Task<bool> IsReportPublishedAsync(Guid studentId, Guid  termId)
        {
            var report = await _context.TermReports
                .FirstOrDefaultAsync(tr => tr.StudentId == studentId && tr.TermId == termId);

            return report?.IsPublished ?? false;
        }

        public async Task<int> GetStudentCountByTermAsync(Guid termId)
        {
            return await _context.TermReports
                .CountAsync(tr => tr.TermId == termId);
        }

        public async Task<decimal> GetClassAverageAsync(Guid classId, Guid termId)
        {
            var reports = await _context.TermReports
                .Where(tr => tr.ClassId == classId && tr.TermId == termId)
                .ToListAsync();

            return reports.Any() ? reports.Average(tr => tr.AverageScore) : 0;
        }

        public async Task<IEnumerable<TermReport>> GetPendingReportsAsync(Guid termId)
        {
            return await _context.TermReports
                .Include(tr => tr.Student)
                .Include(tr => tr.Class)
                .Where(tr => tr.TermId == termId && !tr.IsPublished)
                .OrderBy(tr => tr.Class.Name)
                .ThenBy(tr => tr.Student.User.LastName)
                .ToListAsync();
        }

        public async Task<bool> PublishReportAsync(Guid reportId)
        {
            var report = await _context.TermReports.FindAsync(reportId);
            if (report == null) return false;

            report.IsPublished = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PublishReportsByTermAsync(Guid termId)
        {
            var reports = await _context.TermReports
                .Where(tr => tr.TermId == termId && !tr.IsPublished)
                .ToListAsync();

            if (!reports.Any()) return false;

            foreach (var report in reports)
            {
                report.IsPublished = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }


}
