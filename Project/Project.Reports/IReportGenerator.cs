namespace Project.Reports
{
    public interface IReportGenerator<TReportEntity, in TEntity>
    {
        public Report<TReportEntity> GenerateReport(TEntity entity);
    }
}